// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Reactive.Concurrency;

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
        private readonly Clients _clients;

        public string LedgerId => _clients.LedgerId;
    
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
        /// Constructor from a Daml.Ledger.Client.Client that we will provide a reactive interface to
        /// </summary>
        /// <param name="client"></param>
        public DamlLedgerClient(Clients clients, IScheduler scheduler = null)
        {
            _clients = clients;

            ActiveContractsClient = new ActiveContractsClient(_clients.ActiveContractsClient, scheduler);
            TransactionClient = new TransactionsClient(_clients.TransactionsClient, scheduler);
            CommandCompletionClient = new CommandCompletionClient(_clients.CommandCompletionClient, scheduler);
            CommandSubmissionClient = new CommandSubmissionClient(_clients.CommandSubmissionClient);
            LedgerIdentityClient = new LedgerIdentityClient(_clients.LedgerIdentityClient);
            CommandClient = new CommandClient(_clients.CommandClient);
            PackageClient = new PackageClient(_clients.PackageClient);
            LedgerConfigurationClient = new LedgerConfigurationClient(_clients.LedgerConfigurationClient, scheduler);

            AdminClients = new AdminClientsImpl(_clients);

            TestingClients = new TestingClientsImpl(_clients);
        }

        public class AdminClientsImpl : IAdminClients
        {
            public AdminClientsImpl(Clients clients)
            {
                ConfigManagementClient = new Admin.ConfigManagementClient(clients.Admin.ConfigManagementClient);
                PackageManagementClient = new Admin.PackageManagementClient(clients.Admin.PackageManagementClient);
                PartyManagementClient = new Admin.PartyManagementClient(clients.Admin.PartyManagementClient);
            }

            public Admin.ConfigManagementClient ConfigManagementClient { get; }
            public Admin.PackageManagementClient PackageManagementClient { get; }
            public Admin.PartyManagementClient PartyManagementClient { get; }
        }

        private class TestingClientsImpl : ITestingClients
        {
            public TestingClientsImpl(Clients clients)
            {
                TimeClient = new Testing.TimeClient(clients.Testing.TimeClient);
                ResetClient = new Testing.ResetClient(clients.Testing.ResetClient);
            }

            public Testing.TimeClient TimeClient { get; }
            public Testing.ResetClient ResetClient { get; }
        }

        /// <summary>
        /// Shutdown the connection
        /// </summary>
        public void Close()
        {
            _clients.Close();
        }        
    }
}
