// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1;
    using Daml.Ledger.Client.Auth.Client;
    using Google.Protobuf.WellKnownTypes;
    using Grpc.Core;

    public class CommandClient : ICommandClient
    {
        private readonly ClientStub<CommandService.CommandServiceClient> _commandClient;
        private readonly string _ledgerId;
        
        public CommandClient(string ledgerId, Channel channel, string accessToken)
        {
            _ledgerId = ledgerId;
            _commandClient = new ClientStub<CommandService.CommandServiceClient>(new CommandService.CommandServiceClient(channel), accessToken);
        }
        
        public Empty SubmitAndWait(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands,
            string accessToken = null)
        {
            return SubmitAndWait(BuildCommands(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands), accessToken);
        }

        public async Task SubmitAndWaitAsync(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands,
            string accessToken = null)
        {
            await SubmitAndWaitAsync(BuildCommands(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands), accessToken);
        }

        public Empty SubmitAndWait(Commands commands, string accessToken = null)
        {
            return _commandClient.WithAccess(accessToken).DispatchRequest(new SubmitAndWaitRequest { Commands = commands }, (c, r, co) => c.SubmitAndWait(r, co), (c, r) => c.SubmitAndWait(r));
        }

        public async Task SubmitAndWaitAsync(Commands commands, string accessToken = null)
        {
            await _commandClient.WithAccess(accessToken).DispatchRequest(new SubmitAndWaitRequest { Commands = commands }, (c, r, co) => c.SubmitAndWaitAsync(r, co), (c, r) => c.SubmitAndWaitAsync(r));
        }

        public Transaction SubmitAndWaitForTransaction(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands, 
            string accessToken = null)
        {
            var request = new SubmitAndWaitRequest { Commands = BuildCommands(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands) };

            var response = _commandClient.WithAccess(accessToken).DispatchRequest(request, (c, r, co) => c.SubmitAndWaitForTransaction(r, co), (c, r) => c.SubmitAndWaitForTransaction(r));
            
            return response.Transaction;
        }

        public string SubmitAndWaitForTransactionId(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands, 
            string accessToken = null)
        {
            var request = new SubmitAndWaitRequest { Commands = BuildCommands(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands) };

            var response = _commandClient.WithAccess(accessToken).DispatchRequest(request, (c, r, co) => c.SubmitAndWaitForTransactionId(r, co), (c, r) => c.SubmitAndWaitForTransactionId(r));

            return response.TransactionId;
        }

        public TransactionTree SubmitAndWaitForTransactionTree(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands, 
            string accessToken = null)
        {
            var request = new SubmitAndWaitRequest { Commands = BuildCommands(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands) };

            var response = _commandClient.WithAccess(accessToken).DispatchRequest(request, (c, r, co) => c.SubmitAndWaitForTransactionTree(r, co), (c, r) => c.SubmitAndWaitForTransactionTree(r));

            return response.Transaction;
        }

        public async Task<Transaction> SubmitAndWaitForTransactionAsync(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands, 
            string accessToken = null)
        {
            var request = new SubmitAndWaitRequest { Commands = BuildCommands(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands) };

            var response = await _commandClient.WithAccess(accessToken).DispatchRequest(request, (c, r, co) => c.SubmitAndWaitForTransactionAsync(r, co), (c, r) => c.SubmitAndWaitForTransactionAsync(r));

            return response.Transaction;
        }

        public async Task<string> SubmitAndWaitForTransactionIdAsync(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands, 
            string accessToken = null)
        {
            var request = new SubmitAndWaitRequest { Commands = BuildCommands(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands) };

            var response = await _commandClient.WithAccess(accessToken).DispatchRequest(request, (c, r, co) => c.SubmitAndWaitForTransactionIdAsync(r, co), (c, r) => c.SubmitAndWaitForTransactionIdAsync(r));

            return response.TransactionId;
        }

        public async Task<TransactionTree> SubmitAndWaitForTransactionTreeAsync(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands, 
            string accessToken = null)
        {
            var request = new SubmitAndWaitRequest { Commands = BuildCommands(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands) };

            var callOptions = LedgerCallCredentials.MakeCallOptions(accessToken);

            var response = await _commandClient.WithAccess(accessToken).DispatchRequest(request, (c, r, co) => c.SubmitAndWaitForTransactionTreeAsync(r, co), (c, r) => c.SubmitAndWaitForTransactionTreeAsync(r));
            
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
