// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Api.Data.Test
{
    using Util;

    public class DamlOptionalTest
    {
#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {
            var optional1 = DamlOptional.Of(new Int64(long.MaxValue));
            var optional2 = DamlOptional.Of(new Int64(long.MinValue));
            var optional3 = DamlOptional.Of(new Int64(long.MaxValue));

            Assert.True(optional1.Equals(optional1));
            Assert.True(optional1 == optional1);

            Assert.True(optional1.Equals(optional3));
            Assert.True(optional1 == optional3);

            Assert.False(optional1.Equals(optional2));
            Assert.True(optional1 != optional2);
        }
#pragma warning restore CS1718

        [Fact]
        public void HashCodeHasValueSemantics()
        {
            var optional1 = DamlOptional.Of(new Int64(long.MaxValue));
            var optional2 = DamlOptional.Of(new Int64(long.MinValue));
            var optional3 = DamlOptional.Of(new Int64(long.MaxValue));

            Assert.True(optional1.GetHashCode() == optional3.GetHashCode());
            Assert.True(optional1.GetHashCode() != optional2.GetHashCode());
        }

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(DamlOptional.Of(null));
            ConvertThroughProto(DamlOptional.Of(new Int64(long.MaxValue)));
        }

        [Fact]
        public void CanHandleNullValues()
        {
            var optional = DamlOptional.Of(null);
            var maybe = optional.Value;
            maybe.Should().BeOfType<None<Value>>();
            optional.IsEmpty.Should().BeTrue();
        }

        [Fact]
        public void CanHandleNoneOptionalValues()
        {
            var optional = DamlOptional.Of(None.Value);
            var maybe = optional.Value;
            maybe.Should().BeOfType<None<Value>>();
            optional.IsEmpty.Should().BeTrue();
        }

        [Fact]
        public void CanHandleOptionalValues()
        {
            var optional = DamlOptional.Of(Optional.Of((Value)new Text("hello")));
            var maybe = optional.Value;
            maybe.Should().BeOfType<Some<Value>>();
            optional.IsEmpty.Should().BeFalse();
        }

        private void ConvertThroughProto(DamlOptional source)
        {
            Com.Daml.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsOptional();

            maybe.Should().BeOfType<Some<DamlOptional>>();
            Assert.True(source == (Some<DamlOptional>)maybe);
        }
    }
}


