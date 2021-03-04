// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Api.Data.Test
{
    using Util;

    public class DateTest
    {
#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {
            var date1 = new Date(100);
            var date2 = new Date(200);
            var date3 = new Date(100);

            Assert.True(date1.Equals(date1));
            Assert.True(date1 == date1);

            Assert.True(date1.Equals(date3));
            Assert.True(date1 == date3);

            Assert.False(date1.Equals(date2));
            Assert.True(date1 != date2);
        }
#pragma warning restore CS1718

        [Fact]
        public void HashCodeHasValueSemantics()
        {
            var date1 = new Date(100);
            var date2 = new Date(200);
            var date3 = new Date(100);

            Assert.True(date1.GetHashCode() == date3.GetHashCode());
            Assert.True(date1.GetHashCode() != date2.GetHashCode());
        }

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(new Date(100));
        }

        [Fact]
        public void DatesAreCorrectlyBasedOnUnixEpoch()
        {
            var date1 = new Date(0);
            Assert.Equal(1970, date1.Value.Year);
            Assert.Equal(1, date1.Value.Month);
            Assert.Equal(1, date1.Value.Day);

            var date2 = new Date(1);
            Assert.Equal(1970, date2.Value.Year);
            Assert.Equal(1, date2.Value.Month);
            Assert.Equal(2, date2.Value.Day);
        }

        private void ConvertThroughProto(Date source)
        {
            Com.Daml.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsDate();
            maybe.Should().BeOfType<Some<Date>>();
            Assert.True(source == (Some<Date>)maybe);
        }
    }
}

