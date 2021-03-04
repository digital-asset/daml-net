// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Api.Data.Test
{
    using Util;

    public class BoolTest
    {
        private static readonly Bool _falseBool = new Bool(false);
        private static readonly Bool _trueBool = new Bool(true);
        private static readonly Bool _false2Bool = new Bool(false);

#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {
            Assert.True(_falseBool.Equals(_falseBool));
            Assert.True(_falseBool == _falseBool);

            Assert.True(_falseBool.Equals(_false2Bool));
            Assert.True(_falseBool == _false2Bool);

            Assert.False(_falseBool.Equals(_trueBool));
            Assert.True(_falseBool != _trueBool);
        }
#pragma warning restore CS1718

        [Fact]
        public void HashCodeHasValueSemantics()
        {
            Assert.True(_falseBool.GetHashCode() == _false2Bool.GetHashCode());
            Assert.True(_falseBool.GetHashCode() != _trueBool.GetHashCode());
        }

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(_falseBool);
            ConvertThroughProto(_trueBool);
        }

        private void ConvertThroughProto(Bool source)
        {
            Com.Daml.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsBool();
            maybe.Should().BeOfType<Some<Bool>>();
            Assert.True(source == (Some<Bool>) maybe);
        }
    }
}

