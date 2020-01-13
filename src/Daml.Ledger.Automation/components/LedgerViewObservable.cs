// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Reactive.Linq;
using Com.Daml.Ledger.Api.Data;
using Com.Daml.Ledger.Api.Util;
using Daml.Ledger.Automation.Components.Helpers;
using log4net;

namespace Daml.Ledger.Automation.Components
{
    public partial class LedgerViewObservable
    {
        private class StateWithShouldEmit<R>
        {
            public StateWithShouldEmit(LedgerView<R> ledgerView, int commandsCounter, bool shouldEmit)
            {
                LedgerView = ledgerView;
                CommandsCounter = commandsCounter;
                ShouldEmit = shouldEmit;

                _hashCode = new HashCodeHelper().Add(LedgerView).Add(CommandsCounter).Add(ShouldEmit).ToHashCode();
            }

            public bool ShouldEmit { get; }
            public LedgerView<R> LedgerView { get; }
            public int CommandsCounter { get; }

            // create a new ledgerView which should not be emitted
            public static StateWithShouldEmit<R> Create() => new StateWithShouldEmit<R>(LedgerView<R>.Create(), 0, false);
            public static StateWithShouldEmit<R> Of(LedgerView<R> initialLedgerView) => new StateWithShouldEmit<R>(initialLedgerView, 0, false);
            public StateWithShouldEmit<R> Emit(LedgerView<R> newLedgerView) => new StateWithShouldEmit<R>(newLedgerView, CommandsCounter, true);

            public override string ToString() => $"StateWithShouldEmit{{ledgerView={LedgerView}, commandsCounter={CommandsCounter}, shouldEmit={ShouldEmit}}}";

            public override bool Equals(object obj) => this.Compare(obj, rhs => _hashCode == rhs._hashCode && LedgerView == rhs.LedgerView && CommandsCounter == rhs.CommandsCounter && ShouldEmit == rhs.ShouldEmit);

            public static bool operator ==(StateWithShouldEmit<R> lhs, StateWithShouldEmit<R> rhs) => lhs.Compare(rhs);
            public static bool operator !=(StateWithShouldEmit<R> lhs, StateWithShouldEmit<R> rhs) => !(lhs == rhs);

            public override int GetHashCode() => _hashCode;

            private readonly int _hashCode;
        }

        public static IObservable<LedgerView<R>> Of<R>(LedgerView<R> initialLedgerView,
                                                       IObservable<SubmissionFailure> submissionFailures,
                                                       IObservable<CompletionFailure> completionFailures,
                                                       IObservable<IWorkflowEvent> workflowEvents,
                                                       IObservable<CommandsAndPendingSet> commandsAndPendingsSet,
                                                       Func<CreatedContract, R> transform)
        {
            IObservable<LedgerViewObservableInput> workflowEventsWrapper = from we in workflowEvents select new WorkflowEventWrapper(we);
            IObservable<LedgerViewObservableInput> commandIdAndPendingSet = from c in commandsAndPendingsSet select CommandIdAndPendingSet.From(c);
            IObservable<LedgerViewObservableInput> all = Observable.Merge(submissionFailures, completionFailures, workflowEventsWrapper, commandIdAndPendingSet);

            bool shouldEmitInitial = !initialLedgerView.ActiveContractSet.IsEmpty;
            var initialState = new StateWithShouldEmit<R>(initialLedgerView, 0, shouldEmitInitial);

            return from s in all.Scan(initialState, (input, state) => Accumulate<R>(input, state, transform)).Where(s => s.ShouldEmit) select s.LedgerView;
        }

        public static IObservable<LedgerView<R>> Of<R>(IObservable<SubmissionFailure> submissionFailuresCommandIds,
                                                       IObservable<CompletionFailure> completionFailuresCommandIds,
                                                       IObservable<IWorkflowEvent> workflowEvents,
                                                       IObservable<CommandsAndPendingSet> commandsAndPendingsSet,
                                                       Func<CreatedContract, R> transform)
        {
            return Of(LedgerView<R>.Create(), submissionFailuresCommandIds, completionFailuresCommandIds, workflowEvents, commandsAndPendingsSet, transform);
        }

