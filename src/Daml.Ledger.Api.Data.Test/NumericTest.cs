// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Com.Daml.Ledger.Api.Util;
using NUnit.Framework;

namespace Com.Daml.Ledger.Api.Data.Test
{
    [TestFixture]
    public class NumericTest
    {
        const string LargeInt = "12345678901234567890123456789012345678";
        const string LargeDecimal = "1234567890123456789012345678.9012345678";

#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            var num1 = new Numeric(BigDecimal.Create("123456.789"));
            var num2 = new Numeric(BigDecimal.Create("223456.789"));
            var num3 = new Numeric(BigDecimal.Create("123456.789"));

            Assert.IsTrue(num1.Equals(num1));
            Assert.IsTrue(num1 == num1);

            Assert.IsTrue(num1.Equals(num3));
            Assert.IsTrue(num1 == num3);

            Assert.IsFalse(num1.Equals(num2));
            Assert.IsTrue(num1 != num2);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            var num1 = new Numeric(BigDecimal.Create("123456.789"));
            var num2 = new Numeric(BigDecimal.Create("223456.789"));
            var num3 = new Numeric(BigDecimal.Create("123456.789"));

            Assert.IsTrue(num1.GetHashCode() == num3.GetHashCode());
            Assert.IsTrue(num1.GetHashCode() != num2.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(new Numeric(BigDecimal.Create("123456.789")));
            ConvertThroughProto(new Numeric(BigDecimal.Create(LargeInt)));
            ConvertThroughProto(new Numeric(BigDecimal.Create(LargeDecimal)));
        }

        private void ConvertThroughProto(Numeric source)
        {
            DigitalAsset.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsNumeric();
            Assert.AreEqual(typeof(Some<Numeric>), maybe.GetType());
            Assert.IsTrue(source == (Some<Numeric>)maybe);
        }
    }
}

