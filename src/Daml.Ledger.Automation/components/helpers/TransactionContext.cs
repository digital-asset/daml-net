// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using Com.Daml.Ledger.Api.Util;

using Transaction = Com.Daml.Ledger.Api.Data.Transaction;

namespace Daml.Ledger.Automation.Components.Helpers
{
    /**
     * A {@link Transaction} without the {@link Event}s.
     */
    public class TransactionContext : ICreatedContractContext
    {
        private readonly int _hashCode;

        public TransactionContext(string transactionId, string commandId, string workflowId, DateTimeOffset effectiveAt, string offset)
        {
            TransactionId = transactionId;
            CommandId = commandId;
            WorkflowId = workflowId;
            EffectiveAt = effectiveAt;
            Offset = offset;

            _hashCode = new HashCodeHelper().Add(TransactionId).Add(CommandId).Add(WorkflowId).Add(EffectiveAt).Add(Offset).ToHashCode();
        }

        public static TransactionContext ForTransaction(Transaction transaction) => new TransactionContext(transaction.TransactionId, transaction.CommandId, transaction.WorkflowId, transaction.EffectiveAt, transaction.Offset);

        public string TransactionId { get; }
        public string CommandId { get; }
        public string WorkflowId { get; }
        public DateTimeOffset EffectiveAt { get; }
        public string Offset { get; }

        public override string ToString() => $"TransactionContext{{transactionId='{TransactionId}', commandId='{CommandId}', workflowId='{WorkflowId}', effectiveAt={EffectiveAt}, offset='{Offset}'}}";

        public override bool Equals(object obj) => this.Compare(obj, rhs => _hashCode == rhs._hashCode && TransactionId == rhs.TransactionId && CommandId == rhs.CommandId && WorkflowId == rhs.WorkflowId && EffectiveAt == rhs.EffectiveAt && Offset == rhs.Offset);

        public static bool operator ==(TransactionContext lhs, TransactionContext rhs) => lhs.Compare(rhs);
        public static bool operator !=(TransactionContext lhs, TransactionContext rhs) => !(lhs == rhs);

        public override int GetHashCode() => _hashCode;
    }
}
