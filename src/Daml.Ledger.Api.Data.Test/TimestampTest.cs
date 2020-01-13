// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using Com.Daml.Ledger.Api.Util;
using NUnit.Framework;

namespace Com.Daml.Ledger.Api.Data.Test
{
    [TestFixture]
    public class TimestampTest
    {
#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            var time1 = new Timestamp(100000000);
            var time2 = new Timestamp(200000000);
            var time3 = new Timestamp(100000000);

            Assert.IsTrue(time1.Equals(time1));
            Assert.IsTrue(time1 == time1);

            Assert.IsTrue(time1.Equals(time3));
            Assert.IsTrue(time1 == time3);

            Assert.IsFalse(time1.Equals(time2));
            Assert.IsTrue(time1 != time2);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            var time1 = new Timestamp(100000000);
            var time2 = new Timestamp(200000000);
            var time3 = new Timestamp(100000000);

            Assert.IsTrue(time1.GetHashCode() == time3.GetHashCode());
            Assert.IsTrue(time1.GetHashCode() != time2.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(new Timestamp(10000000));
        }

        [Test]
        public void CanCovertThroughDateTimeOffset()
        {
            CovertThroughDateTimeOffset(new DateTimeOffset(DateTime.UtcNow));
            CovertThroughDateTimeOffset(new DateTimeOffset(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)));
        }

        [Test]
        public void CanCreateFromMillis()
        {
            Timestamp timestamp = Timestamp.FromMillis(1565620091536);

            DateTimeOffset date = timestamp.ToDateTimeOffset();
            Assert.AreEqual(12, date.Day);
            Assert.AreEqual(8, date.Month);
            Assert.AreEqual(2019, date.Year);
            Assert.AreEqual(14, date.Hour);
            Assert.AreEqual(28, date.Minute);
            Assert.AreEqual(11, date.Second);
        }

        private void ConvertThroughProto(Timestamp source)
        {
            DigitalAsset.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsTimestamp();
            Assert.AreEqual(typeof(Some<Timestamp>), maybe.GetType());
            Assert.IsTrue(source == (Some<Timestamp>)maybe);
        }

        private void CovertThroughDateTimeOffset(DateTimeOffset source)
        {
            Timestamp timestamp = Timestamp.FromDateTimeOffset(source);
            DateTimeOffset check = timestamp.ToDateTimeOffset();
            Assert.AreEqual(source, check);
        }
    }
}