// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Xunit;

namespace Daml.Ledger.Api.Data.Test
{
    using Factories;

    public class GetActiveContractsResponseTest
    {
        private readonly GetActiveContractsResponse _request1 = new GetActiveContractsResponse("offset1", new[] { CreatedEventFactory.Event1, CreatedEventFactory.Event2 }, "workflow1");
        private readonly GetActiveContractsResponse _request2 = new GetActiveContractsResponse("offset3", new[] { CreatedEventFactory.Event3, CreatedEventFactory.Event1 }, "workflow2");
        private readonly GetActiveContractsResponse _request3 = new GetActiveContractsResponse("offset1", new[] { CreatedEventFactory.Event1, CreatedEventFactory.Event2 }, "workflow1");

        private readonly GetActiveContractsResponse _request4 = new GetActiveContractsResponse("offset4", new[] { CreatedEventFactory.Event1, CreatedEventFactory.Event2 }, "workflow1");
        private readonly GetActiveContractsResponse _request5 = new GetActiveContractsResponse("offset5", new[] { CreatedEventFactory.Event3, CreatedEventFactory.Event1 }, "workflow2");
        private readonly GetActiveContractsResponse _request6 = new GetActiveContractsResponse("offset4", new[] { CreatedEventFactory.Event1, CreatedEventFactory.Event2 }, "workflow1");


#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {
            Assert.True(_request1.Equals(_request1));
            Assert.True(_request1 == _request1);

            Assert.True(_request1.Equals(_request3));
            Assert.True(_request1 == _request3);

            Assert.False(_request1.Equals(_request2));
            Assert.True(_request1 != _request2);


            Assert.True(_request4.Equals(_request4));
            Assert.True(_request4 == _request4);

            Assert.True(_request4.Equals(_request6));
            Assert.True(_request4 == _request6);

            Assert.False(_request4.Equals(_request5));
            Assert.True(_request4 != _request5);
        }
#pragma warning restore CS1718

        [Fact]
        public void HashCodeHasValueSemantics()
        {
            Assert.True(_request1.GetHashCode() == _request3.GetHashCode());
            Assert.True(_request1.GetHashCode() != _request2.GetHashCode());

            Assert.True(_request4.GetHashCode() == _request6.GetHashCode());
            Assert.True(_request4.GetHashCode() != _request5.GetHashCode());
        }

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(_request1);
            ConvertThroughProto(_request4);
        }

        private void ConvertThroughProto(GetActiveContractsResponse source)
        {
            Com.Daml.Ledger.Api.V1.GetActiveContractsResponse protoValue = source.ToProto();
            GetActiveContractsResponse target = GetActiveContractsResponse.FromProto(protoValue);
            Assert.True(source == target);
        }
    }
}