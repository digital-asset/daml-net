// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Grpc.Core;
using Xunit;
using FluentAssertions;

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

    public class ClientStubTest
    {
        [Fact]
        public void SimpleCallExample()
        {
            var stub = new ClientStub<ServiceClient>(new ServiceClient());

            var response = stub.Dispatch(new MeaningOfLifeRequest(), (c, r, o) => c.WhatIsTheMeaningOfLife(r, o));
            response.MeaningOfLife.Should().Be(42);
        }

        [Fact]
        public void WithNonEmptyAccessTokenGeneratesNewStub()
        {
            var stub = new ClientStub<ServiceClient>(new ServiceClient());

            var stub2 = stub.WithAccess("accessToken");

            stub.Should().NotBe(stub2);
        }

        [Fact]
        public void WithEmptyAccessTokenReturnsSameStub()
        {
            var stub = new ClientStub<ServiceClient>(new ServiceClient());

            var stub2 = stub.WithAccess(null);

            stub.Should().Be(stub2);
        }

        [Fact]
        public void WithDuplicateAccessTokenReturnsSameStub()
        {
            var stub = new ClientStub<ServiceClient>(new ServiceClient(), "accessToken");

            var stub2 = stub.WithAccess("accessToken");

            stub.Should().Be(stub2);
        }
    }
}