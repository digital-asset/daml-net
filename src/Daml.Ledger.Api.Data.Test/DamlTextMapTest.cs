// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    using Daml.Ledger.Api.Data.Test.Factories;
    using Util;

    [TestFixture]
    public class DamlTextMapTest
    {
        private static readonly DamlTextMap _map1 = DamlTextMap.Of((from v in ValuesFactory.Values1 select (v.GetType().ToString(), v)).ToDictionary(t => t.Item1, t => t.Item2));
        private static readonly DamlTextMap _map2 = DamlTextMap.Of((from v in ValuesFactory.Values2 select (v.GetType().ToString(), v)).ToDictionary(t => t.Item1, t => t.Item2));
        private static readonly DamlTextMap _map3 = DamlTextMap.Of((from v in ValuesFactory.Values1 select (v.GetType().ToString(), v)).ToDictionary(t => t.Item1, t => t.Item2));

#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            Assert.IsTrue(_map1.Equals(_map1));
            Assert.IsTrue(_map1 == _map1);

            Assert.IsTrue(_map1.Equals(_map3));
            Assert.IsTrue(_map1 == _map3);

            Assert.IsFalse(_map1.Equals(_map2));
            Assert.IsTrue(_map1 != _map2);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            Assert.IsTrue(_map1.GetHashCode() == _map3.GetHashCode());
            Assert.IsTrue(_map1.GetHashCode() != _map2.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(_map1);
            ConvertThroughProto(DamlTextMap.Of(new Dictionary<string, Value>()));
        }

        private void ConvertThroughProto(DamlTextMap source)
        {
            Com.Daml.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsTextMap();
            Assert.AreEqual(typeof(Some<DamlTextMap>), maybe.GetType());
            Assert.IsTrue(source == (Some<DamlTextMap>)maybe);
        }
    }
}


