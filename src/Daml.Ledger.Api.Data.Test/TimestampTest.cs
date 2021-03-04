// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Api.Data.Test
{
    using Util;

    public class TimestampTest
    {
#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {
            var time1 = new Timestamp(100000000);
            var time2 = new Timestamp(200000000);
            var time3 = new Timestamp(100000000);

            Assert.True(time1.Equals(time1));
            Assert.True(time1 == time1);

            Assert.True(time1.Equals(time3));
            Assert.True(time1 == time3);

            Assert.False(time1.Equals(time2));
            Assert.True(time1 != time2);
        }
#pragma warning restore CS1718

        [Fact]
        public void HashCodeHasValueSemantics()
        {
            var time1 = new Timestamp(100000000);
            var time2 = new Timestamp(200000000);
            var time3 = new Timestamp(100000000);

            Assert.True(time1.GetHashCode() == time3.GetHashCode());
            Assert.True(time1.GetHashCode() != time2.GetHashCode());
        }

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(new Timestamp(10000000));
        }

        [Fact]
        public void CanCovertThroughDateTimeOffset()
        {
            CovertThroughDateTimeOffset(new DateTimeOffset(DateTime.UtcNow));
            CovertThroughDateTimeOffset(new DateTimeOffset(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)));
        }

        [Fact]
        public void CanCreateFromMillis()
        {
            Timestamp timestamp = Timestamp.FromMillis(1565620091536);

            DateTimeOffset date = timestamp.ToDateTimeOffset();
            date.Should().HaveDay(12);
            date.Should().HaveMonth(8);
            date.Should().HaveYear(2019);
            date.Should().HaveHour(14);
            date.Should().HaveMinute(28);
            date.Should().HaveSecond(11);
        }

        private void ConvertThroughProto(Timestamp source)
        {
            Com.Daml.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsTimestamp();
            maybe.Should().BeOfType<Some<Timestamp>>();
            Assert.True(source == (Some<Timestamp>)maybe);
        }

        private void CovertThroughDateTimeOffset(DateTimeOffset source)
        {
            Timestamp timestamp = Timestamp.FromDateTimeOffset(source);
            DateTimeOffset check = timestamp.ToDateTimeOffset();
            Assert.Equal(source, check);
        }
    }
}