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

    public class CommandSubmissionClient : ICommandSubmissionClient
    {
        private readonly CommandSubmissionService.CommandSubmissionServiceClient commandSubmissionClient;

        public CommandSubmissionClient(Channel channel)
        {
            this.commandSubmissionClient = new CommandSubmissionService.CommandSubmissionServiceClient(channel);
        }

        public void Submit(
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
            var request = new SubmitRequest { Commands = cmds };
            this.commandSubmissionClient.Submit(request);
        }

        public async Task SubmitAsync(
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
            var request = new SubmitRequest { Commands = cmds };
            await this.commandSubmissionClient.SubmitAsync(request);
        }

        public void Submit(Commands commands)
        {
            var request = new SubmitRequest { Commands = commands };
            this.commandSubmissionClient.Submit(request);
        }

        public async Task SubmitAsync(Commands commands)
        {
            var request = new SubmitRequest { Commands = commands };
            await this.commandSubmissionClient.SubmitAsync(request);
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
