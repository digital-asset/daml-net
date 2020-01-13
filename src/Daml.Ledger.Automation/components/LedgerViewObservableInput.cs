// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Immutable;
using Com.Daml.Ledger.Api.Data;
using Com.Daml.Ledger.Api.Util;
using Daml.Ledger.Automation.Components.Helpers;
using Google.Rpc;

namespace Daml.Ledger.Automation.Components
{
    public partial class LedgerViewObservable
    {
        public abstract class LedgerViewObservableInput { }

        public class SubmissionFailure : LedgerViewObservableInput
        {
            public SubmissionFailure(string commandId, Exception exception) { CommandId = commandId; Exception = exception; }
            public string CommandId { get; }
            public Exception Exception { get; }
            public override string ToString() => $"SubmissionFailure{{commandId='{CommandId}', exception={Exception.Message}}}";
        }

        public class CompletionFailure : LedgerViewObservableInput
        {
            public CompletionFailure(string commandId, Status status) { CommandId = commandId; Status = status; }
            public string CommandId { get; }
            public Status Status { get; }
            public override string ToString() => $"CompletionFailure{{commandId='{CommandId}', status={Status}}}";

        }

        public class WorkflowEventWrapper : LedgerViewObservableInput
        {
            public WorkflowEventWrapper(IWorkflowEvent workflowEvent) { WorkflowEvent = workflowEvent; }

            public IWorkflowEvent WorkflowEvent { get; }

            public override string ToString() => $"WorkflowEventWrapper{{workflowEvent={WorkflowEvent}}}";
            public override bool Equals(object obj) => this.Compare(obj, rhs => WorkflowEvent == rhs.WorkflowEvent);
            public override int GetHashCode() => WorkflowEvent.GetHashCode();

            public static bool operator ==(WorkflowEventWrapper lhs, WorkflowEventWrapper rhs) => lhs.Compare(rhs);
            public static bool operator !=(WorkflowEventWrapper lhs, WorkflowEventWrapper rhs) => !(lhs == rhs);

        }

        public class CommandIdAndPendingSet : LedgerViewObservableInput
        {
            public CommandIdAndPendingSet(string commandId, ImmutableDictionary<Identifier, ImmutableHashSet<string>> templateToContracts)
            {
                CommandId = commandId;
                TemplateToContracts = templateToContracts;
            }

            public string CommandId { get; }
            public ImmutableDictionary<Identifier, ImmutableHashSet<string>> TemplateToContracts { get; }

            public static CommandIdAndPendingSet From(CommandsAndPendingSet commandsAndPendingSet) => new CommandIdAndPendingSet(commandsAndPendingSet.SubmitCommandsRequest.CommandId, commandsAndPendingSet.ContractIdsPendingIfSucceed);

            public override string ToString() => $"CommandIdAndPendingSet{{commandId='{CommandId}', templateToContracts={TemplateToContracts}}}";
        }
    }
}
 