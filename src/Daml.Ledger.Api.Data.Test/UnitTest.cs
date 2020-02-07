// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    using Util;

    [TestFixture]
    public class UnitTest
    {
        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(Unit.Instance);
        }

        private void ConvertThroughProto(Unit source)
        {
            Com.DigitalAsset.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsUnit();
            Assert.AreEqual(typeof(Some<Unit>), maybe.GetType());
            Assert.IsTrue(source == ((Some<Unit>)maybe).Content);
        }
    }
}