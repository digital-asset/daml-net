// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Com.Daml.Ledger.Api.Util;

namespace Com.Daml.Ledger.Api.Data 
{
    public class Unit : Value
    {
        private Unit() {}

        public static Unit Instance { get; } = new Unit();

        public override DigitalAsset.Ledger.Api.V1.Value ToProto() => new DigitalAsset.Ledger.Api.V1.Value { Unit = new Google.Protobuf.WellKnownTypes.Empty() };

        public override bool Equals(object obj) => Equals((Value)obj);
        public override bool Equals(Value obj) => this.Compare(obj, rhs => true);
        public override int GetHashCode() => ToString().GetHashCode();

        public override string ToString() => "Unit{}";
    }
} 
