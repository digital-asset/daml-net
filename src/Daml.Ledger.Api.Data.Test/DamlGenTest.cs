// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Api.Data.Test
{
    using Factories;
    using Util;

    public class DamlGenMapTest
    {
        private static readonly DamlGenMap _map1 = DamlGenMap.Of((from v in ValuesFactory.Values1 select (v, v)).ToDictionary(t => t.Item1, t => t.Item2));
        private static readonly DamlGenMap _map2 = DamlGenMap.Of((from v in ValuesFactory.Values2 select (v, v)).ToDictionary(t => t.Item1, t => t.Item2));
        private static readonly DamlGenMap _map3 = DamlGenMap.Of((from v in ValuesFactory.Values1 select (v, v)).ToDictionary(t => t.Item1, t => t.Item2));

#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {
            Assert.True(_map1.Equals(_map1));
            Assert.True(_map1 == _map1);

            Assert.True(_map1.Equals(_map3));
            Assert.True(_map1 == _map3);

            Assert.False(_map1.Equals(_map2));
            Assert.True(_map1 != _map2);
        }
#pragma warning restore CS1718

        [Fact]
        public void HashCodeHasValueSemantics()
        {
            Assert.True(_map1.GetHashCode() == _map3.GetHashCode());
            Assert.True(_map1.GetHashCode() != _map2.GetHashCode());
        }

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(_map1);
            ConvertThroughProto(DamlGenMap.Of(new Dictionary<Value, Value>()));
        }

        private void ConvertThroughProto(DamlGenMap source)
        {
            Com.Daml.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsGenMap();
            maybe.Should().BeOfType<Some<DamlGenMap>>();
            Assert.True(source == (Some<DamlGenMap>)maybe);
        }
    }
}


