// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Daml.Ledger.Client
{
    using Com.Daml.Ledger.Api.V1;
    using Daml.Ledger.Client.Auth.Client;

    public class CommandSubmissionClient : ICommandSubmissionClient
    {
        private readonly ClientStub<CommandSubmissionService.CommandSubmissionServiceClient> _commandSubmissionClient;

        public CommandSubmissionClient(string ledgerId, Channel channel, string accessToken)
        {
            LedgerId = ledgerId;
            _commandSubmissionClient = new ClientStub<CommandSubmissionService.CommandSubmissionServiceClient>(new CommandSubmissionService.CommandSubmissionServiceClient(channel), accessToken);
        }

        public string LedgerId { get; }

        public void Submit(
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
            Submit(BuildCommands(applicationId, workflowId, commandId, party, minLedgerTimeAbs, minLedgerTimeRel, deduplicationTime, commands), accessToken);
        }

        public void Submit(
            string applicationId,
            string workflowId,
            string commandId,
            IEnumerable<string> actAs,
            IEnumerable<string> readAs,
            DateTimeOffset? minLedgerTimeAbs,
            TimeSpan? minLedgerTimeRel,
            TimeSpan? deduplicationTime,
            IEnumerable<Command> commands,
            string accessToken = null)
        {
            Submit(BuildCommands(applicationId, workflowId, commandId, actAs, readAs, minLedgerTimeAbs, minLedgerTimeRel, deduplicationTime, commands), accessToken);
        }

        public async Task SubmitAsync(
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
            await SubmitAsync(BuildCommands(applicationId, workflowId, commandId, party, minLedgerTimeAbs, minLedgerTimeRel, deduplicationTime, commands), accessToken);
        }

        public async Task SubmitAsync(
            string applicationId,
            string workflowId,
            string commandId,
            IEnumerable<string> actAs,
            IEnumerable<string> readAs,
            DateTimeOffset? minLedgerTimeAbs,
            TimeSpan? minLedgerTimeRel,
            TimeSpan? deduplicationTime,
            IEnumerable<Command> commands,
            string accessToken = null)
        {
            await SubmitAsync(BuildCommands(applicationId, workflowId, commandId, actAs, readAs, minLedgerTimeAbs, minLedgerTimeRel, deduplicationTime, commands), accessToken);
        }

        public void Submit(Commands commands, string accessToken = null)
        {
            _commandSubmissionClient.WithAccess(accessToken).Dispatch(new SubmitRequest { Commands = commands }, (c, r, co) => c.Submit(r, co));
        }

        public async Task SubmitAsync(Commands commands, string accessToken = null)
        {
            await _commandSubmissionClient.WithAccess(accessToken).Dispatch(new SubmitRequest { Commands = commands }, (c, r, co) => c.SubmitAsync(r, co));
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
            return BuildCommands(applicationId, workflowId, commandId, new List<string>() { party }, new List<string>(), minLedgerTimeAbs, minLedgerTimeRel, deduplicationTime, commands);
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
