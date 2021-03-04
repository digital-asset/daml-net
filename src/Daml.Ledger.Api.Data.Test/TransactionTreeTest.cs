// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Xunit;

namespace Daml.Ledger.Api.Data.Test
{
    using Factories;

    public class TransactionTreeTest
    {
#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {

            Assert.True(TransactionTreeFactory.TransactionTree1.Equals(TransactionTreeFactory.TransactionTree1));
            Assert.True(TransactionTreeFactory.TransactionTree1 == TransactionTreeFactory.TransactionTree1);

            Assert.True(TransactionTreeFactory.TransactionTree1.Equals(TransactionTreeFactory.TransactionTree3));
            Assert.True(TransactionTreeFactory.TransactionTree1 == TransactionTreeFactory.TransactionTree3);

            Assert.False(TransactionTreeFactory.TransactionTree1.Equals(TransactionTreeFactory.TransactionTree2));
            Assert.True(TransactionTreeFactory.TransactionTree1 != TransactionTreeFactory.TransactionTree2);
        }
#pragma warning restore CS1718

        [Fact]
        public void HashCodeHasValueSemantics()
        {
            Assert.True(TransactionTreeFactory.TransactionTree1.GetHashCode() == TransactionTreeFactory.TransactionTree3.GetHashCode());
            Assert.True(TransactionTreeFactory.TransactionTree1.GetHashCode() != TransactionTreeFactory.TransactionTree2.GetHashCode());
        }

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(TransactionTreeFactory.TransactionTree1);
        }

        private void ConvertThroughProto(TransactionTree source)
        {
            Com.Daml.Ledger.Api.V1.TransactionTree protoValue = source.ToProto();
            TransactionTree target = TransactionTree.FromProto(protoValue);
            Assert.True(source == target);
        }
    }
}




