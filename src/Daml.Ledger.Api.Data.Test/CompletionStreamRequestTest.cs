// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Linq;
using Com.Daml.Ledger.Api.Data.Test.Factories;
using NUnit.Framework;

namespace Com.Daml.Ledger.Api.Data.Test
{
    [TestFixture]
    public class CompletionStreamRequestTest
    {
        private static readonly CompletionStreamRequest _request1 = new CompletionStreamRequest("ledgerId1", "applicationId1", PartiesFactory.Parties1);
        private static readonly CompletionStreamRequest _request2 = new CompletionStreamRequest("ledgerId2", "applicationId2", PartiesFactory.Parties2, LedgerOffset.LedgerBegin.Instance);
        private static readonly CompletionStreamRequest _request3 = new CompletionStreamRequest("ledgerId1", "applicationId1", PartiesFactory.Parties1);

#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            Assert.IsTrue(_request1.Equals(_request1));
            Assert.IsTrue(_request1 == _request1);

            Assert.IsTrue(_request1.Equals(_request3));
            Assert.IsTrue(_request1 == _request3);

            Assert.IsFalse(_request1.Equals(_request2));
            Assert.IsTrue(_request1 != _request2);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            Assert.IsTrue(_request1.GetHashCode() == _request3.GetHashCode());
            Assert.IsTrue(_request1.GetHashCode() != _request2.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(_request1);
            ConvertThroughProto(_request2);
        }

        [Test]
        public void EqualityIgnoresPartyOrder()
        {
            var request2 = new CompletionStreamRequest("ledgerId1", "applicationId1", PartiesFactory.Parties1.Reverse());

            Assert.IsTrue(_request1.Equals(request2));
            Assert.IsTrue(_request1 == request2);
        }

        private void ConvertThroughProto(CompletionStreamRequest source)
        {
            DigitalAsset.Ledger.Api.V1.CompletionStreamRequest protoValue = source.ToProto();
            CompletionStreamRequest target = CompletionStreamRequest.FromProto(protoValue);
            Assert.IsTrue(source == target);
        }
    }
}


