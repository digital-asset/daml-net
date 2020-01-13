// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Com.Daml.Ledger.Api.Util;
using NUnit.Framework;

namespace Com.Daml.Ledger.Api.Data.Test
{
    [TestFixture]
    public class BoolTest
    {
        private static readonly Bool _falseBool = new Bool(false);
        private static readonly Bool _trueBool = new Bool(true);
        private static readonly Bool _false2Bool = new Bool(false);

#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            Assert.IsTrue(_falseBool.Equals(_falseBool));
            Assert.IsTrue(_falseBool == _falseBool);

            Assert.IsTrue(_falseBool.Equals(_false2Bool));
            Assert.IsTrue(_falseBool == _false2Bool);

            Assert.IsFalse(_falseBool.Equals(_trueBool));
            Assert.IsTrue(_falseBool != _trueBool);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            Assert.IsTrue(_falseBool.GetHashCode() == _false2Bool.GetHashCode());
            Assert.IsTrue(_falseBool.GetHashCode() != _trueBool.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(_falseBool);
            ConvertThroughProto(_trueBool);
        }

        private void ConvertThroughProto(Bool source)
        {
            DigitalAsset.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsBool();
            Assert.AreEqual(typeof(Some<Bool>), maybe.GetType());
            Assert.IsTrue(source == (Some<Bool>) maybe);
        }
    }
}

