// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using NUnit.Framework;
using Daml.Ledger.Client.Auth.Client;

namespace Daml.Ledger.Client.Test.Auth.Client
{
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