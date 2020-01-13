// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Com.Daml.Ledger.Api.Data.Test.Factories;
using NUnit.Framework;

namespace Com.Daml.Ledger.Api.Data.Test
{
    [TestFixture]
    public class GetTransactionResponseTest
    {
        private readonly GetTransactionResponse _response1 = new GetTransactionResponse(TransactionTreeFactory.TransactionTree1);
        private readonly GetTransactionResponse _response2 = new GetTransactionResponse(TransactionTreeFactory.TransactionTree2);
        private readonly GetTransactionResponse _response3 = new GetTransactionResponse(TransactionTreeFactory.TransactionTree3);

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

        private void ConvertThroughProto(GetTransactionResponse source)
        {
            DigitalAsset.Ledger.Api.V1.GetTransactionResponse protoValue = source.ToProto();
            GetTransactionResponse target = GetTransactionResponse.FromProto(protoValue);
            Assert.IsTrue(source == target);
        }
    }
}
