// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Reactive.Concurrency;
using Com.Daml.Ledger.Api.Util;
using Daml.Ledger.Client.Auth.Client;
using Grpc.Core;

namespace Daml.Ledger.Client.Reactive
{
    /// <summary>
    /// A ILedgerClient implementation that connects to an existing Ledger and provides clients to query it. To use the DamlLedgerClient create an instance
    /// of DamlLedgerClient using ForLedgerIdAndHost(string, string, int, Optional), ForHostWithLedgerIdDiscovery(string, int, Optional) or
    /// DamlLedgerClient(Optional, ManagedChannel)
    /// When ready to use the clients, call the method Connect() to initialize the clients for that particular Ledger
    /// Retrieve one of the clients by using a property, e.g. ActiveContractSetClient
    /// For information on how to set up an SslContext object for mutual authentication please refer to
    /// https://github.com/grpc/grpc-java/blob/master/SECURITY.md and the section on security in the grpc-java documentation.
    /// </summary>
    public class DamlLedgerClient : ILedgerClient
    {
        public sealed class Builder
        {
            private readonly string _host;
            private readonly int _port;
            private Optional<ChannelCredentials> _channelCredentials;
            private Optional<string> _expectedLedgerId = None.Value;
            private Optional<string> _accessToken = None.Value;

            internal Builder(string host, int port)
            {
                _host = host;
                _port = port;
            }

            public Builder WithChannelCredentials(ChannelCredentials channelCredentials)
            {
                _channelCredentials = channelCredentials;
                return this;
            }

            public Builder WithExpectedLedgerId(string expectedLedgerId)
            {
                _expectedLedgerId = Optional.Of(expectedLedgerId);
                return this;
            }

            public Builder WithAccessToken(string accessToken)
            {
                if (!string.IsNullOrEmpty(accessToken))
                {
                    _accessToken = Optional.Of(accessToken);
                    if (!_channelCredentials.IsPresent)
                        _channelCredentials = new SslCredentials();
                }
                return this;
            }

            public DamlLedgerClient Build()
            {
                ChannelCredentials channelCredentials = _channelCredentials.Reduce(_accessToken.MapOrElse((t) => new SslCredentials(), () => ChannelCredentials.Insecure)); 
                
                return new DamlLedgerClient(_expectedLedgerId, new Channel($"{_host}:{_port}", channelCredentials), _accessToken);
            }
        }

        /// <summary>
        /// Create a new Builder with the given parameters
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static Builder NewBuilder(string host, int port) => new Builder(host, port);

        public string LedgerId { get; private set; }
        public ActiveContractsClient ActiveContractsClient { get; private set; }
        public TransactionsClient TransactionClient { get; private set; }
        public CommandClient CommandClient { get; private set; }
        public CommandCompletionClient CommandCompletionClient { get; private set; }
        public CommandSubmissionClient CommandSubmissionClient { get; private set; }
        public LedgerIdentityClient LedgerIdentityClient { get; private set; }
        public PackageClient PackageClient { get; private set; }
        public LedgerConfigurationClient LedgerConfigurationClient { get; private set; }
        public IAdminClients AdminClients { get; private set; }
        public ITestingClients TestingClients { get; private set; }

        /// <summary>
        /// Connects this instance of the {@link DamlLedgerClient} to the Ledger.
        /// </summary>
        public void Connect(IScheduler scheduler = null)
        {
            _scheduler = scheduler;

            LedgerIdentityClient = new Daml.Ledger.Client.Reactive.LedgerIdentityClient(_channel, _accessToken);

            string reportedLedgerId = LedgerIdentityClient.GetLedgerIdentity().Result;

            if (LedgerId != null && LedgerId != reportedLedgerId)
                throw new ArgumentException($"Configured ledger id {LedgerId} is not the same as reported by the ledger {reportedLedgerId}");
            
            LedgerId = reportedLedgerId;
            
            ActiveContractsClient = new ActiveContractsClient(LedgerId, _channel, _accessToken, _scheduler);
            TransactionClient = new TransactionsClient(LedgerId, _channel, _accessToken, _scheduler);
            CommandCompletionClient = new CommandCompletionClient(LedgerId, _channel, _accessToken, _scheduler);
            CommandSubmissionClient = new CommandSubmissionClient(LedgerId, _channel, _accessToken);
            CommandClient = new CommandClient(LedgerId, _channel, _accessToken);
            PackageClient = new PackageClient(LedgerId, _channel, _accessToken);
            LedgerConfigurationClient = new LedgerConfigurationClient(LedgerId, _channel, _accessToken, _scheduler);

            AdminClients = new AdminClientsImpl(new Admin.ConfigManagementClient(_channel, _accessToken), new Admin.PackageManagementClient(_channel, _accessToken), new Admin.PartyManagementClient(_channel, _accessToken));

            TestingClients = new TestingClientsImpl(new Testing.TimeClient(LedgerId, _channel, _accessToken, _scheduler), new Testing.ResetClient(LedgerId, _channel, _accessToken));
        }

        public class AdminClientsImpl : IAdminClients
        {
            public AdminClientsImpl(Admin.ConfigManagementClient configManagementClient, Admin.PackageManagementClient packageManagementClient, Admin.PartyManagementClient partyManagementClient)
            {
                ConfigManagementClient = configManagementClient;
                PackageManagementClient = packageManagementClient;
                PartyManagementClient = partyManagementClient;
            }

            public Admin.ConfigManagementClient ConfigManagementClient { get; }
            public Admin.PackageManagementClient PackageManagementClient { get; }
            public Admin.PartyManagementClient PartyManagementClient { get; }
        }

        private class TestingClientsImpl : ITestingClients
        {
            public TestingClientsImpl(Testing.TimeClient timeClient, Testing.ResetClient resetClient)
            {
                TimeClient = timeClient;
                ResetClient = resetClient;
            }

            public Testing.TimeClient TimeClient { get; }
            public Testing.ResetClient ResetClient { get; }
        }

        /// <summary>
        /// Shutdown the connection
        /// </summary>
        public void Close()
        {
            _channel.ShutdownAsync().Wait();
        }
        
        /// <summary>
        /// Constructor for use by the Builder
        /// </summary>
        /// <param name="expectedLedgerId"></param>
        /// <param name="channel"></param>
        private DamlLedgerClient(Optional<string> expectedLedgerId, Channel channel, Optional<string> accessToken)
        {
            _channel = channel;
            expectedLedgerId.IfPresent(id => LedgerId = id);
            _accessToken = accessToken;
        }

        private readonly Channel _channel;
        private IScheduler _scheduler;
        private readonly Optional<string> _accessToken;
    }
}
