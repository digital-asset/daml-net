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
        private readonly CommandService.CommandServiceClient commandClient;

        public CommandClient(Channel channel)
        {
            this.commandClient = new CommandService.CommandServiceClient(channel);
        }

        public void SubmitAndWait(
            string ledgerId,
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands)
        {
            var cmds = this.BuildCommands(ledgerId, applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands);
            var request = new SubmitAndWaitRequest { Commands = cmds };
            this.commandClient.SubmitAndWait(request);
        }

        public async Task SubmitAndWaitAsync(
            string ledgerId,
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands)
        {
            var cmds = this.BuildCommands(ledgerId, applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands);
            var request = new SubmitAndWaitRequest { Commands = cmds };
            await this.commandClient.SubmitAndWaitAsync(request);
        }

        public void SubmitAndWait(Commands commands)
        {
            var request = new SubmitAndWaitRequest { Commands = commands };
            this.commandClient.SubmitAndWait(request);
        }

        public async Task SubmitAndWaitAsync(Commands commands)
        {
            var request = new SubmitAndWaitRequest { Commands = commands };
            await this.commandClient.SubmitAndWaitAsync(request);
        }

        public Transaction SubmitAndWaitForTransaction(
            string ledgerId,
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands)
        {
            var cmds = this.BuildCommands(ledgerId, applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands);
            var request = new SubmitAndWaitRequest { Commands = cmds };
            var response = this.commandClient.SubmitAndWaitForTransaction(request);
            return response.Transaction;
        }

        public string SubmitAndWaitForTransactionId(
            string ledgerId,
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands)
        {
            var cmds = this.BuildCommands(ledgerId, applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands);
            var request = new SubmitAndWaitRequest { Commands = cmds };
            var response = this.commandClient.SubmitAndWaitForTransactionId(request);
            return response.TransactionId;
        }

        public TransactionTree SubmitAndWaitForTransactionTree(
            string ledgerId,
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands)
        {
            var cmds = this.BuildCommands(ledgerId, applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands);
            var request = new SubmitAndWaitRequest { Commands = cmds };
            var response = this.commandClient.SubmitAndWaitForTransactionTree(request);
            return response.Transaction;
        }

        public async Task<Transaction> SubmitAndWaitForTransactionAsync(
            string ledgerId,
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands)
        {
            var cmds = this.BuildCommands(ledgerId, applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands);
            var request = new SubmitAndWaitRequest { Commands = cmds };
            var response = await this.commandClient.SubmitAndWaitForTransactionAsync(request);
            return response.Transaction;
        }

        public async Task<string> SubmitAndWaitForTransactionIdAsync(
            string ledgerId,
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands)
        {
            var cmds = this.BuildCommands(ledgerId, applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands);
            var request = new SubmitAndWaitRequest { Commands = cmds };
            var response = await this.commandClient.SubmitAndWaitForTransactionIdAsync(request);
            return response.TransactionId;
        }

        public async Task<TransactionTree> SubmitAndWaitForTransactionTreeAsync(
            string ledgerId,
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands)
        {
            var cmds = this.BuildCommands(ledgerId, applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands);
            var request = new SubmitAndWaitRequest { Commands = cmds };
            var response = await this.commandClient.SubmitAndWaitForTransactionTreeAsync(request);
            return response.Transaction;
        }

        private Commands BuildCommands(
            string ledgerId,
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
                    LedgerId = ledgerId,
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
