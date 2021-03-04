// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    using Daml.Ledger.Api.Data.Test.Factories;

    [TestFixture]
    public class FiltersByPartyTest
    {
#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            Assert.IsTrue(FiltersByPartyFactory.Filters1.Equals(FiltersByPartyFactory.Filters1));
            Assert.IsTrue(FiltersByPartyFactory.Filters1 == FiltersByPartyFactory.Filters1);

            Assert.IsTrue(FiltersByPartyFactory.Filters1.Equals(FiltersByPartyFactory.Filters3));
            Assert.IsTrue(FiltersByPartyFactory.Filters1 == FiltersByPartyFactory.Filters3);

            Assert.IsFalse(FiltersByPartyFactory.Filters1.Equals(FiltersByPartyFactory.Filters2));
            Assert.IsTrue(FiltersByPartyFactory.Filters1 != FiltersByPartyFactory.Filters2);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            Assert.IsTrue(FiltersByPartyFactory.Filters1.GetHashCode() == FiltersByPartyFactory.Filters3.GetHashCode());
            Assert.IsTrue(FiltersByPartyFactory.Filters1.GetHashCode() != FiltersByPartyFactory.Filters2.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(FiltersByPartyFactory.Filters1);
        }

        private void ConvertThroughProto(FiltersByParty source)
        {
            Com.Daml.Ledger.Api.V1.TransactionFilter protoValue = source.ToProto();
            var target = FiltersByParty.FromProto(protoValue);
            Assert.IsTrue(source == target);
        }
    }
}



