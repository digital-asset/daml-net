// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

#pragma warning disable CS1701 // Assuming assembly reference matches identity

namespace Daml.Ledger.Examples.Test
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1;
    using Daml.Ledger.Client;
    using Grpc.Core;
    using static Com.DigitalAsset.Ledger.Api.V1.LedgerOffset.Types;

    internal class Program
    {
        private readonly Clients _clients;

        // Arguments: host port packageId party party2
        private static void Main(string[] args)
        {
            var p = new Program(args[0], int.Parse(args[1]));
            p.Run(args[2], args[3], args[4]).Wait();
        }

        internal Program(string host, int port)
        {
            _clients = new Clients.Builder(host, port).WithChannelCredentials(ChannelCredentials.Insecure).Build();
        }

        private async Task Run(string packageId, string party, string party2)
        {
            Console.WriteLine($"Ledger identity: {_clients.LedgerId}");

            await PrintParticipantId();
            await PrintKnownPackages();
            await PrintKnownParties();
            await PrintActiveContracts(party);
            await PrintTransactions(party);

            var contractId = await CreateContract(packageId, party);
            await ExerciseChoice(packageId, party, party2, contractId);
        }
        
        private async Task PrintParticipantId()
        {
            var participantId = await _clients.Admin.PartyManagementClient.GetParticipantIdAsync();
            Console.WriteLine($"Participant identitiy: {participantId}");
        }

        private async Task PrintKnownPackages()
        {
            var packageDetails = await _clients.Admin.PackageManagementClient.ListKnownPackagesAsync();
            foreach (var p in packageDetails) Console.WriteLine($"Package detail: {p}");
        }

        private async Task PrintKnownParties()
        {
            var partyDetails = await _clients.Admin.PartyManagementClient.ListKnownPartiesAsync();
            foreach (var p in partyDetails) Console.WriteLine($"Party detail: {p}");
        }

        private async Task PrintTransactions(string party)
        {
            var transactionFilter = new TransactionFilter();
            transactionFilter.FiltersByParty.Add(party, new Filters());

            var beginOffset = new LedgerOffset { Boundary = LedgerBoundary.LedgerBegin };
            var endOffset = new LedgerOffset { Boundary = LedgerBoundary.LedgerEnd };

            using (var stream = _clients.TransactionsClient.GetTransactions(transactionFilter, beginOffset, endOffset))
            {
                while (await stream.MoveNext(CancellationToken.None))
                {
                    foreach (var tx in stream.Current.Transactions) Console.WriteLine($"Transaction: {tx.ToString()}");
                }
            }
        }

        private async Task PrintActiveContracts(string party)
        {
            var transactionFilter = new TransactionFilter();
            transactionFilter.FiltersByParty.Add(party, new Filters());

            using (var stream = _clients.ActiveContractsClient.GetActiveContracts(transactionFilter))
            {
                while (await stream.MoveNext(CancellationToken.None))
                {
                    foreach (var c in stream.Current.ActiveContracts) Console.WriteLine($"ActiveContract: {c.ToString()}");
                }
            }
        }

        private async Task<string> CreateContract(string packageId, string party)
        {
            var ledgerEffectiveTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var maximumRecordTime = ledgerEffectiveTime.AddSeconds(5);

            var commandId = Guid.NewGuid().ToString();

            var templateId = new Identifier { PackageId = packageId, ModuleName = "Main", EntityName = "Asset" };

            var createArgs = new Record();
            createArgs.Fields.Add(new RecordField { Label = "issuer", Value = new Value { Party = party } });
            createArgs.Fields.Add(new RecordField { Label = "owner", Value = new Value { Party = party } });
            createArgs.Fields.Add(new RecordField { Label = "name", Value = new Value { Text = "TV" } });

            var command = new Command { Create = new CreateCommand { TemplateId = templateId, CreateArguments = createArgs } };
            Console.WriteLine($"Command: {command.ToString()}");

            var tx = await _clients.CommandClient.SubmitAndWaitForTransactionAsync("myApplicationId", "myWorkflowId", commandId, party, ledgerEffectiveTime, maximumRecordTime, new[] { command });
            Console.WriteLine($"Transaction: {tx.ToString()}");

            if (tx.Events.Count != 1)
                throw new ApplicationException("Expecting one event");

            if (tx.Events.Single().EventCase != Event.EventOneofCase.Created)
                throw new ApplicationException("Expecting one created event");
 
            return tx.Events.Single().Created.ContractId;
        }

        private async Task ExerciseChoice(string packageId, string party, string party2, string contractId)
        {
            var ledgerEffectiveTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var maximumRecordTime = ledgerEffectiveTime.AddSeconds(5);

            var commandId = Guid.NewGuid().ToString();

            var templateId = new Identifier { PackageId = packageId, ModuleName = "Main", EntityName = "Asset" };

            var choiceRecord = new Record();
            choiceRecord.Fields.Add(new RecordField { Label = "newOwner", Value = new Value { Party = party2 } });
            var choiceArgs = new Value { Record = choiceRecord };

            var command = new Command { Exercise = new ExerciseCommand { TemplateId = templateId, ContractId = contractId, Choice = "Give", ChoiceArgument = choiceArgs } };
            Console.WriteLine($"Command: {command.ToString()}");

            var tx = await _clients.CommandClient.SubmitAndWaitForTransactionAsync("myApplicationId", "myWorkflowId", commandId, party, ledgerEffectiveTime, maximumRecordTime, new[] { command });
            Console.WriteLine($"Transaction: {tx.ToString()}");
        }
    }
}
