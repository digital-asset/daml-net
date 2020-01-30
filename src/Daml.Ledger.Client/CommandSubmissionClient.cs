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
        private readonly string _ledgerId;
        private readonly CommandSubmissionService.CommandSubmissionServiceClient _commandSubmissionClient;

        public CommandSubmissionClient(string ledgerId, Channel channel)
        {
            _ledgerId = ledgerId;
            _commandSubmissionClient = new CommandSubmissionService.CommandSubmissionServiceClient(channel);
        }

        public void Submit(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands)
        {
            Submit(BuildCommands(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands));
        }

        public async Task SubmitAsync(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands)
        {
            await SubmitAsync(BuildCommands(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands));
        }

        public void Submit(Commands commands)
        {
            _commandSubmissionClient.Submit(new SubmitRequest { Commands = commands });
        }

        public async Task SubmitAsync(Commands commands)
        {
            await _commandSubmissionClient.SubmitAsync(new SubmitRequest { Commands = commands });
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
