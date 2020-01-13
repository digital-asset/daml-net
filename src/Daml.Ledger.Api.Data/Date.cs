// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using Com.Daml.Ledger.Api.Util;

namespace Com.Daml.Ledger.Api.Data 
{
    public class Date : Value
    {
        private const int SecondsInDay = 60 * 60 * 24;

        public Date(int epochDay)
        {
            Value = DateTimeOffset.FromUnixTimeSeconds(epochDay * SecondsInDay).UtcDateTime;
        }

        public override DigitalAsset.Ledger.Api.V1.Value ToProto() => new DigitalAsset.Ledger.Api.V1.Value { Date = (int) new DateTimeOffset(Value).ToUnixTimeSeconds() / SecondsInDay };

        public DateTime Value { get; }

        public override string ToString() => $"Date{{value={Value}}}";

        public override bool Equals(object obj) => Equals((Value)obj);
        public override bool Equals(Value obj) => this.Compare(obj, rhs => Value == rhs.Value);
        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(Date lhs, Date rhs) => lhs.Compare(rhs);
        public static bool operator !=(Date lhs, Date rhs) => !(lhs == rhs);
    }
} 
