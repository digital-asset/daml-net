// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    using Daml.Ledger.Api.Data.Test.Factories;

    [TestFixture]
    public class NoFilterTest
    {
        private readonly Identifier[] _templateIds = { IdentifierFactory.Id1, IdentifierFactory.Id2, IdentifierFactory.Id3 };

#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            var filter = new InclusiveFilter(_templateIds);

            Assert.IsTrue(NoFilter.Instance.Equals(NoFilter.Instance));
            Assert.IsTrue(NoFilter.Instance == NoFilter.Instance);

            Assert.IsFalse(NoFilter.Instance.Equals(filter));
            Assert.IsFalse(filter.Equals(NoFilter.Instance));
            Assert.IsTrue(NoFilter.Instance != filter);
        }
#pragma warning restore CS1718

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(NoFilter.Instance);
        }

        private void ConvertThroughProto(NoFilter source)
        {
            Com.DigitalAsset.Ledger.Api.V1.Filters protoValue = source.ToProto();
            var target = Filter.FromProto(protoValue);
            Assert.IsTrue(source == target);
        }
    }
}


