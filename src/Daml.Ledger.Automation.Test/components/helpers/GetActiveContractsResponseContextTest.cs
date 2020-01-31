// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Daml.Ledger.Automation.Components.Helpers.Test
{
    [TestFixture]
    public class GetActiveContractsResponseContextTest
    {
        private static readonly GetActiveContractsResponseContext _context1 = new GetActiveContractsResponseContext("context1");
        private static readonly GetActiveContractsResponseContext _context2 = new GetActiveContractsResponseContext("context2");
        private static readonly GetActiveContractsResponseContext _context3 = new GetActiveContractsResponseContext("context1");

#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            Assert.IsTrue(_context1.Equals(_context1));
            Assert.IsTrue(_context1 == _context1);

            Assert.IsTrue(_context1.Equals(_context3));
            Assert.IsTrue(_context1 == _context3);

            Assert.IsFalse(_context1.Equals(_context2));
            Assert.IsTrue(_context1 != _context2);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            Assert.IsTrue(_context1.GetHashCode() == _context3.GetHashCode());
            Assert.IsTrue(_context1.GetHashCode() != _context2.GetHashCode());
        }
    }
}
