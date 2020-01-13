// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Com.Daml.Ledger.Api.Util;

namespace Com.Daml.Ledger.Api.Data 
{
    public class Bool : Value
    {
        public Bool(bool value)
        {
            Value = value;
        }

        public static Bool True = new Bool(true);
        public static Bool False = new Bool(false);

        public override DigitalAsset.Ledger.Api.V1.Value ToProto() => new DigitalAsset.Ledger.Api.V1.Value { Bool = Value };

        public bool Value { get; }

        public override string ToString() => $"Bool{{value={Value}}}";

        public override bool Equals(object obj) => Equals((Value) obj);
        public override bool Equals(Value obj) => this.Compare(obj, rhs => Value == rhs.Value);
        public override int GetHashCode() => Value ? 1 : 0;

        public static bool operator ==(Bool lhs, Bool rhs) => lhs.Compare(rhs);
        public static bool operator !=(Bool lhs, Bool rhs) => !(lhs == rhs);
    }
} 
