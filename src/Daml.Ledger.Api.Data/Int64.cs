// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Api.Data
{
    using Util;

    public class Int64 : Value
    {
        public Int64(long int64)
        {
            Value = int64;
        }

        public long Value { get; }
    
        public override Com.DigitalAsset.Ledger.Api.V1.Value ToProto() => new Com.DigitalAsset.Ledger.Api.V1.Value {Int64 = Value};

        public override string ToString() => $"Int64{{value={Value}}}";

        public override bool Equals(object obj) => Equals((Value)obj);
        public override bool Equals(Value obj) => this.Compare(obj, rhs => Value == rhs.Value);
        public override int GetHashCode() => unchecked((int) Value);

        public static bool operator ==(Int64 lhs, Int64 rhs) => lhs.Compare(rhs);
        public static bool operator !=(Int64 lhs, Int64 rhs) => !(lhs == rhs);
    }
} 
