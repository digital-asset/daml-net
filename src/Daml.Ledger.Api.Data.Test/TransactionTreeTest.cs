// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    using Daml.Ledger.Api.Data.Test.Factories;

    [TestFixture]
    public class TransactionTreeTest
    {
#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {

            Assert.IsTrue(TransactionTreeFactory.TransactionTree1.Equals(TransactionTreeFactory.TransactionTree1));
            Assert.IsTrue(TransactionTreeFactory.TransactionTree1 == TransactionTreeFactory.TransactionTree1);

            Assert.IsTrue(TransactionTreeFactory.TransactionTree1.Equals(TransactionTreeFactory.TransactionTree3));
            Assert.IsTrue(TransactionTreeFactory.TransactionTree1 == TransactionTreeFactory.TransactionTree3);

            Assert.IsFalse(TransactionTreeFactory.TransactionTree1.Equals(TransactionTreeFactory.TransactionTree2));
            Assert.IsTrue(TransactionTreeFactory.TransactionTree1 != TransactionTreeFactory.TransactionTree2);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            Assert.IsTrue(TransactionTreeFactory.TransactionTree1.GetHashCode() == TransactionTreeFactory.TransactionTree3.GetHashCode());
            Assert.IsTrue(TransactionTreeFactory.TransactionTree1.GetHashCode() != TransactionTreeFactory.TransactionTree2.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(TransactionTreeFactory.TransactionTree1);
        }

        private void ConvertThroughProto(TransactionTree source)
        {
            Com.Daml.Ledger.Api.V1.TransactionTree protoValue = source.ToProto();
            TransactionTree target = TransactionTree.FromProto(protoValue);
            Assert.IsTrue(source == target);
        }
    }
}




