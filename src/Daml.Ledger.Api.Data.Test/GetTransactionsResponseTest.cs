// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    using Daml.Ledger.Api.Data.Test.Factories;

    [TestFixture]
    public class GetTransactionsResponseTest
    {
        private readonly GetTransactionsResponse _response1 = new GetTransactionsResponse(new [] { TransactionFactory.Transaction1, TransactionFactory.Transaction2 });
        private readonly GetTransactionsResponse _response2 = new GetTransactionsResponse(new[] { TransactionFactory.Transaction3 });
        private readonly GetTransactionsResponse _response3 = new GetTransactionsResponse(new[] { TransactionFactory.Transaction1, TransactionFactory.Transaction2 });

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

        private void ConvertThroughProto(GetTransactionsResponse source)
        {
            Com.DigitalAsset.Ledger.Api.V1.GetTransactionsResponse protoValue = source.ToProto();
            GetTransactionsResponse target = GetTransactionsResponse.FromProto(protoValue);
            Assert.IsTrue(source == target);
        }
    }
}

