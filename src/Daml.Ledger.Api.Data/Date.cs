// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Daml.Ledger.Api.Data 
{
    using Util;

    public class Date : Value
    {
        private const int SecondsInDay = 60 * 60 * 24;

        public Date(int epochDay)
        {
            Value = DateTimeOffset.FromUnixTimeSeconds(epochDay * SecondsInDay).UtcDateTime;
        }

        public override Com.DigitalAsset.Ledger.Api.V1.Value ToProto() => new Com.DigitalAsset.Ledger.Api.V1.Value { Date = (int) new DateTimeOffset(Value).ToUnixTimeSeconds() / SecondsInDay };

        public DateTime Value { get; }

        public override string ToString() => $"Date{{value={Value}}}";

        public override bool Equals(object obj) => Equals((Value)obj);
        public override bool Equals(Value obj) => this.Compare(obj, rhs => Value == rhs.Value);
        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(Date lhs, Date rhs) => lhs.Compare(rhs);
        public static bool operator !=(Date lhs, Date rhs) => !(lhs == rhs);
    }
} 
