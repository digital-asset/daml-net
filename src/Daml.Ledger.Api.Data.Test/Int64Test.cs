// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Api.Data.Test
{
    using Util;

    public class Int64Test
    {
#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {
            var num1 = new Int64(long.MaxValue);
            var num2 = new Int64(long.MinValue);
            var num3 = new Int64(long.MaxValue);

            Assert.True(num1.Equals(num1));
            Assert.True(num1 == num1);

            Assert.True(num1.Equals(num3));
            Assert.True(num1 == num3);

            Assert.False(num1.Equals(num2));
            Assert.True(num1 != num2);
        }
#pragma warning restore CS1718

        [Fact]
        public void HashCodeHasValueSemantics()
        {
            var num1 = new Int64(long.MaxValue);
            var num2 = new Int64(long.MinValue);
            var num3 = new Int64(long.MaxValue);

            Assert.True(num1.GetHashCode() == num3.GetHashCode());
            Assert.True(num1.GetHashCode() != num2.GetHashCode());
        }

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(new Int64(long.MaxValue));
            ConvertThroughProto(new Int64(long.MinValue));
        }
        
        private void ConvertThroughProto(Int64 source)
        {
            Com.Daml.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsInt64();
            maybe.Should().BeOfType<Some<Int64>>();
            Assert.True(source == (Some<Int64>)maybe);
        }
    }
}

