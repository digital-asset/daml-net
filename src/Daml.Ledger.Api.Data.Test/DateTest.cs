// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Com.Daml.Ledger.Api.Util;
using NUnit.Framework;

namespace Com.Daml.Ledger.Api.Data.Test
{
    [TestFixture]
    public class DateTest
    {
#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            var date1 = new Date(100);
            var date2 = new Date(200);
            var date3 = new Date(100);

            Assert.IsTrue(date1.Equals(date1));
            Assert.IsTrue(date1 == date1);

            Assert.IsTrue(date1.Equals(date3));
            Assert.IsTrue(date1 == date3);

            Assert.IsFalse(date1.Equals(date2));
            Assert.IsTrue(date1 != date2);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            var date1 = new Date(100);
            var date2 = new Date(200);
            var date3 = new Date(100);

            Assert.IsTrue(date1.GetHashCode() == date3.GetHashCode());
            Assert.IsTrue(date1.GetHashCode() != date2.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(new Date(100));
        }

        [Test]
        public void DatesAreCorrectlyBasedOnUnixEpoch()
        {
            var date1 = new Date(0);
            Assert.AreEqual(1970, date1.Value.Year);
            Assert.AreEqual(1, date1.Value.Month);
            Assert.AreEqual(1, date1.Value.Day);

            var date2 = new Date(1);
            Assert.AreEqual(1970, date2.Value.Year);
            Assert.AreEqual(1, date2.Value.Month);
            Assert.AreEqual(2, date2.Value.Day);
        }

        private void ConvertThroughProto(Date source)
        {
            DigitalAsset.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsDate();
            Assert.AreEqual(typeof(Some<Date>), maybe.GetType());
            Assert.IsTrue(source == (Some<Date>)maybe);
        }
    }
}

