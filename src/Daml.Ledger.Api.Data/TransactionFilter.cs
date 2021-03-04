// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Immutable;

namespace Daml.Ledger.Api.Data 
{
    using Util;

    public abstract class TransactionFilter : IComparable<TransactionFilter>, IEquatable<TransactionFilter>
    {
        public static TransactionFilter FromProto(Com.Daml.Ledger.Api.V1.TransactionFilter transactionFilter)
        {
            // at the moment, the only transaction filter supported is FiltersByParty
            return FiltersByParty.FromProto(transactionFilter);
        }

        public abstract Com.Daml.Ledger.Api.V1.TransactionFilter ToProto();

        public abstract IImmutableSet<string> Parties { get; }

        public int CompareTo(TransactionFilter rhs) => GetHashCode().CompareTo(rhs.GetHashCode());

        public static bool operator ==(TransactionFilter lhs, TransactionFilter rhs) => lhs.Compare(rhs);
        public static bool operator !=(TransactionFilter lhs, TransactionFilter rhs) => !(lhs == rhs);

        public override bool Equals(object obj) => Equals((Filter)obj);
        public abstract bool Equals(TransactionFilter other);

        public abstract override int GetHashCode();
    }
} 
