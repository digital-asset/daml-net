// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Linq;

namespace Daml.Ledger.Api.Data
{
    using Util;

    public class Transaction : IWorkflowEvent, IComparable<Transaction>, IEquatable<Transaction>
    {
        private readonly int _hashCode;

        public Transaction(string transactionId, string commandId, string workflowId, DateTimeOffset effectiveAt, IEnumerable<IEvent> events, string offset)
        {
            TransactionId = transactionId;
            CommandId = commandId;
            WorkflowId = workflowId;
            EffectiveAt = effectiveAt;
            Events = events.ToList().AsReadOnly();
            Offset = offset;

            _hashCode = new HashCodeHelper().Add(TransactionId).Add(CommandId).Add(WorkflowId).Add(EffectiveAt).AddRange(Events).Add(Offset).ToHashCode();
        }

        public static Transaction FromProto(Com.DigitalAsset.Ledger.Api.V1.Transaction transaction)
        {
            var events = (from e in transaction.Events select EventHelper.FromProtoEvent(e)).ToList();
            return new Transaction(transaction.TransactionId, transaction.CommandId, transaction.WorkflowId, transaction.EffectiveAt.ToDateTimeOffset(), events, transaction.Offset);
        }

        public Com.DigitalAsset.Ledger.Api.V1.Transaction ToProto()
        {
            var transaction = new Com.DigitalAsset.Ledger.Api.V1.Transaction { TransactionId = TransactionId, CommandId = CommandId, WorkflowId = WorkflowId,
                                                                           EffectiveAt = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(EffectiveAt), Offset = Offset };
            transaction.Events.AddRange(from e in Events select EventHelper.ToProtoEvent(e));
            return transaction;
        }

        public string TransactionId { get; }

        public string CommandId { get; }

        public DateTimeOffset EffectiveAt { get; }

        public IReadOnlyList<IEvent> Events { get; }

        public string Offset { get; }

        public string WorkflowId { get; }

        public override string ToString() => $"Transaction{{transactionId='{TransactionId}', commandId='{CommandId}', workflowId='{WorkflowId}', effectiveAt={EffectiveAt}, events={Events}, offset='{Offset}'}}";

        public override bool Equals(object obj) => Equals((Transaction)obj);

        public bool Equals(Transaction obj) => this.Compare(obj, rhs => _hashCode == rhs._hashCode && 
                                                                        TransactionId == rhs.TransactionId &&
                                                                        CommandId == rhs.CommandId &&
                                                                        WorkflowId == rhs.WorkflowId && 
                                                                        EffectiveAt == rhs.EffectiveAt &&
                                                                        Offset == rhs.Offset &&
                                                                        !Events.Except(rhs.Events).Any());

        public override int GetHashCode() => _hashCode;

        public int CompareTo(Transaction rhs) => GetHashCode().CompareTo(rhs.GetHashCode());

        public static bool operator ==(Transaction lhs, Transaction rhs) => lhs.Compare(rhs);
        public static bool operator !=(Transaction lhs, Transaction rhs) => !(lhs == rhs);
    }
} 
