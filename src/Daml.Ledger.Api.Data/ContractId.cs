// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Com.Daml.Ledger.Api.Util;

namespace Com.Daml.Ledger.Api.Data 
{
    public class ContractId : Value
    {
        public ContractId(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public override DigitalAsset.Ledger.Api.V1.Value ToProto() => new DigitalAsset.Ledger.Api.V1.Value { ContractId = Value };

        public override string ToString() => $"ContractId{{value='{Value}'}}";

        public override bool Equals(object obj) => Equals((Value)obj);
        public override bool Equals(Value obj) => this.Compare(obj, rhs => Value == rhs.Value);
        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(ContractId lhs, ContractId rhs) => lhs.Compare(rhs);
        public static bool operator !=(ContractId lhs, ContractId rhs) => !(lhs == rhs);
    }
} 
