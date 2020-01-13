// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Com.Daml.Ledger.Api.Data.Test.Factories;
using NUnit.Framework;

namespace Com.Daml.Ledger.Api.Data.Test
{
    [TestFixture]
    public class GetTransactionTreesResponseTest
    {
        private readonly GetTransactionTreesResponse _response1 = new GetTransactionTreesResponse(new[] { TransactionTreeFactory.TransactionTree1, TransactionTreeFactory.TransactionTree2 });
        private readonly GetTransactionTreesResponse _response2 = new GetTransactionTreesResponse(new[] { TransactionTreeFactory.TransactionTree3 });
        private readonly GetTransactionTreesResponse _response3 = new GetTransactionTreesResponse(new[] { TransactionTreeFactory.TransactionTree1, TransactionTreeFactory.TransactionTree2 });

#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            Assert.IsTrue(_response1.Equals(_response1));
            Assert.IsTrue(_response1 == _response1);

            Assert.IsTrue(_response1.Equals(_response3));
            Assert.IsTrue(_response1 == _response3);

            Assert.IsFalse(_response1.Equals(_response2));
            Assert.IsTrue(_response1 != _response2);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            Assert.IsTrue(_response1.GetHashCode() == _response3.GetHashCode());
            Assert.IsTrue(_response1.GetHashCode() != _response2.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(_response1);
        }

        private void ConvertThroughProto(GetTransactionTreesResponse source)
        {
            DigitalAsset.Ledger.Api.V1.GetTransactionTreesResponse protoValue = source.ToProto();
            GetTransactionTreesResponse target = GetTransactionTreesResponse.FromProto(protoValue);
            Assert.IsTrue(source == target);
        }
    }
}

