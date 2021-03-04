// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Reactive
{
    public interface IAdminClients
    {
        Admin.ConfigManagementClient ConfigManagementClient { get; }
        Admin.PackageManagementClient PackageManagementClient { get; }
        Admin.PartyManagementClient PartyManagementClient { get; }
    }

    public interface ITestingClients
    {
        Testing.TimeClient TimeClient { get; }
        Testing.ResetClient ResetClient { get; }
    }

    /// <summary>
    /// Contains the set of services provided by a Ledger implementation
    /// </summary>
    public interface ILedgerClient
    {
        // return The identifier of the Ledger connected to this LedgerClient
        string LedgerId { get; }

        ActiveContractsClient ActiveContractsClient { get; }

        TransactionsClient TransactionClient { get; }

        CommandClient CommandClient { get; }

        CommandCompletionClient CommandCompletionClient { get; }

        CommandSubmissionClient CommandSubmissionClient { get; }

        LedgerIdentityClient LedgerIdentityClient { get; }

        PackageClient PackageClient { get; }

        LedgerConfigurationClient LedgerConfigurationClient { get; }

        ITestingClients TestingClients { get; }

        IAdminClients AdminClients { get; }
    }
}
