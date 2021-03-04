// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;

namespace Daml.Ledger.Client
{
    using Com.Daml.Ledger.Api.V1;
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
            DateTimeOffset? minLedgerTimeAbs,
            TimeSpan? minLedgerTimeRel,
            TimeSpan? deduplicationTime,
            IEnumerable<Command> commands,
            string accessToken = null)
        {
            SubmitAndWait(BuildCommands(applicationId, workflowId, commandId, party, minLedgerTimeAbs, minLedgerTimeRel, deduplicationTime, commands), accessToken);
        }

        public void SubmitAndWait(string applicationId, string workflowId, string commandId, IEnumerable<string> actAs, IEnumerable<string> readAs, DateTimeOffset? minLedgerTimeAbs, TimeSpan? minLedgerTimeRel, TimeSpan? deduplicationTime, IEnumerable<Command> commands, string accessToken = null)
        {
            SubmitAndWait(BuildCommands(applicationId, workflowId, commandId, actAs, readAs, minLedgerTimeAbs, minLedgerTimeRel, deduplicationTime, commands), accessToken);
        }

        public async Task SubmitAndWaitAsync(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTimeOffset? minLedgerTimeAbs,
            TimeSpan? minLedgerTimeRel,
            TimeSpan? deduplicationTime,
            IEnumerable<Command> commands,
            string accessToken = null)
        {
            await SubmitAndWaitAsync(BuildCommands(applicationId, workflowId, commandId, party, minLedgerTimeAbs, minLedgerTimeRel, deduplicationTime, commands), accessToken);
        }

        public async Task SubmitAndWaitAsync(string applicationId, string workflowId, string commandId, IEnumerable<string> actAs, IEnumerable<string> readAs, DateTimeOffset? minLedgerTimeAbs, TimeSpan? minLedgerTimeRel, TimeSpan? deduplicationTime, IEnumerable<Command> commands, string accessToken = null)
        {
            await SubmitAndWaitAsync(BuildCommands(applicationId, workflowId, commandId, actAs, readAs, minLedgerTimeAbs, minLedgerTimeRel, deduplicationTime, commands), accessToken);
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
            DateTimeOffset? minLedgerTimeAbs,
            TimeSpan? minLedgerTimeRel,
            TimeSpan? deduplicationTime,
            IEnumerable<Command> commands, 
            string accessToken = null)
        {
            var request = new SubmitAndWaitRequest { Commands = BuildCommands(applicationId, workflowId, commandId, party, minLedgerTimeAbs, minLedgerTimeRel, deduplicationTime, commands) };
            return _commandClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.SubmitAndWaitForTransaction(r, co)).Transaction;
        }

        public Transaction SubmitAndWaitForTransaction(string applicationId, string workflowId, string commandId, IEnumerable<string> actAs, IEnumerable<string> readAs, DateTimeOffset? minLedgerTimeAbs, TimeSpan? minLedgerTimeRel, TimeSpan? deduplicationTime, IEnumerable<Command> commands, string accessToken = null)
        {
            var request = new SubmitAndWaitRequest { Commands = BuildCommands(applicationId, workflowId, commandId, actAs, readAs, minLedgerTimeAbs, minLedgerTimeRel, deduplicationTime, commands) };
            return _commandClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.SubmitAndWaitForTransaction(r, co)).Transaction;
        }

        public string SubmitAndWaitForTransactionId(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTimeOffset? minLedgerTimeAbs,
            TimeSpan? minLedgerTimeRel,
            TimeSpan? deduplicationTime,
            IEnumerable<Command> commands, 
            string accessToken = null)
        {
            var request = new SubmitAndWaitRequest { Commands = BuildCommands(applicationId, workflowId, commandId, party, minLedgerTimeAbs, minLedgerTimeRel, deduplicationTime, commands) };
            return _commandClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.SubmitAndWaitForTransactionId(r, co)).TransactionId;
        }

        public string SubmitAndWaitForTransactionId(string applicationId, string workflowId, string commandId, IEnumerable<string> actAs, IEnumerable<string> readAs, DateTimeOffset? minLedgerTimeAbs, TimeSpan? minLedgerTimeRel, TimeSpan? deduplicationTime, IEnumerable<Command> commands, string accessToken = null)
        {
            var request = new SubmitAndWaitRequest { Commands = BuildCommands(applicationId, workflowId, commandId, actAs, readAs, minLedgerTimeAbs, minLedgerTimeRel, deduplicationTime, commands) };
            return _commandClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.SubmitAndWaitForTransactionId(r, co)).TransactionId;
        }

        public TransactionTree SubmitAndWaitForTransactionTree(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTimeOffset? minLedgerTimeAbs,
            TimeSpan? minLedgerTimeRel,
            TimeSpan? deduplicationTime,
            IEnumerable<Command> commands,
            string accessToken = null)
        {
            var request = new SubmitAndWaitRequest { Commands = BuildCommands(applicationId, workflowId, commandId, party, minLedgerTimeAbs, minLedgerTimeRel, deduplicationTime, commands) };
            return _commandClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.SubmitAndWaitForTransactionTree(r, co)).Transaction;
        }

        public TransactionTree SubmitAndWaitForTransactionTree(string applicationId, string workflowId, string commandId, IEnumerable<string> actAs, IEnumerable<string> readAs, DateTimeOffset? minLedgerTimeAbs, TimeSpan? minLedgerTimeRel, TimeSpan? deduplicationTime, IEnumerable<Command> commands, string accessToken = null)
        {
            var request = new SubmitAndWaitRequest { Commands = BuildCommands(applicationId, workflowId, commandId, actAs, readAs, minLedgerTimeAbs, minLedgerTimeRel, deduplicationTime, commands) };
            return _commandClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.SubmitAndWaitForTransactionTree(r, co)).Transaction;
        }

        public async Task<Transaction> SubmitAndWaitForTransactionAsync(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTimeOffset? minLedgerTimeAbs,
            TimeSpan? minLedgerTimeRel,
            TimeSpan? deduplicationTime,
            IEnumerable<Command> commands,
            string accessToken = null)
        {
            var request = new SubmitAndWaitRequest { Commands = BuildCommands(applicationId, workflowId, commandId, party, minLedgerTimeAbs, minLedgerTimeRel, deduplicationTime, commands) };
            return (await _commandClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.SubmitAndWaitForTransactionAsync(r, co))).Transaction;
        }

        public async Task<string> SubmitAndWaitForTransactionIdAsync(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTimeOffset? minLedgerTimeAbs,
            TimeSpan? minLedgerTimeRel,
            TimeSpan? deduplicationTime,
            IEnumerable<Command> commands,
            string accessToken = null)
        {
            var request = new SubmitAndWaitRequest { Commands = BuildCommands(applicationId, workflowId, commandId, party, minLedgerTimeAbs, minLedgerTimeRel, deduplicationTime, commands) };
            return (await _commandClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.SubmitAndWaitForTransactionIdAsync(r, co))).TransactionId;
        }

        public async Task<string> SubmitAndWaitForTransactionIdAsync(string applicationId, string workflowId, string commandId, IEnumerable<string> actAs, IEnumerable<string> readAs, DateTimeOffset? minLedgerTimeAbs, TimeSpan? minLedgerTimeRel, TimeSpan? deduplicationTime, IEnumerable<Command> commands, string accessToken = null)
        {
            var request = new SubmitAndWaitRequest { Commands = BuildCommands(applicationId, workflowId, commandId, actAs, readAs, minLedgerTimeAbs, minLedgerTimeRel, deduplicationTime, commands) };
            return (await _commandClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.SubmitAndWaitForTransactionIdAsync(r, co))).TransactionId;
        }

        public async Task<TransactionTree> SubmitAndWaitForTransactionTreeAsync(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTimeOffset? minLedgerTimeAbs,
            TimeSpan? minLedgerTimeRel,
            TimeSpan? deduplicationTime,
            IEnumerable<Command> commands,
            string accessToken = null)
        {
            var request = new SubmitAndWaitRequest { Commands = BuildCommands(applicationId, workflowId, commandId, party, minLedgerTimeAbs, minLedgerTimeRel, deduplicationTime, commands) };
            return (await _commandClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.SubmitAndWaitForTransactionTreeAsync(r, co))).Transaction;
        }

        public async Task<TransactionTree> SubmitAndWaitForTransactionTreeAsync(string applicationId, string workflowId, string commandId, IEnumerable<string> actAs, IEnumerable<string> readAs, DateTimeOffset? minLedgerTimeAbs, TimeSpan? minLedgerTimeRel, TimeSpan? deduplicationTime, IEnumerable<Command> commands, string accessToken = null)
        {
            var request = new SubmitAndWaitRequest { Commands = BuildCommands(applicationId, workflowId, commandId, actAs, readAs, minLedgerTimeAbs, minLedgerTimeRel, deduplicationTime, commands) };
            return (await _commandClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.SubmitAndWaitForTransactionTreeAsync(r, co))).Transaction;
        }

        private Commands BuildCommands(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTimeOffset? minLedgerTimeAbs,
            TimeSpan? minLedgerTimeRel,
            TimeSpan? deduplicationTime,
            IEnumerable<Command> commands)
        {
            return BuildCommands(applicationId, workflowId, commandId, new List<string> { party }, new List<string>(), minLedgerTimeAbs, minLedgerTimeRel, deduplicationTime, commands);
        }

        private Commands BuildCommands(
            string applicationId,
            string workflowId,
            string commandId,
            IEnumerable<string> actAs,
            IEnumerable<string> readAs,
            DateTimeOffset? minLedgerTimeAbs,
            TimeSpan? minLedgerTimeRel,
            TimeSpan? deduplicationTime,
            IEnumerable<Command> commands)
        {
            var cmds = new Commands
            {
                LedgerId = LedgerId,
                ApplicationId = applicationId,
                WorkflowId = workflowId,
                CommandId = commandId
            };

            cmds.ActAs.AddRange(actAs);
            cmds.ReadAs.AddRange(readAs);
            cmds.Party = cmds.ActAs[0];

            if (minLedgerTimeAbs.HasValue)
                cmds.MinLedgerTimeAbs = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(minLedgerTimeAbs.Value);

            if (minLedgerTimeRel.HasValue)
                cmds.MinLedgerTimeRel = Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(minLedgerTimeRel.Value);

            if (deduplicationTime.HasValue)
                cmds.DeduplicationTime = Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(deduplicationTime.Value);

            cmds.Commands_.AddRange(commands);
            return cmds;
        }
    }
}
