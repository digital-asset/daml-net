// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Immutable;

namespace Daml.Ledger.Automation.Components
{
    using Daml.Ledger.Api.Data;
    using Daml.Ledger.Api.Data.Util;
    using Daml.Ledger.Automation.Components.Helpers;
    
    public partial class LedgerViewObservable
    {
        /// <summary>
        /// The local view of the Ledger.This view is created and updated every time a new event is received by the client.
        /// </summary>
        /// <typeparam name="R">The type of the contracts in this application.</typeparam>
        public class LedgerView<R>
        {
            public ImmutableDictionary<string, ImmutableDictionary<Identifier, ImmutableHashSet<string>>> CommandIdToContractIds { get; }
            public ImmutableDictionary<Identifier, ImmutableDictionary<string, ImmutableHashSet<string>>> ContractIdsToCommandIds { get; }
            public ImmutableDictionary<Identifier, ImmutableDictionary<string, R>> PendingContractSet { get; }
            public ImmutableDictionary<Identifier, ImmutableDictionary<string, R>> ActiveContractSet { get; }

            protected LedgerView(ImmutableDictionary<string, ImmutableDictionary<Identifier, ImmutableHashSet<string>>> commandIdToContractIds,
                                 ImmutableDictionary<Identifier, ImmutableDictionary<string, ImmutableHashSet<string>>> contractIdsToCommandIds,
                                 ImmutableDictionary<Identifier, ImmutableDictionary<string, R>> pendingContractSet,
                                 ImmutableDictionary<Identifier, ImmutableDictionary<string, R>> activeContractSet)
            {
                CommandIdToContractIds = commandIdToContractIds;
                ContractIdsToCommandIds = contractIdsToCommandIds;
                PendingContractSet = pendingContractSet;
                ActiveContractSet = activeContractSet;

                _hashCode = new HashCodeHelper().Add(CommandIdToContractIds).Add(ContractIdsToCommandIds).Add(PendingContractSet).Add(ActiveContractSet).ToHashCode();
            }

            /**
             * Creates a new empty view of the Ledger.
             */
            public static LedgerView<R> Create()
            {
                return new LedgerView<R>(ImmutableDictionary<string, ImmutableDictionary<Identifier, ImmutableHashSet<string>>>.Empty,
                                         ImmutableDictionary<Identifier, ImmutableDictionary<string, ImmutableHashSet<string>>>.Empty,
                                         ImmutableDictionary<Identifier, ImmutableDictionary<string, R>>.Empty,
                                         ImmutableDictionary<Identifier, ImmutableDictionary<string, R>>.Empty);
            }

            // transaction with created
            internal virtual LedgerView<R> AddActiveContract(Identifier templateId, string contractId, R r)
            {
                _logger.DebugFormat("ledgerView.addActiveContract({0}, {1}, {2})", templateId, contractId, r);

                var oldContractIdsToR = ActiveContractSet.GetOrEmpty(templateId);
                var newContractIdsToR = oldContractIdsToR.Add(contractId, r);
                var newActiveContractSet = ActiveContractSet.Add(templateId, newContractIdsToR);
                return new LedgerView<R>(CommandIdToContractIds, ContractIdsToCommandIds, PendingContractSet, newActiveContractSet);
            }

            // set pending is received
            internal LedgerView<R> SetContractPending(string commandId, Identifier templateId, string contractId)
            {
                _logger.DebugFormat("ledgerView.setContractPending({0}, {1}, {2})", commandId, templateId, contractId);

                // remove from the active contract set
                var oldActiveContracts = ActiveContractSet.GetOrEmpty(templateId);
                var newActiveContracts = oldActiveContracts.Remove(contractId);
                var newActiveContractSet = newActiveContracts.IsEmpty ? ActiveContractSet.Remove(templateId) : ActiveContractSet.Add(templateId, newActiveContracts);

                // add to the pending contract set
                var oldPendingContracts = PendingContractSet.GetOrEmpty(templateId);

                if (!oldActiveContracts.TryGetValue(contractId, out var r))
                    throw new ApplicationException($"Failed to locate expected active contract with id {contractId}");

                var newPendingContracts = oldPendingContracts.Add(contractId, r);
                var newPendingContractSet = PendingContractSet.Add(templateId, newPendingContracts);

                // add to commandId -> contractIds
                var oldTemplateIdToContracts = CommandIdToContractIds.GetOrEmpty(commandId);
                var oldContracts = oldTemplateIdToContracts.GetOrEmpty(templateId);
                var newContracts = oldContracts.Add(contractId);
                var newTemplateIdToContracts = oldTemplateIdToContracts.Add(templateId, newContracts);
                var newCommandIdToContracts = CommandIdToContractIds.Add(commandId, newTemplateIdToContracts);

                // add to contractId -> commandIds
                var oldContractToCommandIds = ContractIdsToCommandIds.GetOrEmpty(templateId);
                var oldCommandIds = oldContractToCommandIds.GetOrEmpty(contractId);
                var newCommandIds = oldCommandIds.Add(commandId);
                var newContractToCommandIds = oldContractToCommandIds.Add(contractId, newCommandIds);
                var newContractIdsToCommandIds = ContractIdsToCommandIds.Add(templateId, newContractToCommandIds);

                LedgerView<R> result = new LedgerView<R>(newCommandIdToContracts, newContractIdsToCommandIds, newPendingContractSet, newActiveContractSet);

                _logger.DebugFormat("ledgerView.setContractPending({0}, {1}, {2})).result: {3}", commandId, templateId, contractId, result);

                return result;
            }

