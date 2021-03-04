// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Api.Data 
{
    using Util;

    public class Bool : Value
    {
        public Bool(bool value)
        {
            Value = value;
        }

        public static Bool True = new Bool(true);
        public static Bool False = new Bool(false);

        public override Com.DigitalAsset.Ledger.Api.V1.Value ToProto() => new Com.DigitalAsset.Ledger.Api.V1.Value { Bool = Value };

        public bool Value { get; }

        public override string ToString() => $"Bool{{value={Value}}}";

        public override bool Equals(object obj) => Equals((Value) obj);
        public override bool Equals(Value obj) => this.Compare(obj, rhs => Value == rhs.Value);
        public override int GetHashCode() => Value ? 1 : 0;

        public static bool operator ==(Bool lhs, Bool rhs) => lhs.Compare(rhs);
        public static bool operator !=(Bool lhs, Bool rhs) => !(lhs == rhs);
    }
} 
