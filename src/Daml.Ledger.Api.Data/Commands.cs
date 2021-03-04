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

        public Commands(string workflowId,
                        string applicationId,
                        string commandId,
                        string party,
                        Optional<DateTimeOffset> minLedgerTimeAbs,
                        Optional<TimeSpan> minLedgerTimeRel,
                        Optional<TimeSpan> deduplicationTime,
                        IEnumerable<Command> commands)
            : this(workflowId, applicationId, commandId, new List<string>() { party }, null, minLedgerTimeAbs, minLedgerTimeRel, deduplicationTime, commands)
        { }

        public Commands(string workflowId,
                        string applicationId,
                        string commandId,
                        IEnumerable<string> actAs,
                        IEnumerable<string> readAs,
                        Optional<DateTimeOffset> minLedgerTimeAbs,
                        Optional<TimeSpan> minLedgerTimeRel,
                        Optional<TimeSpan> deduplicationTime,
                        IEnumerable<Command> commands)
        {
            WorkflowId = workflowId;
            ApplicationId = applicationId;
            CommandId = commandId;

            if (actAs == null || !actAs.Any())
                throw new ArgumentNullException("actAs must have at least one element");

            ActAs = actAs.ToList().AsReadOnly();
            Party = ActAs[0];
            ReadAs = readAs != null ? readAs.ToList().AsReadOnly() : new List<string>().AsReadOnly();
            MinLedgerTimeAbs = minLedgerTimeAbs;
            MinLedgerTimeRel = minLedgerTimeRel;
            DeduplicationTime = deduplicationTime;
            CommandList = commands.ToList().AsReadOnly();

            _hashCode = new HashCodeHelper().Add(WorkflowId).Add(ApplicationId).Add(CommandId).AddRange(ActAs).AddRange(ReadAs).Add(MinLedgerTimeAbs).Add(MinLedgerTimeRel).Add(DeduplicationTime).AddRange(CommandList).ToHashCode();
        }

        public static Commands FromProto(Com.Daml.Ledger.Api.V1.Commands commands)
        {
            string commandId = commands.CommandId;
            string party = commands.Party;

            var actAs = new List<string>();
            if (commands.ActAs != null)
                actAs.AddRange(commands.ActAs);

            if (!actAs.Contains(party))
                actAs.Insert(0, party);

            var readAs = new List<string>();
            if (commands.ReadAs != null)
                readAs.AddRange(commands.ReadAs);

            return new Commands(commands.WorkflowId, commands.ApplicationId, commandId, actAs, readAs,
                                commands.MinLedgerTimeAbs != null ? Optional.Of(commands.MinLedgerTimeAbs.ToDateTimeOffset()) : None.Value,
                                commands.MinLedgerTimeRel != null ? Optional.Of(commands.MinLedgerTimeRel.ToTimeSpan()) : None.Value,
                                commands.DeduplicationTime != null ? Optional.Of(commands.DeduplicationTime.ToTimeSpan()) : None.Value,
                                from c in commands.Commands_ select Command.FromProtoCommand(c));
        }

        public static Com.Daml.Ledger.Api.V1.Commands ToProto(string ledgerId, string workflowId, string applicationId, string commandId,
                                                              IEnumerable<string> actAs, IEnumerable<string> readAs,  
                                                              Optional<DateTimeOffset> minLedgerTimeAbs,
                                                              Optional<TimeSpan> minLedgerTimeRel, Optional<TimeSpan> deduplicationTime,
                                                              IEnumerable<Command> commands)
        {
            var c = new Com.Daml.Ledger.Api.V1.Commands { LedgerId = ledgerId, WorkflowId = workflowId, ApplicationId = applicationId, CommandId = commandId };
            
            c.ActAs.AddRange(actAs);
            c.ReadAs.AddRange(readAs);
            c.Party = c.ActAs[0];
            minLedgerTimeAbs.IfPresent(t => c.MinLedgerTimeAbs = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(t));
            minLedgerTimeRel.IfPresent(t => c.MinLedgerTimeRel = Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(t));
            deduplicationTime.IfPresent(d => c.DeduplicationTime = Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(d));
            c.Commands_.AddRange(from command in commands select command.ToProtoCommand());

            return c;
        }
        
        public static Com.Daml.Ledger.Api.V1.Commands ToProto(string ledgerId, string workflowId, string applicationId, string commandId, 
                                                              string party, Optional<DateTimeOffset> minLedgerTimeAbs, 
                                                              Optional<TimeSpan> minLedgerTimeRel, Optional<TimeSpan> deduplicationTime,
                                                              IEnumerable<Command> commands)
        {
            return ToProto(ledgerId, workflowId, applicationId, commandId, new List<string>() { party }, new List<string>(), minLedgerTimeAbs, minLedgerTimeRel, deduplicationTime, commands);
        }

        public Com.Daml.Ledger.Api.V1.Commands ToProto(string ledgerId)
        {
            return ToProto(ledgerId, WorkflowId, ApplicationId, CommandId, Party, MinLedgerTimeAbs, MinLedgerTimeRel, DeduplicationTime, CommandList);
        }

        public string WorkflowId { get; }

        public string ApplicationId { get; }

        public string CommandId { get; }

        public string Party { get; }

        public IReadOnlyList<string> ActAs { get; }

        public IReadOnlyList<string> ReadAs { get; }

        public Optional<DateTimeOffset> MinLedgerTimeAbs { get; }
        
        public Optional<TimeSpan> MinLedgerTimeRel { get; }
        
        public Optional<TimeSpan> DeduplicationTime { get; }

        public IReadOnlyList<Command> CommandList { get; }

        public override string ToString() => $"Commands{{workflowId='{WorkflowId}', applicationId='{ApplicationId}', commandId='{CommandId}', actAs='{ActAs}', readAs='{ReadAs}', minLedgerTimeAbs={MinLedgerTimeAbs}, minLedgerTimeRel={MinLedgerTimeRel}, deduplicationTime={DeduplicationTime}, commands={CommandList}}}";

        public override bool Equals(object obj)
        {
            return this.Compare(obj, rhs => _hashCode == rhs._hashCode &&
                                            WorkflowId == rhs.WorkflowId &&
                                            ApplicationId == rhs.ApplicationId &&
                                            CommandId == rhs.CommandId &&
                                            Party == rhs.Party &&
                                            !ActAs.Except(rhs.ActAs).Any() &&
                                            !ReadAs.Except(rhs.ReadAs).Any() &&
                                            MinLedgerTimeAbs == rhs.MinLedgerTimeAbs &&
                                            MinLedgerTimeRel == rhs.MinLedgerTimeRel &&
                                            DeduplicationTime == rhs.DeduplicationTime && 
                                            !CommandList.Except(rhs.CommandList).Any());
        }

        public override int GetHashCode() => _hashCode;

        public static bool operator ==(Commands lhs, Commands rhs) => lhs.Compare(rhs);
        public static bool operator !=(Commands lhs, Commands rhs) => !(lhs == rhs);
    }
}
