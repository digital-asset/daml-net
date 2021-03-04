// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Xunit;

namespace Daml.Ledger.Api.Data.Test
{
    using Factories;

    public class NoFilterTest
    {
        private readonly Identifier[] _templateIds = { IdentifierFactory.Id1, IdentifierFactory.Id2, IdentifierFactory.Id3 };

#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {
            var filter = new InclusiveFilter(_templateIds);

            Assert.True(NoFilter.Instance.Equals(NoFilter.Instance));
            Assert.True(NoFilter.Instance == NoFilter.Instance);

            Assert.False(NoFilter.Instance.Equals(filter));
            Assert.False(filter.Equals(NoFilter.Instance));
            Assert.True(NoFilter.Instance != filter);
        }
#pragma warning restore CS1718

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(NoFilter.Instance);
        }

        private void ConvertThroughProto(NoFilter source)
        {
            Com.Daml.Ledger.Api.V1.Filters protoValue = source.ToProto();
            var target = Filter.FromProto(protoValue);
            Assert.True(source == target);
        }
    }
}


