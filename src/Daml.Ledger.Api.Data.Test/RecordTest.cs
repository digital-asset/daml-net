// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Linq;
using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Api.Data.Test
{
    using Factories;
    using Util;

    public class RecordTest
    {
#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {
            Assert.True(RecordFactory.Record1.Equals(RecordFactory.Record1));
            Assert.True(RecordFactory.Record1 == RecordFactory.Record1);

            Assert.True(RecordFactory.Record1.Equals(RecordFactory.Record3));
            Assert.True(RecordFactory.Record1 == RecordFactory.Record3);

            Assert.False(RecordFactory.Record1.Equals(RecordFactory.Record2));
            Assert.True(RecordFactory.Record1 != RecordFactory.Record2);
        }
#pragma warning restore CS1718

        [Fact]
        public void HashCodeHasValueSemantics()
        {
            Assert.True(RecordFactory.Record1.GetHashCode() == RecordFactory.Record3.GetHashCode());
            Assert.True(RecordFactory.Record1.GetHashCode() != RecordFactory.Record2.GetHashCode());
        }

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(RecordFactory.Record1);
            ConvertThroughProto(new Record(IdentifierFactory.Id1, new Record.Field("Label1", new Int64(12345)), new Record.Field("Label2", new Party("party1")), new Record.Field("Label3", new Bool(false))));
        }

        [Fact]
        public void FieldOrderDoesNotAffectEquality()
        {
            var record = new Record(RecordFactory.Record1.RecordId.Reduce(IdentifierFactory.Id1), RecordFactory.Record1.Fields.Reverse());

            Assert.True(record.Equals(RecordFactory.Record1));
        }

        [Fact]
        public void FieldOrderDoesNotAffectHashCode()
        {
            var record = new Record(RecordFactory.Record1.RecordId.Reduce(IdentifierFactory.Id1), RecordFactory.Record1.Fields.Reverse());

            Assert.True(record.GetHashCode() == RecordFactory.Record1.GetHashCode());
        }

        private void ConvertThroughProto(Record source)
        {
            Com.Daml.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsRecord();
            maybe.Should().BeOfType<Some<Record>>();
            Assert.True(source == (Some<Record>)maybe);
        }
    }
}