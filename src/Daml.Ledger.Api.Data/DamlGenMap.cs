// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Daml.Ledger.Api.Data 
{
    using Util;

    public class DamlGenMap : Value
    {
        private readonly int _hashCode;

        protected DamlGenMap(IReadOnlyDictionary<Value, Value> value)
        {
            Map = ImmutableDictionary.CreateRange(value);
            _hashCode = new HashCodeHelper().AddDictionary(Map).ToHashCode();
        }

        public static DamlGenMap Of(IReadOnlyDictionary<Value, Value> value) => new DamlGenMap(value);

        public IImmutableDictionary<Value, Value> Map { get; }

        public IEnumerable<KeyValuePair<Value, Value>> Stream() => Map;

        public IImmutableDictionary<K, V> ToMap<K, V>(Func<Value, K> keyMapper, Func<Value, V> valueMapper)
        {
            return Map.Select(p => new KeyValuePair<K, V>(keyMapper(p.Key), valueMapper(p.Value))).ToImmutableDictionary();
        }

        public IImmutableDictionary<Value, V> ToMap<V>(Func<Value, V> valueMapper)
        {
            return Map.Select(p => new KeyValuePair<Value, V>(p.Key, valueMapper(p.Value))).ToImmutableDictionary();
        }
        
        public override bool Equals(object obj) => Equals((Value)obj);
        public override bool Equals(Value obj) => this.Compare(obj, rhs => _hashCode == rhs._hashCode && !Map.Values.Except(rhs.Map.Values).Any());
        public override int GetHashCode() => _hashCode; 

        public static bool operator ==(DamlGenMap lhs, DamlGenMap rhs) => lhs.Compare(rhs);
        public static bool operator !=(DamlGenMap lhs, DamlGenMap rhs) => !(lhs == rhs);

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("GenMap{");
            foreach (var pair in Map)
            {
                if (sb.Length > 0)
                    sb.Append(", ");
                sb.Append($"{pair.Key}->{pair.Value}");
            }

            sb.Append("}");
            return sb.ToString();
        }

        public override Com.DigitalAsset.Ledger.Api.V1.Value ToProto()
        {
            var mb = new Com.DigitalAsset.Ledger.Api.V1.GenMap();
            foreach (var pair in Map)
                mb.Entries.Add(new Com.DigitalAsset.Ledger.Api.V1.GenMap.Types.Entry { Key = pair.Key.ToProto(), Value = pair.Value.ToProto() });

            return new Com.DigitalAsset.Ledger.Api.V1.Value { GenMap = mb };
        }

        public static DamlGenMap FromProto(Com.DigitalAsset.Ledger.Api.V1.GenMap map)
        {
            var genMap = map.Entries.Aggregate(new Dictionary<Value, Value>(), (s, p) =>
            {
                s.Add(Value.FromProto(p.Key), Value.FromProto(p.Value));
                return s;
            });

            return Of(genMap);
        }
    }
} 
