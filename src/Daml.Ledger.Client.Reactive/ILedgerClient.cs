// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Reactive
{
    public interface IAdminClients
    {
        Daml.Ledger.Client.Reactive.Admin.ConfigManagementClient ConfigManagementClient { get; }
        Daml.Ledger.Client.Reactive.Admin.PackageManagementClient PackageManagementClient { get; }
        Daml.Ledger.Client.Reactive.Admin.PartyManagementClient PartyManagementClient { get; }
    }

    public interface ITestingClients
    {
        Daml.Ledger.Client.Reactive.Testing.TimeClient TimeClient { get; }
        Daml.Ledger.Client.Reactive.Testing.ResetClient ResetClient { get; }
    }

    /// <summary>
    /// Contains the set of services provided by a Ledger implementation
    /// </summary>
    public interface ILedgerClient
    {
        // return The identifier of the Ledger connected to this LedgerClient
        string LedgerId { get; }

        Daml.Ledger.Client.Reactive.ActiveContractsClient ActiveContractsClient { get; }

        Daml.Ledger.Client.Reactive.TransactionsClient TransactionClient { get; }

        Daml.Ledger.Client.Reactive.CommandClient CommandClient { get; }

        Daml.Ledger.Client.Reactive.CommandCompletionClient CommandCompletionClient { get; }

        Daml.Ledger.Client.Reactive.CommandSubmissionClient CommandSubmissionClient { get; }

        Daml.Ledger.Client.Reactive.LedgerIdentityClient LedgerIdentityClient { get; }

        Daml.Ledger.Client.Reactive.PackageClient PackageClient { get; }

        Daml.Ledger.Client.Reactive.LedgerConfigurationClient LedgerConfigurationClient { get; }

        ITestingClients TestingClients { get; }

        IAdminClients AdminClients { get; }
    }
}
