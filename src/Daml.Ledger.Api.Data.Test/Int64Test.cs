// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    using Util;

    [TestFixture]
    public class Int64Test
    {
#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            var num1 = new Int64(long.MaxValue);
            var num2 = new Int64(long.MinValue);
            var num3 = new Int64(long.MaxValue);

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
            var num1 = new Int64(long.MaxValue);
            var num2 = new Int64(long.MinValue);
            var num3 = new Int64(long.MaxValue);

            Assert.IsTrue(num1.GetHashCode() == num3.GetHashCode());
            Assert.IsTrue(num1.GetHashCode() != num2.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(new Int64(long.MaxValue));
            ConvertThroughProto(new Int64(long.MinValue));
        }
        
        private void ConvertThroughProto(Int64 source)
        {
            Com.DigitalAsset.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsInt64();
            Assert.AreEqual(typeof(Some<Int64>), maybe.GetType());
            Assert.IsTrue(source == (Some<Int64>)maybe);
        }
    }
}

