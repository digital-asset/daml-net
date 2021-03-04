// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Xunit;

namespace Daml.Ledger.Api.Data.Test
{
    using Factories;

    public class TransactionTest
    {
#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {
            Assert.True(TransactionFactory.Transaction1.Equals(TransactionFactory.Transaction1));
            Assert.True(TransactionFactory.Transaction1 == TransactionFactory.Transaction1);

            Assert.True(TransactionFactory.Transaction1.Equals(TransactionFactory.Transaction3));
            Assert.True(TransactionFactory.Transaction1 == TransactionFactory.Transaction3);

            Assert.False(TransactionFactory.Transaction1.Equals(TransactionFactory.Transaction2));
            Assert.True(TransactionFactory.Transaction1 != TransactionFactory.Transaction2);
        }
#pragma warning restore CS1718

        [Fact]
        public void HashCodeHasValueSemantics()
        {
            Assert.True(TransactionFactory.Transaction1.GetHashCode() == TransactionFactory.Transaction3.GetHashCode());
            Assert.True(TransactionFactory.Transaction1.GetHashCode() != TransactionFactory.Transaction2.GetHashCode());
        }

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(TransactionFactory.Transaction1);
        }

        private void ConvertThroughProto(Transaction source)
        {
            Com.Daml.Ledger.Api.V1.Transaction protoValue = source.ToProto();
            Transaction target = Transaction.FromProto(protoValue);
            Assert.True(source == target);
        }
    }
}




