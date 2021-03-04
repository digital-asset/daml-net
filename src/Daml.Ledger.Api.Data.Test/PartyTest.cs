// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Api.Data.Test
{
    using Util;

    public class PartyTest
    {
#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {
            var party1 = new Party("Party1");
            var party2 = new Party("Party2");
            var party3 = new Party("Party1");

            Assert.True(party1.Equals(party1));
            Assert.True(party1 == party1);

            Assert.True(party1.Equals(party3));
            Assert.True(party1 == party3);

            Assert.False(party1.Equals(party2));
            Assert.True(party1 != party2);
        }
#pragma warning restore CS1718

        [Fact]
        public void HashCodeHasValueSemantics()
        {
            var party1 = new Party("Party1");
            var party2 = new Party("Party2");
            var party3 = new Party("Party1");

            Assert.True(party1.GetHashCode() == party3.GetHashCode());
            Assert.True(party1.GetHashCode() != party2.GetHashCode());
        }

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(new Party("Party"));
        }

        private void ConvertThroughProto(Party source)
        {
            Com.Daml.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsParty();
            maybe.Should().BeOfType<Some<Party>>();
            Assert.True(source == (Some<Party>)maybe);
        }
    }
}