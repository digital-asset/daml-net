// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Linq;

namespace Daml.Ledger.Api.Data 
{
    using Util;

    /// <summary>
    /// This used be named SubmitCommandRequest but in this implementation we don't use the Request classes as the service calls are via the Daml.Ledger.CLient classes and not directly to the gRPC service classes.
    /// </summary>
    public class Commands
    {
        private readonly int _hashCode;

        public Commands(string workflowId, string applicationId, string commandId, string party, 
                        DateTimeOffset ledgerEffectiveTime, DateTimeOffset maximumRecordTime, IEnumerable<Command> commands)
        {
            WorkflowId = workflowId;
            ApplicationId = applicationId;
            CommandId = commandId;
            Party = party;
            LedgerEffectiveTime = ledgerEffectiveTime;
            MaximumRecordTime = maximumRecordTime;
            CommandList = commands.ToList().AsReadOnly();

            _hashCode = new HashCodeHelper().Add(WorkflowId).Add(ApplicationId).Add(CommandId).Add(Party).Add(LedgerEffectiveTime).Add(MaximumRecordTime).AddRange(CommandList).ToHashCode();
        }

        public static Commands FromProto(Com.DigitalAsset.Ledger.Api.V1.Commands commands)
        {
            string commandId = commands.CommandId;
            string party = commands.Party;
            
            return new Commands(commands.WorkflowId, commands.ApplicationId, commandId, party,
                                commands.LedgerEffectiveTime.ToDateTimeOffset(), commands.MaximumRecordTime.ToDateTimeOffset(),
                                from c in commands.Commands_ select Command.FromProtoCommand(c));
        }

        public static Com.DigitalAsset.Ledger.Api.V1.Commands ToProto(string ledgerId, string workflowId, string applicationId,
                                                                      string commandId, string party, DateTimeOffset ledgerEffectiveTime,
                                                                      DateTimeOffset maximumRecordTime, IEnumerable<Command> commands)
        {
            var c = new Com.DigitalAsset.Ledger.Api.V1.Commands { LedgerId = ledgerId, WorkflowId = workflowId, ApplicationId = applicationId, CommandId = commandId, Party = party,
                                                                  LedgerEffectiveTime = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(ledgerEffectiveTime),
                                                                  MaximumRecordTime = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(maximumRecordTime) };
            c.Commands_.AddRange(from command in commands select command.ToProtoCommand());

            return c;
        }

        public Com.DigitalAsset.Ledger.Api.V1.Commands ToProto(string ledgerId)
        {
            return ToProto(ledgerId, WorkflowId, ApplicationId, CommandId, Party, LedgerEffectiveTime, MaximumRecordTime, CommandList);
        }

        public string WorkflowId { get; }

        public string ApplicationId { get; }

        public string CommandId { get; }

        public string Party { get; }

        public DateTimeOffset LedgerEffectiveTime { get; }

        public DateTimeOffset MaximumRecordTime { get; }

        public IReadOnlyList<Command> CommandList { get; }

        public override string ToString() => $"Commands{{workflowId='{WorkflowId}', applicationId='{ApplicationId}', commandId='{CommandId}', party='{Party}', ledgerEffectiveTime={LedgerEffectiveTime}, maximumRecordTime={MaximumRecordTime}, commands={CommandList}}}";

        public override bool Equals(object obj)
        {
            return this.Compare(obj, rhs => _hashCode == rhs._hashCode &&
                                            WorkflowId == rhs.WorkflowId &&
                                            ApplicationId == rhs.ApplicationId &&
                                            CommandId == rhs.CommandId &&
                                            Party == rhs.Party &&
                                            LedgerEffectiveTime == rhs.LedgerEffectiveTime &&
                                            MaximumRecordTime == rhs.MaximumRecordTime &&
                                            !CommandList.Except(rhs.CommandList).Any());
        }

        public override int GetHashCode() => _hashCode;

        public static bool operator ==(Commands lhs, Commands rhs) => lhs.Compare(rhs);
        public static bool operator !=(Commands lhs, Commands rhs) => !(lhs == rhs);
    }
}
