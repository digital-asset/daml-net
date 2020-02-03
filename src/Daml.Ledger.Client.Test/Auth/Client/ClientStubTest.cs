// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Grpc.Core;
using NUnit.Framework;

namespace Daml.Ledger.Client.Test.Auth.Client
{
    using Daml.Ledger.Client.Auth.Client;

    public class ServiceClient
    {
        public MeaningOfLifeResponse WhatIsTheMeaningOfLife(MeaningOfLifeRequest request, CallOptions callOptions) => new MeaningOfLifeResponse();
    }

    public class MeaningOfLifeRequest
    {
    }

    public class MeaningOfLifeResponse
    {
        public int MeaningOfLife = 42;
    }

    [TestFixture]
    public class ClientStubTest
    {
        [Test]
        public void SimpleCallExample()
        {
            var stub = new ClientStub<ServiceClient>(new ServiceClient());

            var response = stub.Dispatch(new MeaningOfLifeRequest(), (c, r, o) => c.WhatIsTheMeaningOfLife(r, o));
            Assert.AreEqual(42, response.MeaningOfLife);
        }

        [Test]
        public void WithNonEmptyAccessTokenGeneratesNewStub()
        {
            var stub = new ClientStub<ServiceClient>(new ServiceClient());

            var stub2 = stub.WithAccess("accessToken");

            Assert.AreNotEqual(stub, stub2);
        }

        [Test]
        public void WithEmptyAccessTokenReturnsSameStub()
        {
            var stub = new ClientStub<ServiceClient>(new ServiceClient());

            var stub2 = stub.WithAccess(null);

            Assert.AreEqual(stub, stub2);
        }

        [Test]
        public void WithDuplicateAccessTokenReturnsSameStub()
        {
            var stub = new ClientStub<ServiceClient>(new ServiceClient(), "accessToken");

            var stub2 = stub.WithAccess("accessToken");

            Assert.AreEqual(stub, stub2);
        }
    }
}