        private static StateWithShouldEmit<R> Accumulate<R>(StateWithShouldEmit<R> stateWithShouldEmit, LedgerViewObservableInput ledgerViewObservableInput, Func<CreatedContract, R> transform)
        {
            _logger.DebugFormat("LedgerView.accumulate({0}, {1})", stateWithShouldEmit, ledgerViewObservableInput);

            if (ledgerViewObservableInput is SubmissionFailure)
            {
                var submissionFailure = (SubmissionFailure)ledgerViewObservableInput;

                LedgerView<R> newLedgerView = stateWithShouldEmit.LedgerView.UnsetPendingForContractsOfCommandId(submissionFailure.CommandId);
                return stateWithShouldEmit.Emit(newLedgerView);
            }

            if (ledgerViewObservableInput is CompletionFailure)
            {
                var completionFailure = (CompletionFailure)ledgerViewObservableInput;

                LedgerView<R> newLedgerView = stateWithShouldEmit.LedgerView.UnsetPendingForContractsOfCommandId(completionFailure.CommandId);

                if (newLedgerView.Equals(stateWithShouldEmit.LedgerView))
                    return stateWithShouldEmit;

                _logger.Info($"Command {completionFailure.CommandId} failed: {completionFailure.Status}");
                return stateWithShouldEmit.Emit(newLedgerView);
            }

            if (ledgerViewObservableInput is WorkflowEventWrapper)
            {
                var workflowEvent = ((WorkflowEventWrapper)ledgerViewObservableInput).WorkflowEvent;

                if (workflowEvent is Transaction)
                {
                    var transaction = (Transaction)workflowEvent;

                    LedgerView<R> newLedgerView = stateWithShouldEmit.LedgerView;

                    TransactionContext transactionContext = TransactionContext.ForTransaction(transaction);
                    foreach (var ev in transaction.Events)
                    {
                        if (ev is CreatedEvent)
                        {
                            var createdEvent = (CreatedEvent)ev;
                            R r = transform(new CreatedContract(createdEvent.TemplateId, createdEvent.Arguments, transactionContext));
                            newLedgerView = newLedgerView.AddActiveContract(createdEvent.TemplateId, createdEvent.ContractId, r);
                        }
                        else if (ev is ArchivedEvent)
                        {
                            var archivedEvent = (ArchivedEvent)ev;
                            newLedgerView = newLedgerView.ArchiveContract(archivedEvent.TemplateId, archivedEvent.ContractId);
                        }
                    }

                    // the remaining contracts that were considered pending when the command was submitted
                    // but are not archived by this transaction are set as active again

                    string commandId = transaction.CommandId;
                    newLedgerView = newLedgerView.UnsetPendingForContractsOfCommandId(commandId);
                    return stateWithShouldEmit.Emit(newLedgerView);
                }

                if (workflowEvent is GetActiveContractsResponse)
                {
                    var activeContracts = (GetActiveContractsResponse)workflowEvent;

                    LedgerView<R> newLedgerView = stateWithShouldEmit.LedgerView;
                    GetActiveContractsResponseContext context = new GetActiveContractsResponseContext(activeContracts.WorkflowId);

                    foreach (var createdEvent in activeContracts.CreatedEvents)
                    {
                        R r = transform(new CreatedContract(createdEvent.TemplateId, createdEvent.Arguments, context));
                        newLedgerView = newLedgerView.AddActiveContract(createdEvent.TemplateId, createdEvent.ContractId, r);
                    }

                    return stateWithShouldEmit.Emit(newLedgerView);
                }

                throw new ApplicationException(workflowEvent.ToString());

            }

            if (ledgerViewObservableInput is CommandIdAndPendingSet)
            {
                var commandIdAndPendingSet = (CommandIdAndPendingSet)ledgerViewObservableInput;

                LedgerView<R> newLedgerView = stateWithShouldEmit.LedgerView;

                foreach (var templateId in commandIdAndPendingSet.TemplateToContracts.Keys)
                {
                    var contractIds = commandIdAndPendingSet.TemplateToContracts[templateId];
                    foreach (var contractId in contractIds)
                        newLedgerView = newLedgerView.SetContractPending(commandIdAndPendingSet.CommandId, templateId, contractId);

                    _logger.DebugFormat("newLedgerView: {0}", newLedgerView.GetContracts(templateId));
                }

                // commands with commandId empty is the signal that the bot is done with the commands

                bool isBoundary = string.IsNullOrEmpty(commandIdAndPendingSet.CommandId);

                if (isBoundary)
                {
                    // if we found a boundary, we emit a value only in case there have been commands in between this and the
                    // previous boundary:
                    // previous_boundary ~> any non commands ~> this_boundary =====> don't emit anything
                    // previous_boundary ~> one or more commands ~> this_boundary =====> emit the new ledgerView
                    return new StateWithShouldEmit<R>(newLedgerView, 0, stateWithShouldEmit.CommandsCounter > 0);
                }
                return new StateWithShouldEmit<R>(newLedgerView, stateWithShouldEmit.CommandsCounter + 1, false);
            }

            // return stateWithShouldEmit;
            throw new ApplicationException(stateWithShouldEmit.ToString());
        }

#if Compilable

