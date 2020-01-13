// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Com.Daml.Ledger.Api.Data.Test.Factories;
using NUnit.Framework;

namespace Com.Daml.Ledger.Api.Data.Test
{
    [TestFixture]
    public class GetActiveContractsRequestTest
    {
#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            var request1 = new GetActiveContractsRequest("ledgerId1", FiltersByPartyFactory.Filters1, true);
            var request2 = new GetActiveContractsRequest("ledgerId2", FiltersByPartyFactory.Filters2, false);
            var request3 = new GetActiveContractsRequest("ledgerId1", FiltersByPartyFactory.Filters1, true);

            Assert.IsTrue(request1.Equals(request1));
            Assert.IsTrue(request1 == request1);

            Assert.IsTrue(request1.Equals(request3));
            Assert.IsTrue(request1 == request3);

            Assert.IsFalse(request1.Equals(request2));
            Assert.IsTrue(request1 != request2);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            var request1 = new GetActiveContractsRequest("ledgerId1", FiltersByPartyFactory.Filters1, true);
            var request2 = new GetActiveContractsRequest("ledgerId2", FiltersByPartyFactory.Filters2, false);
            var request3 = new GetActiveContractsRequest("ledgerId1", FiltersByPartyFactory.Filters1, true);

            Assert.IsTrue(request1.GetHashCode() == request3.GetHashCode());
            Assert.IsTrue(request1.GetHashCode() != request2.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(new GetActiveContractsRequest("ledgerId1", FiltersByPartyFactory.Filters1, true));
        }

        private void ConvertThroughProto(GetActiveContractsRequest source)
        {
            DigitalAsset.Ledger.Api.V1.GetActiveContractsRequest protoValue = source.ToProto();
            GetActiveContractsRequest target = GetActiveContractsRequest.FromProto(protoValue);
            Assert.IsTrue(source == target);
        }
    }
}



