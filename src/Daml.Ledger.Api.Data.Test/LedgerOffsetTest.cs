// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Api.Data.Test
{
    using System;

    public class LedgerOffsetTest
    {
#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {
            var offset1 = new LedgerOffset.Absolute("offset1");
            var offset2 = new LedgerOffset.Absolute("offset2");
            var offset3 = new LedgerOffset.Absolute("offset1");

            Assert.True(offset1.Equals(offset1));
            Assert.True(offset1 == offset1);

            Assert.True(offset1.Equals(offset3));
            Assert.True(offset1 == offset3);

            Assert.False(offset1.Equals(offset2));
            Assert.True(offset1 != offset2);
        }
#pragma warning restore CS1718

        [Fact]
        public void HashCodeHasValueSemantics()
        {
            var offset1 = new LedgerOffset.Absolute("offset1");
            var offset2 = new LedgerOffset.Absolute("offset2");
            var offset3 = new LedgerOffset.Absolute("offset1");

            Assert.True(offset1.GetHashCode() == offset3.GetHashCode());
            Assert.True(offset1.GetHashCode() != offset2.GetHashCode());
        }

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(new LedgerOffset.Absolute("offset"));
            ConvertThroughProto(LedgerOffset.LedgerBegin.Instance);
            ConvertThroughProto(LedgerOffset.LedgerEnd.Instance);
        }

        [Fact]
        public void LedgerEndHasEquality()
        {
            var offset1 = LedgerOffset.LedgerEnd.Instance;
            var offset2 = LedgerOffset.LedgerEnd.Instance;
            var offset3 = new LedgerOffset.Absolute(offset1.Offset);

            Assert.True(offset1.Equals(offset2));
            Assert.True(offset1 == offset2);

            Assert.True(offset1.Equals(offset3));
            Assert.True(offset1 == offset3);
        }

        [Fact]
        public void CannotCreateEmptyAbsoluteOffset()
        {
            Assert.Throws<ArgumentNullException>(() => new LedgerOffset.Absolute(null));
            Assert.Throws<ArgumentNullException>(() => new LedgerOffset.Absolute(""));
        }

        [Fact]
        public void ProtoConversionOfLedgerBoundaryAbsolutesResolvesToledgerBoundaries()
        {
            var offsetBegin = LedgerOffset.FromProto(new LedgerOffset.Absolute(LedgerOffset.LedgerBegin.Instance.Offset).ToProto());
            offsetBegin.Should().BeOfType<LedgerOffset.LedgerBegin>();

            var offsetEnd = LedgerOffset.FromProto(new LedgerOffset.Absolute(LedgerOffset.LedgerEnd.Instance.Offset).ToProto());
            offsetEnd.Should().BeOfType<LedgerOffset.LedgerEnd>();
        }

        private void ConvertThroughProto(LedgerOffset source)
        {
            Com.Daml.Ledger.Api.V1.LedgerOffset protoValue = source.ToProto();
            LedgerOffset target = LedgerOffset.FromProto(protoValue);
            Assert.True(source == target);
        }
    }
}