            // completion failure
            internal LedgerView<R> UnsetPendingForContractsOfCommandId(string commandId)
            {
                _logger.DebugFormat("ledgerView.unsetPendingForContractsOfCommandId({0})", commandId);

                var newCommandIdToContractIds = CommandIdToContractIds.Remove(commandId);

                // we go over each contract, see if this is the only command locking it and if the answer is yes we move
                // the contract from pending to active
                var contractsToUnset = CommandIdToContractIds.GetOrEmpty(commandId);

                // these variables can potentially mutate for each contract
                var newContractIdsToCommandIds = ContractIdsToCommandIds;
                var newPendingContractSet = PendingContractSet;
                var newActiveContractSet = ActiveContractSet;

                foreach (var pair in contractsToUnset)
                {
                    var templateId = pair.Key;
                    var oldContractIds = pair.Value;

                    // there variables can potentially mutate
                    var contractIdToCommandIds = newContractIdsToCommandIds.GetOrEmpty(templateId);
                    var pendingContractSet = newPendingContractSet.GetOrEmpty(templateId);
                    var activeContractSet = newActiveContractSet.GetOrEmpty(templateId);

                    foreach (var contractId in oldContractIds)
                    {
                        var oldCommandIds = contractIdToCommandIds.GetOrEmpty(contractId);
                        var newCommandIds = oldCommandIds.Remove(commandId);

                        if (newCommandIds.IsEmpty)
                        {
                            // unset pending for the contract
                            contractIdToCommandIds = contractIdToCommandIds.Remove(contractId);
                            if (pendingContractSet.TryGetValue(contractId, out R r))
                            {
                                pendingContractSet = pendingContractSet.Remove(contractId);
                                activeContractSet = activeContractSet.Add(contractId, r);
                            }
                        }
                        else
                        {
                            contractIdToCommandIds = contractIdToCommandIds.Add(contractId, newCommandIds);
                            // do nothing because other commands are still locking the contract
                        }
                    }

                    newContractIdsToCommandIds = contractIdToCommandIds.IsEmpty ? newContractIdsToCommandIds.Remove(templateId) : newContractIdsToCommandIds.Add(templateId, contractIdToCommandIds);
                    newPendingContractSet = pendingContractSet.IsEmpty ? newPendingContractSet.Remove(templateId) : newPendingContractSet.Add(templateId, pendingContractSet);
                    newActiveContractSet = activeContractSet.IsEmpty ? newActiveContractSet.Remove(templateId) : newActiveContractSet.Add(templateId, activeContractSet);
                }

                return new LedgerView<R>(newCommandIdToContractIds, newContractIdsToCommandIds, newPendingContractSet, newActiveContractSet);
            }

            private static ImmutableDictionary<Identifier, ImmutableDictionary<string, R>> RemoveContract(Identifier templateId, string contractId, ImmutableDictionary<Identifier, ImmutableDictionary<string, R>> from)
            {
                if (from.TryGetValue(templateId, out var oldActiveContracts))
                {
                    if (oldActiveContracts.ContainsKey(contractId))
                    {
                        ImmutableDictionary<string, R> newActiveContracts = oldActiveContracts.Remove(contractId);
                        return newActiveContracts.IsEmpty ? from.Remove(templateId) : from.Add(templateId, newActiveContracts);
                    }
                }
                return from;
            }

