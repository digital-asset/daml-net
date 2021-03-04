// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    using Util;

    [TestFixture]
    public class DamlOptionalTest
    {
#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            var optional1 = DamlOptional.Of(new Int64(long.MaxValue));
            var optional2 = DamlOptional.Of(new Int64(long.MinValue));
            var optional3 = DamlOptional.Of(new Int64(long.MaxValue));

            Assert.IsTrue(optional1.Equals(optional1));
            Assert.IsTrue(optional1 == optional1);

            Assert.IsTrue(optional1.Equals(optional3));
            Assert.IsTrue(optional1 == optional3);

            Assert.IsFalse(optional1.Equals(optional2));
            Assert.IsTrue(optional1 != optional2);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            var optional1 = DamlOptional.Of(new Int64(long.MaxValue));
            var optional2 = DamlOptional.Of(new Int64(long.MinValue));
            var optional3 = DamlOptional.Of(new Int64(long.MaxValue));

            Assert.IsTrue(optional1.GetHashCode() == optional3.GetHashCode());
            Assert.IsTrue(optional1.GetHashCode() != optional2.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(DamlOptional.Of(null));
            ConvertThroughProto(DamlOptional.Of(new Int64(long.MaxValue)));
        }

        [Test]
        public void CanHandleNullValues()
        {
            var optional = DamlOptional.Of(null);
            var maybe = optional.Value;
            Assert.AreEqual(typeof(None<Value>), maybe.GetType());
            Assert.IsTrue(optional.IsEmpty);
        }

        [Test]
        public void CanHandleNoneOptionalValues()
        {
            var optional = DamlOptional.Of(None.Value);
            var maybe = optional.Value;
            Assert.AreEqual(typeof(None<Value>), maybe.GetType());
            Assert.IsTrue(optional.IsEmpty);
        }

        [Test]
        public void CanHandleOptionalValues()
        {
            var optional = DamlOptional.Of(Optional.Of((Value)new Text("hello")));
            var maybe = optional.Value;
            Assert.AreEqual(typeof(Some<Value>), maybe.GetType());
            Assert.IsFalse(optional.IsEmpty);
        }

        private void ConvertThroughProto(DamlOptional source)
        {
            Com.DigitalAsset.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsOptional();

            Assert.AreEqual(typeof(Some<DamlOptional>), maybe.GetType());
            Assert.IsTrue(source == (Some<DamlOptional>)maybe);
        }
    }
}


