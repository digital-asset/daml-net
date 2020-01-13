// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Com.Daml.Ledger.Api.Data.Test
{
    [TestFixture]
    public class CompletionEndResponseTest
    {
        private static readonly CompletionEndResponse _response1 = new CompletionEndResponse(LedgerOffset.LedgerBegin.Instance);
        private static readonly CompletionEndResponse _response2 = new CompletionEndResponse(LedgerOffset.LedgerEnd.Instance);
        private static readonly CompletionEndResponse _response3 = new CompletionEndResponse(LedgerOffset.LedgerBegin.Instance);

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
    }
}



