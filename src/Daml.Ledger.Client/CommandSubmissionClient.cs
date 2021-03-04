// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
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
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands,
            string accessToken = null)
        {
            Submit(BuildCommands(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands), accessToken);
        }

        public async Task SubmitAsync(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands,
            string accessToken = null)
        {
            await SubmitAsync(BuildCommands(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands), accessToken);
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
