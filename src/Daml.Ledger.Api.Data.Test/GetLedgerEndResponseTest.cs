// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Com.Daml.Ledger.Api.Data.Test
{
    [TestFixture]
    public class GetLedgerEndResponseTest
    {
        private readonly GetLedgerEndResponse _response1 = new GetLedgerEndResponse(LedgerOffset.LedgerBegin.Instance);
        private readonly GetLedgerEndResponse _response2 = new GetLedgerEndResponse(LedgerOffset.LedgerEnd.Instance);
        private readonly GetLedgerEndResponse _response3 = new GetLedgerEndResponse(LedgerOffset.LedgerBegin.Instance);

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

        private void ConvertThroughProto(GetLedgerEndResponse source)
        {
            DigitalAsset.Ledger.Api.V1.GetLedgerEndResponse protoValue = source.ToProto();
            GetLedgerEndResponse target = GetLedgerEndResponse.FromProto(protoValue);
            Assert.IsTrue(source == target);
        }
    }
}




