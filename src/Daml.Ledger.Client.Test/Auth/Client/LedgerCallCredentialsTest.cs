// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Grpc.Core;
using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Client.Test.Auth.Client
{
    using Daml.Ledger.Client.Auth.Client;

    public class LedgerCallOptionsTest
    {
        [Fact]
        public void NullCallOptionReturnedForEmptyAccessToken()
        {
            CallOptions? callOptions = LedgerCallOptions.MakeCallOptions((string) null);
            callOptions.Should().BeNull();
        }

        [Fact]
        public void NullCallOptionReturnedForEmptyAccessTokens()
        {
            CallOptions? callOptions = LedgerCallOptions.MakeCallOptions(new string[] { null, string.Empty, null });
            callOptions.Should().BeNull();
        }
    }
}