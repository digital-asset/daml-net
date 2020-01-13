// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using Com.Daml.Ledger.Api.Util;

namespace Com.Daml.Ledger.Api.Data 
{
    public sealed class DamlOptional : Value
    {
        public static readonly DamlOptional Empty = new DamlOptional(None.Value);

        private readonly Value _value;

        private DamlOptional(Value value)
        {
            _value = value;
        }

        private DamlOptional(Optional<Value> value)
         : this(value?.Reduce((DamlOptional)null)) 
        {
        }

        public static DamlOptional Of(Optional<Value> value) => (value is null || !value.IsPresent) ? Empty : new DamlOptional(value);

        public static DamlOptional Of(Value value) => value is null ? Empty : new DamlOptional(value);

        public Optional<T> ToOptional<T>(Func<Value, T> valueMapper) => _value == null ? None.Value : Optional.Of(valueMapper(_value));

        public Optional<Value> Value => Optional.OfNullable(_value);

        public bool IsEmpty => _value is null;

        public override bool Equals(object obj) => Equals((Value) obj);
        public override bool Equals(Value obj) => this.Compare(obj, rhs =>_value is null && rhs._value is null || _value == rhs._value);
        
        public override int GetHashCode() => _value is null ? 0 : _value.GetHashCode();

        public static bool operator ==(DamlOptional lhs, DamlOptional rhs) => lhs.Compare(rhs);
        public static bool operator !=(DamlOptional lhs, DamlOptional rhs) => !(lhs == rhs);

        public override string ToString() => $"Optional{{value={_value?.ToString() ?? "None"}}}";

        public override DigitalAsset.Ledger.Api.V1.Value ToProto() => new DigitalAsset.Ledger.Api.V1.Value { Optional = new DigitalAsset.Ledger.Api.V1.Optional { Value = _value?.ToProto() } };

        public static DamlOptional FromProto(DigitalAsset.Ledger.Api.V1.Optional optional)
        {
            return optional.Value != null ? new DamlOptional(FromProto(optional.Value)) : Empty;
        }
    }
} 
