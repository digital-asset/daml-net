// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using System;
    using Admin;
    using Testing;
    using Grpc.Core;

    public class Clients
    {
        public string LedgerId { get; }

        public IActiveContractsClient       ActiveContractsClient       { get; }
        public ICommandClient               CommandClient               { get; }
        public ICommandCompletionClient     CommandCompletionClient     { get; }
        public ICommandSubmissionClient     CommandSubmissionClient     { get; }
        public ILedgerConfigurationClient   LedgerConfigurationClient   { get; }
        public ILedgerIdentityClient        LedgerIdentityClient        { get; }
        public IPackageClient               PackageClient               { get; }
        public ITransactionsClient          TransactionsClient          { get; }
        public AdminClients                 Admin                       { get; }
        public TestingClients               Testing                     { get; }

        public sealed class Builder
        {
            private readonly string _host;
            private readonly int _port;
            private string _expectedLedgerId;
            private ChannelCredentials _channelCredentials;
            private string _accessToken;

            public Builder(string host, int port)
            {
                _host = host;
                _port = port;
            }

            public Builder WithExpectedLedgerId(string expectedLedgerId)
            {
                return new Builder(_host, _port, expectedLedgerId, _channelCredentials, _accessToken);
            }

            public Builder WithChannelCredentials(ChannelCredentials channelCredentials)
            {
                return new Builder(_host, _port, _expectedLedgerId, channelCredentials, _accessToken);
            }
    
            public Builder WithAccessToken(string accessToken)
            {
                return new Builder(_host, _port, _expectedLedgerId, _channelCredentials, accessToken);
            }

            public Clients Build()
            {
                ChannelCredentials channelCredentials = _channelCredentials;
                    
                if (channelCredentials == null)
                    channelCredentials = _accessToken != null ? new SslCredentials() : ChannelCredentials.Insecure;

                return new Clients(new Channel($"{_host}:{_port}", channelCredentials), _expectedLedgerId, _accessToken);
            }

            private Builder(string host, int port, string expectedLedgerId, ChannelCredentials channelCredentials, string accessToken)
            {
                _host = host;
                _port = port;
                _expectedLedgerId = expectedLedgerId;
                _channelCredentials = channelCredentials;
                _accessToken = accessToken;
            }
        }

        private Clients(Channel channel, string expectedLedgerId, string accessToken)
        {
            LedgerIdentityClient = new LedgerIdentityClient(channel, accessToken);

            LedgerId = LedgerIdentityClient.GetLedgerIdentity(accessToken);

            if (!string.IsNullOrEmpty(expectedLedgerId) && LedgerId != expectedLedgerId)
                throw new LedgerIdMismatchException(expectedLedgerId, LedgerId);

            ActiveContractsClient      = new ActiveContractsClient(LedgerId, channel, accessToken);
            CommandClient              = new CommandClient(LedgerId, channel, accessToken);
            CommandCompletionClient    = new CommandCompletionClient(LedgerId, channel, accessToken);
            CommandSubmissionClient    = new CommandSubmissionClient(LedgerId, channel, accessToken);
            LedgerConfigurationClient  = new LedgerConfigurationClient(LedgerId, channel, accessToken);
            PackageClient              = new PackageClient(LedgerId, channel, accessToken);
            TransactionsClient         = new TransactionsClient(LedgerId, channel, accessToken);
            Admin                      = new AdminClients(channel, accessToken);
            Testing                    = new TestingClients(LedgerId, channel, accessToken);
        }

        public class AdminClients
        {
            public IConfigManagementClient ConfigManagementClient { get; }
            public IPackageManagementClient PackageManagementClient { get; }
            public IPartyManagementClient PartyManagementClient { get; }

            public AdminClients(Channel channel, string accessToken)
            {
                ConfigManagementClient = new ConfigManagementClient(channel, accessToken);
                PackageManagementClient = new PackageManagementClient(channel, accessToken);
                PartyManagementClient = new PartyManagementClient(channel, accessToken);
            }
        }

        public class TestingClients
        {
            public ITimeClient TimeClient { get; }
            public IResetClient ResetClient { get; }

            public TestingClients(string ledgerId, Channel channel, string accessToken)
            {
                TimeClient = new TimeClient(ledgerId, channel, accessToken);
                ResetClient = new ResetClient(ledgerId, channel, accessToken);
            }
        }
        
        private class LedgerIdMismatchException : Exception
        {
            public LedgerIdMismatchException(string expectedLedgerId, string ledgerId)
                : base($"Configured ledger id {expectedLedgerId} is not the same as reported by the ledger {ledgerId}")
            {
            }
        }
    }
}
