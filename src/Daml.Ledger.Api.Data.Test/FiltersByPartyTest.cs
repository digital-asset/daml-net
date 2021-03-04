// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Xunit;

namespace Daml.Ledger.Api.Data.Test
{
    using Factories;

    public class FiltersByPartyTest
    {
#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {
            Assert.True(FiltersByPartyFactory.Filters1.Equals(FiltersByPartyFactory.Filters1));
            Assert.True(FiltersByPartyFactory.Filters1 == FiltersByPartyFactory.Filters1);

            Assert.True(FiltersByPartyFactory.Filters1.Equals(FiltersByPartyFactory.Filters3));
            Assert.True(FiltersByPartyFactory.Filters1 == FiltersByPartyFactory.Filters3);

            Assert.False(FiltersByPartyFactory.Filters1.Equals(FiltersByPartyFactory.Filters2));
            Assert.True(FiltersByPartyFactory.Filters1 != FiltersByPartyFactory.Filters2);
        }
#pragma warning restore CS1718

        [Fact]
        public void HashCodeHasValueSemantics()
        {
            Assert.True(FiltersByPartyFactory.Filters1.GetHashCode() == FiltersByPartyFactory.Filters3.GetHashCode());
            Assert.True(FiltersByPartyFactory.Filters1.GetHashCode() != FiltersByPartyFactory.Filters2.GetHashCode());
        }

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(FiltersByPartyFactory.Filters1);
        }

        private void ConvertThroughProto(FiltersByParty source)
        {
            Com.Daml.Ledger.Api.V1.TransactionFilter protoValue = source.ToProto();
            var target = FiltersByParty.FromProto(protoValue);
            Assert.True(source == target);
        }
    }
}



