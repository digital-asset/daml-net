// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Com.Daml.Ledger.Api.Util;

namespace Com.Daml.Ledger.Api.Data 
{
    public class FiltersByParty : TransactionFilter
    {
        private readonly int _hashCode;

        public override IImmutableSet<string> Parties { get; }

        public FiltersByParty(IReadOnlyDictionary<string, Filter> partyToFilters)
        {
            PartyToFilters = ImmutableDictionary.CreateRange(partyToFilters);
            Parties = ImmutableHashSet.CreateRange(PartyToFilters.Keys);

            _hashCode = new HashCodeHelper().AddDictionary(PartyToFilters).ToHashCode();
        }

        public override DigitalAsset.Ledger.Api.V1.TransactionFilter ToProto()
        {
            var filter = new DigitalAsset.Ledger.Api.V1.TransactionFilter();
            filter.FiltersByParty.Add(PartyToFilters.Select(p => (p.Key, p.Value.ToProto())).ToDictionary(p => p.Item1, p => p.Item2));
            return filter;
        }

        public new static FiltersByParty FromProto(DigitalAsset.Ledger.Api.V1.TransactionFilter transactionFilter)
        {
            return new FiltersByParty(transactionFilter.FiltersByParty.Select(p => (p.Key, Filter.FromProto(p.Value))).ToDictionary(p => p.Item1, p => p.Item2));
        }

        public IImmutableDictionary<string, Filter> PartyToFilters { get; }

        public override string ToString() => $"FiltersByParty{{partyToFilters={PartyToFilters}}}";

        public override bool Equals(object obj) => Equals((TransactionFilter)obj);
        public override bool Equals(TransactionFilter obj) => this.Compare(obj, rhs => _hashCode == rhs._hashCode && !PartyToFilters.Except(rhs.PartyToFilters).Any());

        public override int GetHashCode() => _hashCode;

        public static bool operator ==(FiltersByParty lhs, FiltersByParty rhs) => lhs.Compare(rhs);
        public static bool operator !=(FiltersByParty lhs, FiltersByParty rhs) => !(lhs == rhs);
    }
} 
