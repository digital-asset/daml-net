// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    using Daml.Ledger.Api.Data.Test.Factories;

    [TestFixture]
    public class InclusiveFilterTest
    {
        private readonly Identifier[] _templateIds1 = { IdentifierFactory.Id1, IdentifierFactory.Id2, IdentifierFactory.Id3 };
        private readonly Identifier[] _templateIds2 = { IdentifierFactory.Id4, IdentifierFactory.Id5, IdentifierFactory.Id6 };

#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            var filter1 = new InclusiveFilter(_templateIds1);
            var filter2 = new InclusiveFilter(_templateIds2);
            var filter3 = new InclusiveFilter(_templateIds1);

            Assert.IsTrue(filter1.Equals(filter1));
            Assert.IsTrue(filter1 == filter1);

            Assert.IsTrue(filter1.Equals(filter3));
            Assert.IsTrue(filter1 == filter3);

            Assert.IsFalse(filter1.Equals(filter2));
            Assert.IsTrue(filter1 != filter2);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            var filter1 = new InclusiveFilter(_templateIds1);
            var filter2 = new InclusiveFilter(_templateIds2);
            var filter3 = new InclusiveFilter(_templateIds1);

            Assert.IsTrue(filter1.GetHashCode() == filter3.GetHashCode());
            Assert.IsTrue(filter1.GetHashCode() != filter2.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(new InclusiveFilter(_templateIds1));
        }

        private void ConvertThroughProto(InclusiveFilter source)
        {
            Com.DigitalAsset.Ledger.Api.V1.Filters protoValue = source.ToProto();
            var target = Filter.FromProto(protoValue);
            Assert.IsTrue(source == target);
        }
    }
}


