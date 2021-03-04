// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Xunit;

namespace Daml.Ledger.Api.Data.Test
{
    using Factories;

    public class GetTransactionsResponseTest
    {
        private readonly GetTransactionsResponse _response1 = new GetTransactionsResponse(new [] { TransactionFactory.Transaction1, TransactionFactory.Transaction2 });
        private readonly GetTransactionsResponse _response2 = new GetTransactionsResponse(new[] { TransactionFactory.Transaction3 });
        private readonly GetTransactionsResponse _response3 = new GetTransactionsResponse(new[] { TransactionFactory.Transaction1, TransactionFactory.Transaction2 });

#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {
            Assert.True(_response1.Equals(_response1));
            Assert.True(_response1 == _response1);

            Assert.True(_response1.Equals(_response3));
            Assert.True(_response1 == _response3);

            Assert.False(_response1.Equals(_response2));
            Assert.True(_response1 != _response2);
        }
#pragma warning restore CS1718

        [Fact]
        public void HashCodeHasValueSemantics()
        {
            Assert.True(_response1.GetHashCode() == _response3.GetHashCode());
            Assert.True(_response1.GetHashCode() != _response2.GetHashCode());
        }

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(_response1);
        }

        private void ConvertThroughProto(GetTransactionsResponse source)
        {
            Com.Daml.Ledger.Api.V1.GetTransactionsResponse protoValue = source.ToProto();
            GetTransactionsResponse target = GetTransactionsResponse.FromProto(protoValue);
            Assert.True(source == target);
        }
    }
}

