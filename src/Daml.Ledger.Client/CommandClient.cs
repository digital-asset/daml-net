// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1;
    using Google.Protobuf.WellKnownTypes;
    using Grpc.Core;

    public class CommandClient : ICommandClient
    {
        private readonly string _ledgerId;
        private readonly CommandService.CommandServiceClient _commandClient;

        public CommandClient(string ledgerId, Channel channel)
        {
            _ledgerId = ledgerId;
            _commandClient = new CommandService.CommandServiceClient(channel);
        }

        public void SubmitAndWait(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands)
        {
            SubmitAndWait(BuildCommands(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands));
        }

        public async Task SubmitAndWaitAsync(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands)
        {
            await SubmitAndWaitAsync(BuildCommands(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands));
        }

        public void SubmitAndWait(Commands commands)
        {
            _commandClient.SubmitAndWait(new SubmitAndWaitRequest { Commands = commands });
        }

        public async Task SubmitAndWaitAsync(Commands commands)
        {
            await _commandClient.SubmitAndWaitAsync(new SubmitAndWaitRequest { Commands = commands });
        }

        public Transaction SubmitAndWaitForTransaction(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands)
        {
            var request = new SubmitAndWaitRequest { Commands = BuildCommands(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands) };
            var response = _commandClient.SubmitAndWaitForTransaction(request);
            return response.Transaction;
        }

        public string SubmitAndWaitForTransactionId(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands)
        {
            var request = new SubmitAndWaitRequest { Commands = BuildCommands(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands) };
            var response = _commandClient.SubmitAndWaitForTransactionId(request);
            return response.TransactionId;
        }

        public TransactionTree SubmitAndWaitForTransactionTree(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands)
        {
            var request = new SubmitAndWaitRequest { Commands = BuildCommands(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands) };
            var response = _commandClient.SubmitAndWaitForTransactionTree(request);
            return response.Transaction;
        }

        public async Task<Transaction> SubmitAndWaitForTransactionAsync(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands)
        {
            var request = new SubmitAndWaitRequest { Commands = BuildCommands(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands) };
            var response = await _commandClient.SubmitAndWaitForTransactionAsync(request);
            return response.Transaction;
        }

        public async Task<string> SubmitAndWaitForTransactionIdAsync(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands)
        {
            var request = new SubmitAndWaitRequest { Commands = BuildCommands(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands) };
            var response = await _commandClient.SubmitAndWaitForTransactionIdAsync(request);
            return response.TransactionId;
        }

        public async Task<TransactionTree> SubmitAndWaitForTransactionTreeAsync(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands)
        {
            var request = new SubmitAndWaitRequest { Commands = BuildCommands(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands) };
            var response = await _commandClient.SubmitAndWaitForTransactionTreeAsync(request);
            return response.Transaction;
        }

        private Commands BuildCommands(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands)
        {
            var cmds = new Commands
                {
                    LedgerId = _ledgerId,
                    ApplicationId = applicationId,
                    WorkflowId = workflowId,
                    CommandId = commandId,
                    Party = party,
                    LedgerEffectiveTime = Timestamp.FromDateTime(ledgerEffectiveTime),
                    MaximumRecordTime = Timestamp.FromDateTime(maximumRecordTime)
                };
            cmds.Commands_.AddRange(commands);
            return cmds;
        }
    }
}
