// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using Com.Daml.Ledger.Api.Util;
using Commands = Com.DigitalAsset.Ledger.Api.V1.Commands;

namespace Com.Daml.Ledger.Api.Data 
{
    public class SubmitCommandsRequest
    {
        private readonly int _hashCode;

        public SubmitCommandsRequest(string workflowId, string applicationId, string commandId, string party, 
                                     DateTimeOffset ledgerEffectiveTime, DateTimeOffset maximumRecordTime, IEnumerable<Command> commands)
        {
            WorkflowId = workflowId;
            ApplicationId = applicationId;
            CommandId = commandId;
            Party = party;
            LedgerEffectiveTime = ledgerEffectiveTime;
            MaximumRecordTime = maximumRecordTime;
            Commands = commands.ToList().AsReadOnly();

            _hashCode = new HashCodeHelper().Add(WorkflowId).Add(ApplicationId).Add(CommandId).Add(Party).Add(LedgerEffectiveTime).Add(MaximumRecordTime).AddRange(Commands).ToHashCode();
        }

        public static SubmitCommandsRequest FromProto(DigitalAsset.Ledger.Api.V1.Commands commands)
        {
            string commandId = commands.CommandId;
            string party = commands.Party;
            
            return new SubmitCommandsRequest(commands.WorkflowId, commands.ApplicationId, commandId, party,
                                            commands.LedgerEffectiveTime.ToDateTimeOffset(), commands.MaximumRecordTime.ToDateTimeOffset(),
                                            from c in commands.Commands_ select Command.FromProtoCommand(c));
        }

        public static DigitalAsset.Ledger.Api.V1.Commands ToProto(string ledgerId, string workflowId, string applicationId,
                                                                  string commandId, string party, DateTimeOffset ledgerEffectiveTime,
                                                                  DateTimeOffset maximumRecordTime, IEnumerable<Command> commands)
        {
            var c = new Commands { LedgerId = ledgerId, WorkflowId = workflowId, ApplicationId = applicationId, CommandId = commandId, Party = party,
                                   LedgerEffectiveTime = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(ledgerEffectiveTime),
                                   MaximumRecordTime = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(maximumRecordTime) };
            c.Commands_.AddRange(from command in commands select command.ToProtoCommand());

            return c;
        }

        public DigitalAsset.Ledger.Api.V1.Commands ToProto(string ledgerId)
        {
            return ToProto(ledgerId, WorkflowId, ApplicationId, CommandId, Party, LedgerEffectiveTime, MaximumRecordTime, Commands);
        }

        public string WorkflowId { get; }

        public string ApplicationId { get; }

        public string CommandId { get; }

        public string Party { get; }

        public DateTimeOffset LedgerEffectiveTime { get; }

        public DateTimeOffset MaximumRecordTime { get; }

        public IReadOnlyList<Command> Commands { get; }

        public override string ToString() => $"SubmitCommandsRequest{{workflowId='{WorkflowId}', applicationId='{ApplicationId}', commandId='{CommandId}', party='{Party}', ledgerEffectiveTime={LedgerEffectiveTime}, maximumRecordTime={MaximumRecordTime}, commands={Commands}}}";

        public override bool Equals(object obj)
        {
            return this.Compare(obj, rhs => _hashCode == rhs._hashCode &&
                                            WorkflowId == rhs.WorkflowId &&
                                            ApplicationId == rhs.ApplicationId &&
                                            CommandId == rhs.CommandId &&
                                            Party == rhs.Party &&
                                            LedgerEffectiveTime == rhs.LedgerEffectiveTime &&
                                            MaximumRecordTime == rhs.MaximumRecordTime &&
                                            !Commands.Except(rhs.Commands).Any());
        }

        public override int GetHashCode() => _hashCode;

        public static bool operator ==(SubmitCommandsRequest lhs, SubmitCommandsRequest rhs) => lhs.Compare(rhs);
        public static bool operator !=(SubmitCommandsRequest lhs, SubmitCommandsRequest rhs) => !(lhs == rhs);
    }
}
