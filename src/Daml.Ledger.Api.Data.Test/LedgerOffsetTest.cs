// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using NUnit.Framework;

namespace Com.Daml.Ledger.Api.Data.Test
{
    [TestFixture]
    public class LedgerOffsetTest
    {
#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            var offset1 = new LedgerOffset.Absolute("offset1");
            var offset2 = new LedgerOffset.Absolute("offset2");
            var offset3 = new LedgerOffset.Absolute("offset1");

            Assert.IsTrue(offset1.Equals(offset1));
            Assert.IsTrue(offset1 == offset1);

            Assert.IsTrue(offset1.Equals(offset3));
            Assert.IsTrue(offset1 == offset3);

            Assert.IsFalse(offset1.Equals(offset2));
            Assert.IsTrue(offset1 != offset2);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            var offset1 = new LedgerOffset.Absolute("offset1");
            var offset2 = new LedgerOffset.Absolute("offset2");
            var offset3 = new LedgerOffset.Absolute("offset1");

            Assert.IsTrue(offset1.GetHashCode() == offset3.GetHashCode());
            Assert.IsTrue(offset1.GetHashCode() != offset2.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(new LedgerOffset.Absolute("offset"));
            ConvertThroughProto(LedgerOffset.LedgerBegin.Instance);
            ConvertThroughProto(LedgerOffset.LedgerEnd.Instance);
        }

        [Test]
        public void LedgerEndHasEquality()
        {
            var offset1 = LedgerOffset.LedgerEnd.Instance;
            var offset2 = LedgerOffset.LedgerEnd.Instance;
            var offset3 = new LedgerOffset.Absolute(offset1.Offset);

            Assert.IsTrue(offset1.Equals(offset2));
            Assert.IsTrue(offset1 == offset2);

            Assert.IsTrue(offset1.Equals(offset3));
            Assert.IsTrue(offset1 == offset3);
        }

        [Test]
        public void CannotCreateEmptyAbsoluteOffset()
        {
            Assert.Throws<ArgumentNullException>(() => new LedgerOffset.Absolute(null));
            Assert.Throws<ArgumentNullException>(() => new LedgerOffset.Absolute(""));
        }

        [Test]
        public void ProtoConversionOfLedgerBoundaryAbsolutesResolvesToledgerBoundaries()
        {
            var offsetBegin = LedgerOffset.FromProto(new LedgerOffset.Absolute(LedgerOffset.LedgerBegin.Instance.Offset).ToProto());
            Assert.AreEqual(typeof(LedgerOffset.LedgerBegin), offsetBegin.GetType());

            var offsetEnd = LedgerOffset.FromProto(new LedgerOffset.Absolute(LedgerOffset.LedgerEnd.Instance.Offset).ToProto());
            Assert.AreEqual(typeof(LedgerOffset.LedgerEnd), offsetEnd.GetType());
        }

        private void ConvertThroughProto(LedgerOffset source)
        {
            DigitalAsset.Ledger.Api.V1.LedgerOffset protoValue = source.ToProto();
            LedgerOffset target = LedgerOffset.FromProto(protoValue);
            Assert.IsTrue(source == target);
        }
    }
}