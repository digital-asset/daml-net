﻿// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Daml.Ledger.Client
{
    using Com.DigitalAsset.Ledger.Api.V1;
    using Daml.Ledger.Client.Auth.Client;

    public class CommandClient : ICommandClient
    {
        private readonly ClientStub<CommandService.CommandServiceClient> _commandClient;

        public CommandClient(string ledgerId, Channel channel, string accessToken)
        {
            LedgerId = ledgerId;
            _commandClient = new ClientStub<CommandService.CommandServiceClient>(new CommandService.CommandServiceClient(channel), accessToken);
        }

        public string LedgerId { get; }

        public void SubmitAndWait(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands,
            string accessToken = null)
        {
            SubmitAndWait(BuildCommands(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands), accessToken);
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

        public void SubmitAndWait(Commands commands, string accessToken = null)
        {
            _commandClient.WithAccess(accessToken).Dispatch(new SubmitAndWaitRequest { Commands = commands }, (c, r, co) => c.SubmitAndWait(r, co));
        }

        public async Task SubmitAndWaitAsync(Commands commands, string accessToken = null)
        {
            await _commandClient.WithAccess(accessToken).Dispatch(new SubmitAndWaitRequest { Commands = commands }, (c, r, co) => c.SubmitAndWaitAsync(r, co));
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
            var response = _commandClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.SubmitAndWaitForTransaction(r, co));
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
            var response = _commandClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.SubmitAndWaitForTransactionId(r, co));
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
            var response = _commandClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.SubmitAndWaitForTransactionTree(r, co));
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
            var response = await _commandClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.SubmitAndWaitForTransactionAsync(r, co));
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
            var response = await _commandClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.SubmitAndWaitForTransactionIdAsync(r, co));
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
            var response = await _commandClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.SubmitAndWaitForTransactionTreeAsync(r, co));
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
                    LedgerId = LedgerId,
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
