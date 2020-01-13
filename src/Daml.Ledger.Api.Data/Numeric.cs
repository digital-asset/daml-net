// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Com.Daml.Ledger.Api.Util;

namespace Com.Daml.Ledger.Api.Data 
{
    public class Numeric : Value
    {
        public Numeric(BigDecimal value)
        {
            Value = value;
        }

        public static Numeric FromProto(string number) => new Numeric(BigDecimal.Create(number));

        public override DigitalAsset.Ledger.Api.V1.Value ToProto() => new DigitalAsset.Ledger.Api.V1.Value { Numeric = Value.ToPlainString()};

        public BigDecimal Value { get; }

        public override string ToString() => $"Numeric{{value={Value}}}";

        public override bool Equals(object obj) => Equals((Value)obj);
        public override bool Equals(Value obj) => this.Compare(obj, rhs => Value == rhs.Value);
        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(Numeric lhs, Numeric rhs) => lhs.Compare(rhs);
        public static bool operator !=(Numeric lhs, Numeric rhs) => !(lhs == rhs);
    }
} 
