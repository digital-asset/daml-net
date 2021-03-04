// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Grpc.Core;

namespace Daml.Ledger.Examples.Bindings.Grpc
{
    using Com.Daml.Ledger.Api.V1;

    /// This class subscribes to the stream of transactions for a given party and reacts to Ping or Pong contracts.
    public class PingPongProcessor 
    {
        private readonly string _party;
        private readonly string _ledgerId;
        private readonly TransactionService.TransactionServiceClient _transactionService;
        private readonly CommandSubmissionService.CommandSubmissionServiceClient _submissionService;
        private readonly Identifier _pingIdentifier;
        private readonly Identifier _pongIdentifier;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public PingPongProcessor(string party, string ledgerId, Channel channel, Identifier pingIdentifier, Identifier pongIdentifier) 
        {
            _party = party;
            _ledgerId = ledgerId;
            _transactionService = new TransactionService.TransactionServiceClient(channel);
            _submissionService = new CommandSubmissionService.CommandSubmissionServiceClient(channel);
            _pingIdentifier = pingIdentifier;
            _pongIdentifier = pongIdentifier;
        }

        public void RunIndefinitely() 
        {
            var filter = new TransactionFilter();
            filter.FiltersByParty.Add(_party, new Filters());            // we use the default filter since we don't want to filter out any contracts

            // assemble the request for the transaction stream
            GetTransactionsRequest transactionsRequest = new GetTransactionsRequest { LedgerId = _ledgerId, 
                                                                                      Begin = new LedgerOffset { Boundary = LedgerOffset.Types.LedgerBoundary.LedgerBegin },
                                                                                      Filter = filter,
                                                                                      Verbose = true };

            // this StreamObserver reacts to transactions and prints a message if an error occurs or the stream gets closed

            Console.WriteLine($"{_party} starts reading transactions.");

            AsyncServerStreamingCall<GetTransactionsResponse> transactionObserver = _transactionService.GetTransactions(transactionsRequest, null, null, _cancellationTokenSource.Token);

            Task.Run(async() => 
            {
                try
                {
                    while (await transactionObserver.ResponseStream.MoveNext(CancellationToken.None))
                    {
                        foreach (var transaction in transactionObserver.ResponseStream.Current.Transactions)
                            ProcessTransaction(transaction);
                    }

                    Console.WriteLine($"{_party}'s transactions stream completed.");
                }
                catch (RpcException ex)
                {
                    if (ex.StatusCode != StatusCode.Cancelled)
                        Console.Error.WriteLine($"{_party} encountered an RpcException while processing transactions! {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"{_party} encountered an error while processing transactions! {ex.Message}");
                }
            });
        }

        public void Shutdown()
        {
            _cancellationTokenSource.Cancel();
        }

        /// <summary>
        /// Processes a transaction and sends the resulting commands to the Command Submission Service
        /// </summary>
        /// <param name="tx">the Transaction to process</param>
        private void ProcessTransaction(Transaction tx) 
        {
            var request = new SubmitRequest { Commands = new Commands { CommandId = Guid.NewGuid().ToString(), 
                                                                        WorkflowId = tx.WorkflowId,
                                                                        LedgerId = _ledgerId,
                                                                        Party = _party,
                                                                        ApplicationId = PingPongGrpcMain.APP_ID }};
           
            foreach (var e in from e in tx.Events where e.EventCase == Event.EventOneofCase.Created select e.Created)
            {
                Command command = ProcessEvent(tx.WorkflowId, e);
                if (command != null)
                    request.Commands.Commands_.Add(command);
            }

            if (request.Commands.Commands_.Count > 0)
                _submissionService.Submit(request);
        }

        /// <summary>
        /// For each CreatedEvent where the receiver is
        /// the current party, exercise the Pong choice of Ping contracts, or the Ping
        /// choice of Pong contracts.
        /// </summary>
        /// <param name="workflowId">the workflow the event is part of</param>
        /// <param name="createdEvent">the CreatedEvent to process</param>
        /// <returns>null if this event doesn't trigger any action for this PingPongProcessor's party</returns>
        private Command ProcessEvent(string workflowId, CreatedEvent createdEvent) 
        {
            Identifier template = createdEvent.TemplateId;

            bool isPingPongModule = template.ModuleName == _pingIdentifier.ModuleName;

            bool isPing = template.EntityName == _pingIdentifier.EntityName;
            bool isPong = template.EntityName == _pongIdentifier.EntityName;

            if (!isPingPongModule || !isPing && !isPong) 
                return null;

            var fields = new Dictionary<string, Value>(from f in createdEvent.CreateArguments.Fields select KeyValuePair.Create(f.Label, f.Value)); 

            // check that this party is set as the receiver of the contract
            bool thisPartyIsReceiver = fields["receiver"].Party == _party;

            if (!thisPartyIsReceiver) 
                return null;

            string contractId = createdEvent.ContractId;
            string choice = isPing ? "RespondPong" : "RespondPing";

            long count = fields["count"].Int64;
            Console.WriteLine($"{_party} is exercising {choice} on {contractId} in workflow {workflowId} at count {count}");

            // assemble the exercise command
            return new Command { Exercise = new ExerciseCommand { TemplateId = template, ContractId = contractId, Choice = choice, ChoiceArgument = new Value { Record = new Record() } } };
        }
    }
}