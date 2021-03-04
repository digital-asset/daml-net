// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Linq;
using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Api.Data.Test
{
    using Factories;
    using Util;

    public class DamlListTest
    {
        private readonly DamlList _list1 = DamlList.Of(ValuesFactory.Values1[0], ValuesFactory.Values1[1], ValuesFactory.Values1[2]);
        private readonly DamlList _list2 = DamlList.Of(ValuesFactory.Values2[0], ValuesFactory.Values2[1], ValuesFactory.Values2[2]);
        private readonly DamlList _list3 = DamlList.Of(ValuesFactory.Values1);

#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {
            Assert.True(_list1.Equals(_list1));
            Assert.True(_list1 == _list1);

            Assert.True(_list1.Equals(_list3));
            Assert.True(_list1 == _list3);

            Assert.False(_list1.Equals(_list2));
            Assert.True(_list1 != _list2);
        }
#pragma warning restore CS1718

        [Fact]
        public void HashCodeHasValueSemantics()
        {
            Assert.True(_list1.GetHashCode() == _list3.GetHashCode());
            Assert.True(_list1.GetHashCode() != _list2.GetHashCode());
        }

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(_list1);
            ConvertThroughProto(DamlList.Of(new Value[] {}));
        }

        [Fact]
        public void FieldOrderDoesNotAffectEquality()
        {
            var list = DamlList.Of(ValuesFactory.Values1.Reverse());

            Assert.True(_list1.Equals(list));
        }

        [Fact]
        public void FieldOrderDoesNotAffectHashCode()
        {
            var list1 = DamlList.Of(new Int64(12345), new Party("party1"), new Bool(false));
            var list2 = DamlList.Of(new Party("party1"), new Int64(12345), new Bool(false));

            Assert.True(list1.GetHashCode() == list2.GetHashCode());
        }

        private void ConvertThroughProto(DamlList source)
        {
            Com.Daml.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsList();
            maybe.Should().BeOfType<Some<DamlList>>();
            Assert.True(source == (Some<DamlList>)maybe);
        }
    }
}


