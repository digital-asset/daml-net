// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    using Daml.Ledger.Api.Data.Test.Factories;

    [TestFixture]
    public class TransactionTest
    {
#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            Assert.IsTrue(TransactionFactory.Transaction1.Equals(TransactionFactory.Transaction1));
            Assert.IsTrue(TransactionFactory.Transaction1 == TransactionFactory.Transaction1);

            Assert.IsTrue(TransactionFactory.Transaction1.Equals(TransactionFactory.Transaction3));
            Assert.IsTrue(TransactionFactory.Transaction1 == TransactionFactory.Transaction3);

            Assert.IsFalse(TransactionFactory.Transaction1.Equals(TransactionFactory.Transaction2));
            Assert.IsTrue(TransactionFactory.Transaction1 != TransactionFactory.Transaction2);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            Assert.IsTrue(TransactionFactory.Transaction1.GetHashCode() == TransactionFactory.Transaction3.GetHashCode());
            Assert.IsTrue(TransactionFactory.Transaction1.GetHashCode() != TransactionFactory.Transaction2.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(TransactionFactory.Transaction1);
        }

        private void ConvertThroughProto(Transaction source)
        {
            Com.DigitalAsset.Ledger.Api.V1.Transaction protoValue = source.ToProto();
            Transaction target = Transaction.FromProto(protoValue);
            Assert.IsTrue(source == target);
        }
    }
}




