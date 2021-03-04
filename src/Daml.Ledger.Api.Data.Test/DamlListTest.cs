// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Linq;
using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    using Daml.Ledger.Api.Data.Test.Factories;
    using Util;

    [TestFixture]
    public class DamlListTest
    {
        private readonly DamlList _list1 = DamlList.Of(ValuesFactory.Values1[0], ValuesFactory.Values1[1], ValuesFactory.Values1[2]);
        private readonly DamlList _list2 = DamlList.Of(ValuesFactory.Values2[0], ValuesFactory.Values2[1], ValuesFactory.Values2[2]);
        private readonly DamlList _list3 = DamlList.Of(ValuesFactory.Values1);

#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            Assert.IsTrue(_list1.Equals(_list1));
            Assert.IsTrue(_list1 == _list1);

            Assert.IsTrue(_list1.Equals(_list3));
            Assert.IsTrue(_list1 == _list3);

            Assert.IsFalse(_list1.Equals(_list2));
            Assert.IsTrue(_list1 != _list2);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            Assert.IsTrue(_list1.GetHashCode() == _list3.GetHashCode());
            Assert.IsTrue(_list1.GetHashCode() != _list2.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(_list1);
            ConvertThroughProto(DamlList.Of(new Value[] {}));
        }

        [Test]
        public void FieldOrderDoesNotAffectEquality()
        {
            var list = DamlList.Of(ValuesFactory.Values1.Reverse());

            Assert.IsTrue(_list1.Equals(list));
        }

        [Test]
        public void FieldOrderDoesNotAffectHashCode()
        {
            var list1 = DamlList.Of(new Int64(12345), new Party("party1"), new Bool(false));
            var list2 = DamlList.Of(new Party("party1"), new Int64(12345), new Bool(false));

            Assert.IsTrue(list1.GetHashCode() == list2.GetHashCode());
        }

        private void ConvertThroughProto(DamlList source)
        {
            Com.DigitalAsset.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsList();
            Assert.AreEqual(typeof(Some<DamlList>), maybe.GetType());
            Assert.IsTrue(source == (Some<DamlList>)maybe);
        }
    }
}


