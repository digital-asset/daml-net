// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Linq;

namespace Daml.Ledger.Api.Data
{
    using Util;

    public class DamlList : Value
    {
        private readonly int _hashCode;

        private DamlList()
        {
            Values = new List<Value>().AsReadOnly();
            _hashCode = 0;
        }

        private DamlList(IEnumerable<Value> values)
        {
            Values = values.ToList().AsReadOnly();
            _hashCode = new HashCodeHelper().AddRange(Values).ToHashCode();
        }

        /**
         * The list that is passed to this constructor must not be changed once passed.
         */
        public static DamlList Of(IEnumerable<Value> values) => new DamlList(values);

        public static DamlList Of(params Value[] values) => new DamlList((IEnumerable<Value>)values);

        public IReadOnlyList<Value> Values { get; }

        public IEnumerable<Value> Stream() => Values;

        public List<T> ToList<T>(Func<Value, T> valueMapper) => Values.Select(valueMapper).ToList();
        
        public override Com.DigitalAsset.Ledger.Api.V1.Value ToProto()
        {
            var list = new Com.DigitalAsset.Ledger.Api.V1.List();
            list.Elements.AddRange(from v in Values select v.ToProto());
            return new Com.DigitalAsset.Ledger.Api.V1.Value { List = list };
        }

        public static DamlList FromProto(Com.DigitalAsset.Ledger.Api.V1.List list) => new DamlList(new List<Value>(from v in list.Elements select Value.FromProto(v)));

        public override bool Equals(object obj) => Equals((Value)obj);
        public override bool Equals(Value obj) => this.Compare(obj, rhs => _hashCode == rhs._hashCode && !Values.Except(rhs.Values).Any());
        public override int GetHashCode() => _hashCode;

        public static bool operator ==(DamlList lhs, DamlList rhs) => lhs.Compare(rhs);
        public static bool operator !=(DamlList lhs, DamlList rhs) => !(lhs == rhs);

        public override string ToString() => $"DamlList{{values={Values}}}";
    }
} 
