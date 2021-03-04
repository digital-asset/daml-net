// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Api.Data 
{
    using Util;

    public class Party : Value
    {
        public Party(string value)
        {
            Value = value;
        }

        public override Com.DigitalAsset.Ledger.Api.V1.Value ToProto() => new Com.DigitalAsset.Ledger.Api.V1.Value { Party = Value };

        public string Value {  get; }

        public override string ToString() => $"Party{{value='{Value}'}}";

        public override bool Equals(object obj) => Equals((Value)obj);
        public override bool Equals(Value obj) => this.Compare(obj, rhs => Value == rhs.Value);
        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(Party lhs, Party rhs) => lhs.Compare(rhs);
        public static bool operator !=(Party lhs, Party rhs) => !(lhs == rhs);
    }
} 
