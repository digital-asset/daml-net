// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using Com.Daml.Ledger.Api.Data.Test.Factories;
using NUnit.Framework;

namespace Com.Daml.Ledger.Api.Data.Test
{
    [TestFixture]
    public class SubmitCommandsRequestTest
    {
        private static readonly DateTimeOffset _now = DateTimeOffset.UtcNow;

        private static readonly SubmitCommandsRequest _request1 = new SubmitCommandsRequest("workflowId1", "applicationId1", "commandId1", "party1", _now, _now.AddDays(5), CreateCommandsFactory.Commands1);
        private static readonly SubmitCommandsRequest _request2 = new SubmitCommandsRequest("workflowId2", "applicationId2", "commandId2", "party2", _now.AddDays(1), _now.AddDays(6), CreateCommandsFactory.Commands2);
        private static readonly SubmitCommandsRequest _request3 = new SubmitCommandsRequest("workflowId1", "applicationId1", "commandId1", "party1", _now, _now.AddDays(5), CreateCommandsFactory.Commands1);

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
        }

        private void ConvertThroughProto(SubmitCommandsRequest source)
        {
            DigitalAsset.Ledger.Api.V1.Commands protoValue = source.ToProto("ledgerId");
            SubmitCommandsRequest target = SubmitCommandsRequest.FromProto(protoValue);
            Assert.IsTrue(source == target);
        }
    }
}




