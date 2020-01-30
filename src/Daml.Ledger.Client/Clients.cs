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
      
            public Builder(string host, int port)
            {
                _host = host;
                _port = port;
            }

            public Builder WithExpectedLedgerId(string expectedLedgerId)
            {
                return new Builder(_host, _port, expectedLedgerId, _channelCredentials);
            }

            public Builder WithChannelCredentials(ChannelCredentials channelCredentials)
            {
                return new Builder(_host, _port, _expectedLedgerId, channelCredentials);
            }

            public Clients Build()
            {
                ChannelCredentials channelCredentials = _channelCredentials;
                    
                if (channelCredentials == null)
                    channelCredentials = ChannelCredentials.Insecure;

                return new Clients(new Channel($"{_host}:{_port}", channelCredentials), _expectedLedgerId);
            }

            private Builder(string host, int port, string expectedLedgerId, ChannelCredentials channelCredentials)
            {
                _host = host;
                _port = port;
                _expectedLedgerId = expectedLedgerId;
                _channelCredentials = channelCredentials;
            }
        }

        private Clients(Channel channel, string expectedLedgerId = null)
        {
            LedgerIdentityClient       = new LedgerIdentityClient(channel);

            LedgerId = LedgerIdentityClient.GetLedgerIdentity();

            if (!string.IsNullOrEmpty(expectedLedgerId) && LedgerId != expectedLedgerId)
                throw new LedgerIdMismatchException(expectedLedgerId, LedgerId);
            
            ActiveContractsClient      = new ActiveContractsClient(LedgerId, channel);
            CommandClient              = new CommandClient(LedgerId, channel);
            CommandCompletionClient    = new CommandCompletionClient(LedgerId, channel);
            CommandSubmissionClient    = new CommandSubmissionClient(LedgerId, channel);
            LedgerConfigurationClient  = new LedgerConfigurationClient(LedgerId, channel);
            PackageClient              = new PackageClient(LedgerId, channel);
            TransactionsClient         = new TransactionsClient(LedgerId, channel);
            Admin                      = new AdminClients(channel);
            Testing                    = new TestingClients(LedgerId, channel);
        }

        public class AdminClients
        {
            public IConfigManagementClient ConfigManagementClient { get; }
            public IPackageManagementClient PackageManagementClient { get; }
            public IPartyManagementClient PartyManagementClient { get; }

            public AdminClients(Channel channel)
            {
                ConfigManagementClient = new ConfigManagementClient(channel);
                PackageManagementClient = new PackageManagementClient(channel);
                PartyManagementClient = new PartyManagementClient(channel);
            }
        }

        public class TestingClients
        {
            public ITimeClient TimeClient { get; }
            public IResetClient ResetClient { get; }

            public TestingClients(string ledgerId, Channel channel)
            {
                TimeClient = new TimeClient(ledgerId, channel);
                ResetClient = new ResetClient(ledgerId, channel);
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
