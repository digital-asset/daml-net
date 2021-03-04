// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Api.Data.Test
{
    using Util;

    public class UnitTest
    {
        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(Unit.Instance);
        }

        private void ConvertThroughProto(Unit source)
        {
            Com.Daml.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsUnit();
            maybe.Should().BeOfType<Some<Unit>>();
            Assert.True(source == ((Some<Unit>)maybe).Content);
        }
    }
}