// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using Admin;
    using Testing;
    using Grpc.Core;

    public class Clients
    {
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
            private readonly string _ledgerId;
            private readonly string _host;
            private readonly int _port;
            private ChannelCredentials _channelCredentials;
            private string _accessToken;

            internal Builder(string ledgerId, string host, int port)
            {
                _ledgerId = ledgerId;
                _host = host;
                _port = port;
            }

            public Builder WithChannelCredentials(ChannelCredentials channelCredentials)
            {
                _channelCredentials = channelCredentials;
                return this;
            }

            public Builder WithAccessToken(string accessToken)
            {
                if (!string.IsNullOrEmpty(accessToken))
                {
                    _accessToken = accessToken;
                    if (_channelCredentials is null)
                        _channelCredentials = new SslCredentials();
                }
                return this;
            }

            public Clients Build()
            {
                ChannelCredentials channelCredentials = _channelCredentials;
                    
                if (channelCredentials == null)
                    channelCredentials = _accessToken != null ? new SslCredentials() : ChannelCredentials.Insecure;

                return new Clients(_ledgerId, new Channel($"{_host}:{_port}", channelCredentials), _accessToken);
            }
        }

        public Clients(string ledgerId, Channel channel, string accessToken)
        {
            ActiveContractsClient      = new ActiveContractsClient(ledgerId, channel, accessToken);
            CommandClient              = new CommandClient(ledgerId, channel, accessToken);
            CommandCompletionClient    = new CommandCompletionClient(ledgerId, channel, accessToken);
            CommandSubmissionClient    = new CommandSubmissionClient(ledgerId, channel, accessToken);
            LedgerConfigurationClient  = new LedgerConfigurationClient(ledgerId, channel, accessToken);
            LedgerIdentityClient       = new LedgerIdentityClient(channel, accessToken);
            PackageClient              = new PackageClient(ledgerId, channel, accessToken);
            TransactionsClient         = new TransactionsClient(ledgerId, channel, accessToken);
            Admin                      = new AdminClients(channel, accessToken);
            Testing                    = new TestingClients(ledgerId, channel, accessToken);
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
    }
}
