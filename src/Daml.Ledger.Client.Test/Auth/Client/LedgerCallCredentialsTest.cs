// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Grpc.Core;
using NUnit.Framework;

namespace Daml.Ledger.Client.Test.Auth.Client
{
    using Daml.Ledger.Client.Auth.Client;

    [TestFixture]
    public class LedgerCallOptionsTest
    {
        [Test]
        public void NullCallOptionReturnedForEmptyAccessToken()
        {
            CallOptions? callOptions = LedgerCallOptions.MakeCallOptions((string) null);
            Assert.IsNull(callOptions);
        }

        [Test]
        public void NullCallOptionReturnedForEmptyAccessTokens()
        {
            CallOptions? callOptions = LedgerCallOptions.MakeCallOptions(new string[] { null, string.Empty, null });
            Assert.IsNull(callOptions);
        }
    }
}