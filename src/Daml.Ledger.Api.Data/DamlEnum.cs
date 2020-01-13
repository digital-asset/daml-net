// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Com.Daml.Ledger.Api.Util;

namespace Com.Daml.Ledger.Api.Data 
{
    public sealed class DamlEnum : Value
    {
        public DamlEnum(Identifier enumId, string constructor)
        {
            EnumId = Optional.Of(enumId);
            Constructor = constructor;
        }

        public DamlEnum(string constructor)
        {
            EnumId = None.Value;
            Constructor = constructor;
        }

        public static DamlEnum FromProto(DigitalAsset.Ledger.Api.V1.Enum value)
        {
            string constructor = value.Constructor;
            if (value.EnumId != null)
            {
                Identifier variantId = Identifier.FromProto(value.EnumId);
                return new DamlEnum(variantId, constructor);
            }

            return new DamlEnum(constructor);
        }

        public Optional<Identifier> EnumId { get; }

        public string Constructor {  get; }

        public override DigitalAsset.Ledger.Api.V1.Value ToProto() => new DigitalAsset.Ledger.Api.V1.Value { Enum = ToProtoEnum() };

        public DigitalAsset.Ledger.Api.V1.Enum ToProtoEnum()
        {
             var val = new DigitalAsset.Ledger.Api.V1.Enum { Constructor = Constructor };
             EnumId.IfPresent(identifier => val.EnumId = identifier.ToProto());
             return val;
        }

        public override bool Equals(object obj) => Equals((Value)obj);
        public override bool Equals(Value obj) => this.Compare(obj, rhs => EnumId == rhs.EnumId && Constructor == rhs.Constructor);
        public override int GetHashCode() => ( EnumId, Constructor ).GetHashCode();

        public static bool operator ==(DamlEnum lhs, DamlEnum rhs) => lhs.Compare(rhs);
        public static bool operator !=(DamlEnum lhs, DamlEnum rhs) => !(lhs == rhs);

        public override string ToString() => $"Enum{{variantId={EnumId}, constructor='{Constructor}'}}";
    }
} 
