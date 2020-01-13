// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using NUnit.Framework;

namespace Com.Daml.Ledger.Api.Data.Codegen.Test
{
    public class Foo
    { }

    public class Bar : Foo
    { }

    [TestFixture]
    public class ContractIdTest
    {
#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemanticsAcrossTypes()
        {
            string commonId = Guid.NewGuid().ToString();

            var fooId = new ContractId<Foo>(commonId);
            var barId = new ContractId<Bar>(commonId);
            var barId2= new ContractId<Bar>(Guid.NewGuid().ToString());

            Assert.IsTrue(fooId.Equals(fooId));
            Assert.IsTrue(fooId.Equals(barId));

            Assert.IsFalse(barId.Equals(barId2));
            Assert.IsFalse(fooId.Equals(barId2));
        }
#pragma warning restore CS1718

        [Test]
        public void CanConvertToValue()
        {
            string commonId = Guid.NewGuid().ToString();

            var fooId = new ContractId<Foo>(commonId);

            var value = fooId.ToValue();

            Assert.AreEqual(commonId, value.AsContractId().Reduce(new ContractId("missing")).Value);
        }
    }
}