            // transaction with archived
            internal LedgerView<R> ArchiveContract(Identifier templateId, string contractId)
            {
                _logger.DebugFormat("ledgerView.archiveContract({0}, {1})", templateId, contractId);

                var newPendingContractSet = RemoveContract(templateId, contractId, PendingContractSet);
                var newActiveContractSet = RemoveContract(templateId, contractId, ActiveContractSet);

                var newCommandIdToContractIds = CommandIdToContractIds;

                if (ContractIdsToCommandIds.TryGetValue(templateId, out var contractIdToCommandIds))
                {
                    if (contractIdToCommandIds.TryGetValue(contractId, out var commandIds))
                    {
                        foreach (var commandId in commandIds)
                        {
                            var oldContracts = newCommandIdToContractIds.GetOrEmpty(commandId);
                            var oldContractIds = oldContracts.GetOrEmpty(templateId);
                            var newContractIds = oldContractIds.Remove(contractId);
                            var newContracts = newContractIds.IsEmpty ? oldContracts.Remove(templateId) : oldContracts.Add(templateId, newContractIds);
                            newCommandIdToContractIds = newContracts.IsEmpty ? newCommandIdToContractIds.Remove(commandId) : newCommandIdToContractIds.Add(commandId, newContracts);
                        }
                    }
                }

                var newContractIdsToCommandIds = ContractIdsToCommandIds;
                if (newContractIdsToCommandIds.TryGetValue(templateId, out var oldContractsMap))
                {
                    var newContracts = oldContractsMap.Remove(contractId);
                    newContractIdsToCommandIds = newContracts.IsEmpty ? newContractIdsToCommandIds.Remove(templateId) : newContractIdsToCommandIds.Add(templateId, newContracts);
                }

                return new LedgerView<R>(newCommandIdToContractIds, newContractIdsToCommandIds, newPendingContractSet, newActiveContractSet);
            }

            /**
             * @param templateId The template of the contracts in which the client is interested
             * @return A map contractId to contract containing all the contracts with template id equal
             *         to the one passed as argument
             */
            public ImmutableDictionary<string, R> GetContracts(Identifier templateId)
            {
                return ActiveContractSet.GetOrEmpty(templateId);
            }

            public override string ToString() => $"LedgerView{{activeContractSet={ActiveContractSet}, pendingContractSet={PendingContractSet}, contractIdsToCommandIds={ContractIdsToCommandIds}, commandIdToContractIds={CommandIdToContractIds}}}";

            public override bool Equals(object obj) => this.Compare(obj, rhs => CommandIdToContractIds == rhs.CommandIdToContractIds && ContractIdsToCommandIds == rhs.ContractIdsToCommandIds && PendingContractSet == rhs.PendingContractSet && ActiveContractSet == rhs.ActiveContractSet);

            public static bool operator ==(LedgerView<R> lhs, LedgerView<R> rhs) => lhs.Compare(rhs);
            public static bool operator !=(LedgerView<R> lhs, LedgerView<R> rhs) => !(lhs == rhs);

            public override int GetHashCode() => _hashCode;

            private readonly int _hashCode;
        }

        /**
         * A ledger view for unit testing of bots.
         *
         * @param <R> The type of the contracts in this application.
         */
        public class LedgerTestView<R> : LedgerView<R>
        {
            public LedgerTestView(ImmutableDictionary<String, ImmutableDictionary<Identifier, ImmutableHashSet<string>>> commandIdToContractIds,
                                  ImmutableDictionary<Identifier, ImmutableDictionary<string, ImmutableHashSet<string>>> contractIdsToCommandIds,
                                  ImmutableDictionary<Identifier, ImmutableDictionary<string, R>> pendingContractSet,
                                  ImmutableDictionary<Identifier, ImmutableDictionary<string, R>> activeContractSet)
             : base(commandIdToContractIds, contractIdsToCommandIds, pendingContractSet, activeContractSet)
            {
            }

            private LedgerTestView(LedgerView<R> ledgerView)
                : base(ledgerView.CommandIdToContractIds, ledgerView.ContractIdsToCommandIds, ledgerView.PendingContractSet, ledgerView.ActiveContractSet)
            {
            }

            internal override LedgerView<R> AddActiveContract(Identifier templateId, string contractId, R r)
            {
                var lv = base.AddActiveContract(templateId, contractId, r);
                return new LedgerTestView<R>(lv);
            }
        }
    }
}
 