// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Daml.Ledger.Api.Data 
{
    using Util;

    public class DamlTextMap : Value
    {
        private readonly int _hashCode;

        protected DamlTextMap(IReadOnlyDictionary<string, Value> value)
        {
            Map = ImmutableDictionary.CreateRange(value);
            _hashCode = new HashCodeHelper().AddDictionary(Map).ToHashCode();
        }

        public static DamlTextMap Of(IReadOnlyDictionary<string, Value> value) => new DamlTextMap(value);

        public IImmutableDictionary<string, Value> Map { get; }

        public IEnumerable<KeyValuePair<string, Value>> Stream() => Map;

        public IImmutableDictionary<K, V> ToMap<K, V>(Func<string, K> keyMapper, Func<Value, V> valueMapper)
        {
            return Map.Select(p => new KeyValuePair<K, V>(keyMapper(p.Key), valueMapper(p.Value))).ToImmutableDictionary();
        }

        public IImmutableDictionary<string, V> ToMap<V>(Func<Value, V> valueMapper)
        {
            return Map.Select(p => new KeyValuePair<string, V>(p.Key, valueMapper(p.Value))).ToImmutableDictionary();
        }
        
        public override bool Equals(object obj) => Equals((Value)obj);
        public override bool Equals(Value obj) => this.Compare(obj, rhs => _hashCode == rhs._hashCode && !Map.Values.Except(rhs.Map.Values).Any());
        public override int GetHashCode() => _hashCode; 

        public static bool operator ==(DamlTextMap lhs, DamlTextMap rhs) => lhs.Compare(rhs);
        public static bool operator !=(DamlTextMap lhs, DamlTextMap rhs) => !(lhs == rhs);

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("TextMap{");
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
            var mb = new Com.DigitalAsset.Ledger.Api.V1.Map();
            foreach (var pair in Map)
                mb.Entries.Add(new Com.DigitalAsset.Ledger.Api.V1.Map.Types.Entry { Key = pair.Key, Value = pair.Value.ToProto() });

            return new Com.DigitalAsset.Ledger.Api.V1.Value { Map = mb };
        }

        public static DamlTextMap FromProto(Com.DigitalAsset.Ledger.Api.V1.Map map)
        {
            var textMap = map.Entries.Aggregate(new Dictionary<string, Value>(), (s, p) => { s.Add(p.Key, Value.FromProto(p.Value)); return s; });
            return Of(textMap);
        }
    }
} 
