// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Api.Data 
{
    using Util;

    public class Text : Value
    {
        public Text(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public override Com.Daml.Ledger.Api.V1.Value ToProto() => new Com.Daml.Ledger.Api.V1.Value { Text = Value };

        public override string ToString() => $"Text{{value='{Value}'}}";

        public override bool Equals(object obj) => Equals((Value)obj);
        public override bool Equals(Value obj) => this.Compare(obj, rhs => Value == rhs.Value);
        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(Text lhs, Text rhs) => lhs.Compare(rhs);
        public static bool operator !=(Text lhs, Text rhs) => !(lhs == rhs);
    }
} 
