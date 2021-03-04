// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    using Daml.Ledger.Api.Data.Test.Factories;

    [TestFixture]
    public class GetActiveContractsResponseTest
    {
        private readonly GetActiveContractsResponse _request1 = new GetActiveContractsResponse("offset1", new[] { CreatedEventFactory.Event1, CreatedEventFactory.Event2 }, "workflow1");
        private readonly GetActiveContractsResponse _request2 = new GetActiveContractsResponse("offset3", new[] { CreatedEventFactory.Event3, CreatedEventFactory.Event1 }, "workflow2");
        private readonly GetActiveContractsResponse _request3 = new GetActiveContractsResponse("offset1", new[] { CreatedEventFactory.Event1, CreatedEventFactory.Event2 }, "workflow1");

        private readonly GetActiveContractsResponse _request4 = new GetActiveContractsResponse("offset4", new[] { CreatedEventFactory.Event1, CreatedEventFactory.Event2 }, "workflow1");
        private readonly GetActiveContractsResponse _request5 = new GetActiveContractsResponse("offset5", new[] { CreatedEventFactory.Event3, CreatedEventFactory.Event1 }, "workflow2");
        private readonly GetActiveContractsResponse _request6 = new GetActiveContractsResponse("offset4", new[] { CreatedEventFactory.Event1, CreatedEventFactory.Event2 }, "workflow1");


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


            Assert.IsTrue(_request4.Equals(_request4));
            Assert.IsTrue(_request4 == _request4);

            Assert.IsTrue(_request4.Equals(_request6));
            Assert.IsTrue(_request4 == _request6);

            Assert.IsFalse(_request4.Equals(_request5));
            Assert.IsTrue(_request4 != _request5);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            Assert.IsTrue(_request1.GetHashCode() == _request3.GetHashCode());
            Assert.IsTrue(_request1.GetHashCode() != _request2.GetHashCode());

            Assert.IsTrue(_request4.GetHashCode() == _request6.GetHashCode());
            Assert.IsTrue(_request4.GetHashCode() != _request5.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(_request1);
            ConvertThroughProto(_request4);
        }

        private void ConvertThroughProto(GetActiveContractsResponse source)
        {
            Com.DigitalAsset.Ledger.Api.V1.GetActiveContractsResponse protoValue = source.ToProto();
            GetActiveContractsResponse target = GetActiveContractsResponse.FromProto(protoValue);
            Assert.IsTrue(source == target);
        }
    }
}