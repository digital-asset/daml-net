// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using Daml.Ledger.Automation.Components.Helpers;
using NUnit.Framework;

namespace Daml.Ledger.Automation.Test.Components.Helpers
{
    [TestFixture]
    public class TransactionContextTest
    {
        private static readonly DateTimeOffset _now = DateTimeOffset.UtcNow;

        private static readonly TransactionContext _context1 = new TransactionContext("transactionId1", "commandId1", "workflowId1", _now, "offset1");
        private static readonly TransactionContext _context2 = new TransactionContext("transactionId2", "commandId2", "workflowId2", _now.AddDays(1), "offset2");
        private static readonly TransactionContext _context3 = new TransactionContext("transactionId1", "commandId1", "workflowId1", _now, "offset1");

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
