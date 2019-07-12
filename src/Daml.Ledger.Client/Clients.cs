// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using Grpc.Core;

    public class Clients
    {
        public IActiveContractsClient       ActiveContractsClient       { get; private set; }
        public ICommandClient               CommandClient               { get; private set; }
        public ICommandCompletionClient     CommandCompletionClient     { get; private set; }
        public ICommandSubmissionClient     CommandSubmissionClient { get; private set; }
        public ILedgerConfigurationClient   LedgerConfigurationClient   { get; private set; }
        public ILedgerIdentityClient        LedgerIdentityClient        { get; private set; }
        public IPackageClient               PackageClient               { get; private set; }
        public ITransactionsClient          TransactionsClient          { get; private set; }

        public Clients(Channel channel)
        {
            this.ActiveContractsClient      = new ActiveContractsClient(channel);
            this.CommandClient              = new CommandClient(channel);
            this.CommandCompletionClient    = new CommandCompletionClient(channel);
            this.CommandSubmissionClient    = new CommandSubmissionClient(channel);
            this.LedgerConfigurationClient  = new LedgerConfigurationClient(channel);
            this.LedgerIdentityClient       = new LedgerIdentityClient(channel);
            this.PackageClient              = new PackageClient(channel);
            this.TransactionsClient         = new TransactionsClient(channel);
        }
    }
}
