﻿// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    using Util;

    [TestFixture]
    public class ContractIdTest
    {
        static readonly string _commonId = Guid.NewGuid().ToString();

#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            var contract1 = new ContractId(_commonId);
            var contract2 = new ContractId(_commonId);
            var contract3 = new ContractId(Guid.NewGuid().ToString());

            Assert.IsTrue(contract1.Equals(contract1));
            Assert.IsTrue(contract1 == contract1);

            Assert.IsTrue(contract1.Equals(contract2));
            Assert.IsTrue(contract1 == contract2);

            Assert.IsFalse(contract1.Equals(contract3));
            Assert.IsTrue(contract1 != contract3);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            var contract1 = new ContractId(_commonId);
            var contract2 = new ContractId(_commonId);
            var contract3 = new ContractId(Guid.NewGuid().ToString());

            Assert.IsTrue(contract1.GetHashCode() == contract2.GetHashCode());
            Assert.IsTrue(contract1.GetHashCode() != contract3.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(new ContractId(_commonId));
        }

        private void ConvertThroughProto(ContractId source)
        {
            Com.Daml.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsContractId();
            Assert.AreEqual(typeof(Some<ContractId>), maybe.GetType());
            Assert.IsTrue(source == (Some<ContractId>)maybe);
        }
    }
}