            private static Single<Tuple<LedgerView<R>, LedgerOffset>> LedgerViewAndOffsetFromACS<R>(IObservable<GetActiveContractsResponse> responses, Func<CreatedContract, R> transform)
            {
                var t = responses.Reduce(Tuple.Create(StateWithShouldEmit<R>.Create(), (Optional<LedgerOffset>) None.Value), 
                                                        (viewAndOffset, response) => {
                                                                     StateWithShouldEmit<R> ledgerView = viewAndOffset.Item1;
                                                                     StateWithShouldEmit<R> newLedgerView = Accumulate(ledgerView, new WorkflowEventWrapper(response), transform);
                                                                     Optional<LedgerOffset> offset = response.Offset.Map(off => (LedgerOffset) new LedgerOffset.Absolute(off));
                                                                     return Tuple.Create(newLedgerView, offset);
                                                        });

                Single <Tuple<StateWithShouldEmit<R>, Optional<LedgerOffset>>> lastViewAndOffset = responses.Reduce(Tuple.Create(StateWithShouldEmit.Create(), None.Value),
                                                                                                                   (viewAndOffset, response) => 
                                                                                                                   {
                                                                                                                       StateWithShouldEmit<R> ledgerView = viewAndOffset.First;
                                                                                                                       StateWithShouldEmit<R> newLedgerView = Accumulate(ledgerView, new WorkflowEventWrapper(response), transform);
                                                                                                                       Optional<LedgerOffset> offset = response.Offset.Map(off => new LedgerOffset.Absolute(off));
                                                                                                                       return Tuple.Create(newLedgerView, offset);
                                                                                                                   });
                return lastViewAndOffset.Map(m => Tuple.Create(m.getFirst().ledgerView, m.getSecond().orElse(LedgerOffset.LedgerBegin.Instance)));
            }

            private static Single<LedgerView<R>> LedgerViewFromObservable(LedgerView<R> initial, IObservable<IWorkflowEvent> events, Func<CreatedContract, R> transform)
            {
                return events.Reduce(new StateWithShouldEmit<R>(initial, 0, false), (ledgerView, event) => Accumulate(ledgerView, new WorkflowEventWrapper(event), transform)).Map(m => m.ledgerView);
            }

#endif
        private static readonly ILog _logger = LogManager.GetLogger(typeof(LedgerViewObservable));
    }
}
 