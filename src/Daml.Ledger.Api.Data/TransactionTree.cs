// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Daml.Ledger.Api.Data 
{
    using Util;

    public class TransactionTree : IComparable<TransactionTree>, IEquatable<TransactionTree>
    {
        private readonly int _hashCode;

        public TransactionTree(string transactionId, string commandId, string workflowId, DateTimeOffset effectiveAt, IReadOnlyDictionary<string, TreeEvent> eventsById, 
                               IEnumerable<string> rootEventIds, string offset)
        {
            TransactionId = transactionId;
            CommandId = commandId;
            WorkflowId = workflowId;
            EffectiveAt = effectiveAt;
            EventsById = ImmutableDictionary.CreateRange(eventsById);
            RootEventIds = rootEventIds.ToList().AsReadOnly();
            Offset = offset;

            _hashCode = new HashCodeHelper().Add(TransactionId).Add(CommandId).Add(WorkflowId).Add(EffectiveAt).AddDictionary(EventsById).AddRange(RootEventIds).Add(Offset).ToHashCode();
        }

        public static TransactionTree FromProto(Com.DigitalAsset.Ledger.Api.V1.TransactionTree tree)
        {
            var eventsById = tree.EventsById.Values.Select(e => (e.Created?.EventId ?? e.Exercised?.EventId, TreeEvent.FromProtoTreeEvent(e))).ToDictionary(p => p.Item1, p => p.Item2);
            return new TransactionTree(tree.TransactionId, tree.CommandId, tree.WorkflowId, tree.EffectiveAt.ToDateTimeOffset(), eventsById, tree.RootEventIds.ToList(), tree.Offset);
        }

        public Com.DigitalAsset.Ledger.Api.V1.TransactionTree ToProto()
        {
            var transactionTree = new Com.DigitalAsset.Ledger.Api.V1.TransactionTree { TransactionId = TransactionId, CommandId = CommandId, WorkflowId = WorkflowId,
                                                                                   EffectiveAt = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(EffectiveAt), Offset = Offset };

            transactionTree.EventsById.Add(EventsById.Select(p => (p.Key, p.Value.ToProtoTreeEvent())).ToDictionary(p => p.Item1, p => p.Item2));
            transactionTree.RootEventIds.AddRange(RootEventIds);

            return transactionTree;
        }

        public string TransactionId { get; }

        public string CommandId { get; }

        public string WorkflowId {  get; }

        public DateTimeOffset EffectiveAt { get; }

        public IImmutableDictionary<string, TreeEvent> EventsById { get; }

        public IReadOnlyList<string> RootEventIds { get; }

        public string Offset { get; }

        public override string ToString() => $"TransactionTree{{transactionId='{TransactionId}', commandId='{CommandId}', workflowId='{WorkflowId}', effectiveAt={EffectiveAt}, eventsById={EventsById}, rootEventIds={RootEventIds}, offset='{Offset}'}}";

        public override bool Equals(object obj) => Equals((TransactionTree)obj);

        public bool Equals(TransactionTree obj)
        {
            return this.Compare(obj, rhs => _hashCode == rhs._hashCode &&
                                            TransactionId == rhs.TransactionId &&
                                            CommandId == rhs.CommandId &&
                                            WorkflowId == rhs.WorkflowId &&
                                            EffectiveAt == rhs.EffectiveAt &&
                                            Offset == rhs.Offset &&
                                            !EventsById.Except(rhs.EventsById).Any() &&
                                            !RootEventIds.Except(rhs.RootEventIds).Any());
        }

        public int CompareTo(TransactionTree rhs) => GetHashCode().CompareTo(rhs.GetHashCode());

        public override int GetHashCode() => _hashCode;

        public static bool operator ==(TransactionTree lhs, TransactionTree rhs) => lhs.Compare(rhs);
        public static bool operator !=(TransactionTree lhs, TransactionTree rhs) => !(lhs == rhs);
    }
}
