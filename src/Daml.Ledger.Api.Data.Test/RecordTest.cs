// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Linq;
using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    using Daml.Ledger.Api.Data.Test.Factories;
    using Util;

    [TestFixture]
    public class RecordTest
    {
#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            Assert.IsTrue(RecordFactory.Record1.Equals(RecordFactory.Record1));
            Assert.IsTrue(RecordFactory.Record1 == RecordFactory.Record1);

            Assert.IsTrue(RecordFactory.Record1.Equals(RecordFactory.Record3));
            Assert.IsTrue(RecordFactory.Record1 == RecordFactory.Record3);

            Assert.IsFalse(RecordFactory.Record1.Equals(RecordFactory.Record2));
            Assert.IsTrue(RecordFactory.Record1 != RecordFactory.Record2);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            Assert.IsTrue(RecordFactory.Record1.GetHashCode() == RecordFactory.Record3.GetHashCode());
            Assert.IsTrue(RecordFactory.Record1.GetHashCode() != RecordFactory.Record2.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(RecordFactory.Record1);
            ConvertThroughProto(new Record(IdentifierFactory.Id1, new Record.Field("Label1", new Int64(12345)), new Record.Field("Label2", new Party("party1")), new Record.Field("Label3", new Bool(false))));
        }

        [Test]
        public void FieldOrderDoesNotAffectEquality()
        {
            var record = new Record(RecordFactory.Record1.RecordId.Reduce(IdentifierFactory.Id1), RecordFactory.Record1.Fields.Reverse());

            Assert.IsTrue(record.Equals(RecordFactory.Record1));
        }

        [Test]
        public void FieldOrderDoesNotAffectHashCode()
        {
            var record = new Record(RecordFactory.Record1.RecordId.Reduce(IdentifierFactory.Id1), RecordFactory.Record1.Fields.Reverse());

            Assert.IsTrue(record.GetHashCode() == RecordFactory.Record1.GetHashCode());
        }

        private void ConvertThroughProto(Record source)
        {
            Com.DigitalAsset.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsRecord();
            Assert.AreEqual(typeof(Some<Record>), maybe.GetType());
            Assert.IsTrue(source == (Some<Record>)maybe);
        }
    }
}