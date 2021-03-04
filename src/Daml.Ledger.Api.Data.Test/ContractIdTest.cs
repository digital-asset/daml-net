// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Api.Data.Test
{
    using Util;

    public class ContractIdTest
    {
        static readonly string _commonId = Guid.NewGuid().ToString();

#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {
            var contract1 = new ContractId(_commonId);
            var contract2 = new ContractId(_commonId);
            var contract3 = new ContractId(Guid.NewGuid().ToString());

            Assert.True(contract1.Equals(contract1));
            Assert.True(contract1 == contract1);

            Assert.True(contract1.Equals(contract2));
            Assert.True(contract1 == contract2);

            Assert.False(contract1.Equals(contract3));
            Assert.True(contract1 != contract3);
        }
#pragma warning restore CS1718

        [Fact]
        public void HashCodeHasValueSemantics()
        {
            var contract1 = new ContractId(_commonId);
            var contract2 = new ContractId(_commonId);
            var contract3 = new ContractId(Guid.NewGuid().ToString());

            Assert.True(contract1.GetHashCode() == contract2.GetHashCode());
            Assert.True(contract1.GetHashCode() != contract3.GetHashCode());
        }

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(new ContractId(_commonId));
        }

        private void ConvertThroughProto(ContractId source)
        {
            Com.Daml.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsContractId();
            maybe.Should().BeOfType<Some<ContractId>>();
            Assert.True(source == (Some<ContractId>)maybe);
        }
    }
}
