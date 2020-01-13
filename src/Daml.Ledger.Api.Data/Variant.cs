// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Com.Daml.Ledger.Api.Util;

namespace Com.Daml.Ledger.Api.Data 
{
    public sealed class Variant : Value
    {
        public Variant(Identifier variantId, string constructor, Value value)
        {
            VariantId = Optional.Of(variantId);
            Constructor = constructor;
            Value = value;
        }

        public Variant(string constructor, Value value)
        {
            VariantId = None.Value;
            Constructor = constructor;
            Value = value;
        }

        public static Variant FromProto(DigitalAsset.Ledger.Api.V1.Variant variant)
        {
            Value value = Value.FromProto(variant.Value);

            return variant.VariantId != null ? new Variant(Identifier.FromProto(variant.VariantId), variant.Constructor, value) : new Variant(variant.Constructor, value);
        }
    
        public Optional<Identifier> VariantId { get; }

        public string Constructor { get; }

        public Value Value { get; }

       public override DigitalAsset.Ledger.Api.V1.Value ToProto() => new DigitalAsset.Ledger.Api.V1.Value { Variant = ToProtoVariant() };

        public DigitalAsset.Ledger.Api.V1.Variant ToProtoVariant()
        {
            var variant = new DigitalAsset.Ledger.Api.V1.Variant { Constructor = Constructor, Value = Value.ToProto() };
            VariantId.IfPresent(identifier => variant.VariantId = identifier.ToProto());
            return variant;
        }

        public override bool Equals(object obj) => Equals((Value)obj);
        public override bool Equals(Value obj) => this.Compare(obj, rhs => VariantId == rhs.VariantId && Constructor == rhs.Constructor && Value == rhs.Value);
        public override int GetHashCode() => (VariantId, Constructor, Value).GetHashCode();

        public static bool operator ==(Variant lhs, Variant rhs) => lhs.Compare(rhs);
        public static bool operator !=(Variant lhs, Variant rhs) => !(lhs == rhs);

        public override string ToString() => $"Variant{{variantId={VariantId}, constructor='{Constructor}', value={Value}}}";
    }
} 
