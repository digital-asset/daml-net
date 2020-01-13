// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Com.Daml.Ledger.Api.Data.Test.Factories;
using Daml.Ledger.Automation.Components.Helpers;
using NUnit.Framework;

namespace Daml.Ledger.Automation.Test.components.helpers
{
    [TestFixture]
    public class CreatedContractTest
    {
        private static readonly ICreatedContractContext _context1 = new GetActiveContractsResponseContext("context1");
        private static readonly ICreatedContractContext _context2 = new GetActiveContractsResponseContext("context2");

        private static readonly CreatedContract _contract1 = new CreatedContract(IdentifierFactory.Id1, RecordFactory.Record1, _context1);
        private static readonly CreatedContract _contract2 = new CreatedContract(IdentifierFactory.Id2, RecordFactory.Record2, _context2);
        private static readonly CreatedContract _contract3 = new CreatedContract(IdentifierFactory.Id1, RecordFactory.Record1, _context1);

#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            Assert.IsTrue(_contract1.Equals(_contract1));
            Assert.IsTrue(_contract1 == _contract1);

            Assert.IsTrue(_contract1.Equals(_contract3));
            Assert.IsTrue(_contract1 == _contract3);

            Assert.IsFalse(_contract1.Equals(_contract2));
            Assert.IsTrue(_contract1 != _contract2);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            Assert.IsTrue(_contract1.GetHashCode() == _contract3.GetHashCode());
            Assert.IsTrue(_contract1.GetHashCode() != _contract2.GetHashCode());
        }
    }
}
