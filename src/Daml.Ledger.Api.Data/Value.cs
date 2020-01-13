// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using Com.Daml.Ledger.Api.Util;

namespace Com.Daml.Ledger.Api.Data 
{
    public abstract class Value : IComparable<Value>, IEquatable<Value>
    {
        public static Value FromProto(DigitalAsset.Ledger.Api.V1.Value value)
        {
            switch (value.SumCase)
            {
                case DigitalAsset.Ledger.Api.V1.Value.SumOneofCase.Record:
                    return Record.FromProto(value.Record);
                case DigitalAsset.Ledger.Api.V1.Value.SumOneofCase.Variant:
                    return Variant.FromProto(value.Variant);
                case DigitalAsset.Ledger.Api.V1.Value.SumOneofCase.Enum:
                    return DamlEnum.FromProto(value.Enum);
                case DigitalAsset.Ledger.Api.V1.Value.SumOneofCase.ContractId:
                    return new ContractId(value.ContractId);
                case DigitalAsset.Ledger.Api.V1.Value.SumOneofCase.List:
                    return DamlList.FromProto(value.List);
                case DigitalAsset.Ledger.Api.V1.Value.SumOneofCase.Int64:
                    return new Int64(value.Int64);
                case DigitalAsset.Ledger.Api.V1.Value.SumOneofCase.Numeric:
                    return Numeric.FromProto(value.Numeric);
                case DigitalAsset.Ledger.Api.V1.Value.SumOneofCase.Text:
                    return new Text(value.Text);
                case DigitalAsset.Ledger.Api.V1.Value.SumOneofCase.Timestamp:
                    return new Timestamp(value.Timestamp);
                case DigitalAsset.Ledger.Api.V1.Value.SumOneofCase.Party:
                    return new Party(value.Party);
                case DigitalAsset.Ledger.Api.V1.Value.SumOneofCase.Bool:
                    return new Bool(value.Bool);
                case DigitalAsset.Ledger.Api.V1.Value.SumOneofCase.Unit:
                    return Unit.Instance;
                case DigitalAsset.Ledger.Api.V1.Value.SumOneofCase.Date:
                    return new Date(value.Date);
                case DigitalAsset.Ledger.Api.V1.Value.SumOneofCase.Optional:
                    return DamlOptional.FromProto(value.Optional);
                case DigitalAsset.Ledger.Api.V1.Value.SumOneofCase.Map:
                    return DamlTextMap.FromProto(value.Map);
                case DigitalAsset.Ledger.Api.V1.Value.SumOneofCase.GenMap:
                    return DamlGenMap.FromProto(value.GenMap);
                case DigitalAsset.Ledger.Api.V1.Value.SumOneofCase.None:
                    throw new SumNotSetException(value);
                default:
                    throw new UnknownValueException(value);
            }
        }

        public Optional<Bool> AsBool()
        {
            if (this is Bool) 
                return new Some<Bool>((Bool) this);
            return None.Value;
        }

        public Optional<Record> AsRecord()
        {
            if (this is Record)
                return new Some<Record>((Record) this);
            return None.Value;
        }

        public Optional<Variant> AsVariant()
        {
            if (this is Variant)
                return new Some<Variant>((Variant) this);
            return None.Value;
        }

        public Optional<DamlEnum> AsEnum()
        {
            if (this is DamlEnum)
                return new Some<DamlEnum>((DamlEnum) this);
            return None.Value;
        }

        public Optional<ContractId> AsContractId()
        {
            if (this is ContractId)
                return new Some<ContractId>((ContractId)this);
            return None.Value;
        }

        public Optional<DamlList> AsList()
        {
            if (this is DamlList)
                return new Some<DamlList>((DamlList) this);
            return None.Value;
        }

        public Optional<Int64> AsInt64()
        {
            if (this is Int64)
                return new Some<Int64>((Int64) this);
            return None.Value;
        }

        public Optional<Numeric> AsNumeric()
        {
            if (this is Numeric)
                return new Some<Numeric>((Numeric) this);
            return None.Value;
        }

        public Optional<Text> AsText()
        {
            if (this is Text)
                return new Some<Text>((Text) this);
            return None.Value;
        }

        public Optional<Timestamp> AsTimestamp()
        {
            if (this is Timestamp)
                return new Some<Timestamp>((Timestamp) this);
            return None.Value;
        }

        public Optional<Party> AsParty()
        {
            if (this is Party)
                return new Some<Party>((Party)this);
            return None.Value;
        }

        public Optional<Unit> AsUnit()
        {
            if (this is Unit)
                return new Some<Unit>((Unit) this);
            return None.Value;
        }

        public Optional<Date> AsDate()
        {
            if (this is Date)
                return new Some<Date>((Date) this);
            return None.Value;
        }

        public Optional<DamlOptional> AsOptional()
        {
            if (this is DamlOptional)
                return new Some<DamlOptional>((DamlOptional) this);
            return None.Value;
        }

        public Optional<DamlTextMap> AsTextMap()
        {
            if (this is DamlTextMap)
                return new Some<DamlTextMap>((DamlTextMap) this);
            return None.Value;
        }
        public Optional<DamlGenMap> AsGenMap()
        {
            if (this is DamlGenMap)
                return new Some<DamlGenMap>((DamlGenMap)this);
            return None.Value;
        }

        public abstract DigitalAsset.Ledger.Api.V1.Value ToProto();

        public int CompareTo(Value rhs) => GetHashCode().CompareTo(rhs.GetHashCode());

        public static bool operator ==(Value lhs, Value rhs) => lhs.Compare(rhs);
        public static bool operator !=(Value lhs, Value rhs) => !(lhs == rhs);

        public override bool Equals(object obj) => Equals((Value)obj);
        public abstract bool Equals(Value other);

        public abstract override int GetHashCode();
    }

    class SumNotSetException : Exception
    {
        public SumNotSetException(DigitalAsset.Ledger.Api.V1.Value value)
         : base($"Sum not set for value {value}")
        {
        }
    }

    class UnknownValueException : Exception
    {
        public UnknownValueException(DigitalAsset.Ledger.Api.V1.Value value)
         :base($"value unknown {value}")
        {
        }
    }

    class InvalidKeyValue : Exception
    {
        public InvalidKeyValue(DigitalAsset.Ledger.Api.V1.Value value)  
         : base($"invalid key value, expected TEXT, found {value.SumCase.ToString()}")
        { 
        }
    }
} 
