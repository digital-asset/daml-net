// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Daml.Ledger.Api.Data 
{
    using Util;

    using RecordField = Com.DigitalAsset.Ledger.Api.V1.RecordField;

    public sealed class Record : Value
    {
        private readonly int _hashCode;

        public Record(Identifier recordId, params Field[] fields)
         : this(recordId, (IEnumerable<Field>) fields)
        {
        }

        public Record(params Field[] fields)
         : this((IEnumerable<Field>) fields)
        {
        }

        public Record(Identifier recordId, IEnumerable<Field> fields)
         : this(recordId, fields, FieldsListToDictionary(fields))
        {
        }

        public Record(IEnumerable<Field> fields)
         : this(None.Value, fields, FieldsListToDictionary(fields))
        {
        }

        /**
         * @since 2.2.0
         */
        public Record(Optional<Identifier> recordId, IEnumerable<Field> fields, IReadOnlyDictionary<string, Value> fieldsMap)
        {
            RecordId = recordId;
            Fields = fields.ToList().AsReadOnly();
            FieldsMap = ImmutableDictionary.CreateRange(fieldsMap);

            _hashCode = new HashCodeHelper().Add(RecordId).AddRange(Fields).ToHashCode();
        }

        private static IReadOnlyDictionary<string, Value> FieldsListToDictionary(IEnumerable<Field> fields)
        {
            if (!fields.FirstOrNone().Map(f => f.Label).IsPresent)
                return new Dictionary<string, Value>();

            return fields.Select(f => (GetLabel(f), f.Value)).ToDictionary(p => p.Item1, p => p.Item2);
        }

        private static string GetLabel(Field field)
        {
            var label = field.Label.Reduce(string.Empty);
            if (label == string.Empty)
                throw new ApplicationException("Unexpectedly empty label in collection");
            return label;
        }

        public static Record FromProto(Com.DigitalAsset.Ledger.Api.V1.Record record)
        {
            var fields = record.Fields.Select(Field.FromProto);

            var fieldsMap = fields.Where(f => f.Label.IsPresent).Select(f => (f.Label.Reduce(string.Empty), f.Value)).ToDictionary(p => p.Item1, p => p.Item2);

            if (record.RecordId != null)
                return new Record(Identifier.FromProto(record.RecordId), fields, fieldsMap);

            return new Record(None.Value, fields, fieldsMap);
        }

        public override Com.DigitalAsset.Ledger.Api.V1.Value ToProto() => new Com.DigitalAsset.Ledger.Api.V1.Value { Record = ToProtoRecord() };

        public Com.DigitalAsset.Ledger.Api.V1.Record ToProtoRecord()
        {
            Com.DigitalAsset.Ledger.Api.V1.Record record = new Com.DigitalAsset.Ledger.Api.V1.Record();

            RecordId.IfPresent(recordId => record.RecordId = recordId.ToProto());
            record.Fields.Add(from f in Fields select f.ToProto());

            return record;
        }

        public Optional<Identifier> RecordId { get; }

        public IReadOnlyList<Field> Fields { get; }

        /**
         * @return the Map of this Record fields containing the records that have the label
         * @since 2.2.0
         */
        public IImmutableDictionary<string, Value> FieldsMap { get; }

        public override string ToString() => $"Record{{recordId={RecordId}, fields={Fields}}}";

        public override bool Equals(object obj) => Equals((Value) obj);
        public override bool Equals(Value obj) => this.Compare(obj, rhs => _hashCode == rhs._hashCode && RecordId == rhs.RecordId && !Fields.Except(rhs.Fields).Any());
        public override int GetHashCode() => _hashCode;

        public static bool operator ==(Record lhs, Record rhs) => lhs.Compare(rhs);
        public static bool operator !=(Record lhs, Record rhs) => !(lhs == rhs);

        public sealed class Field : IComparable<Field>
        {
            public Field(string label, Value value)
            {
                Label = Optional.OfNullable(label);
                Value = value;

                _hashCode = (Label, Value).GetHashCode();
            }

            public Field(Value value)
            : this(null, value)
            {
            }

            public Optional<string> Label { get; }

            public Value Value { get; }

            public static Field FromProto(RecordField field)
            {
                string label = field.Label;
                Value value = Value.FromProto(field.Value);
                return string.IsNullOrEmpty(label) ? new Field(value) : new Field(label, value);
            }

            public RecordField ToProto()
            {
                var recordField = new RecordField { Value = Value.ToProto() };

                Label.IfPresent(l => recordField.Label = l);

                return recordField;
            }

            public override bool Equals(object obj) => this.Compare(obj, rhs => _hashCode == rhs._hashCode && Label == rhs.Label && Value.Equals(rhs.Value));

            public override int GetHashCode() => _hashCode;

            public static bool operator ==(Field lhs, Field rhs) => lhs.Compare(rhs);
            public static bool operator !=(Field lhs, Field rhs) => !(lhs == rhs);

            public override string ToString() => $"Field{{label={Label}, value={Value}}}";

            public int CompareTo(Field rhs) => _hashCode.CompareTo(rhs._hashCode);

            private readonly int _hashCode;
        }
    }
} 
