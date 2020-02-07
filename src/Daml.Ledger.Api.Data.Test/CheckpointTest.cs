// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    [TestFixture]
    public class CheckpointTest
    {
        private static readonly Checkpoint _checkPoint1 = new Checkpoint(DateTimeOffset.UnixEpoch, LedgerOffset.LedgerBegin.Instance);
        private static readonly Checkpoint _checkPoint2 = new Checkpoint(DateTimeOffset.UtcNow, LedgerOffset.LedgerEnd.Instance);
        private static readonly Checkpoint _checkPoint3 = new Checkpoint(DateTimeOffset.UnixEpoch, LedgerOffset.LedgerBegin.Instance);

#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            Assert.IsTrue(_checkPoint1.Equals(_checkPoint1));
            Assert.IsTrue(_checkPoint1 == _checkPoint1);

            Assert.IsTrue(_checkPoint1.Equals(_checkPoint3));
            Assert.IsTrue(_checkPoint1 == _checkPoint3);

            Assert.IsFalse(_checkPoint1.Equals(_checkPoint2));
            Assert.IsTrue(_checkPoint1 != _checkPoint2);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            Assert.IsTrue(_checkPoint1.GetHashCode() == _checkPoint3.GetHashCode());
            Assert.IsTrue(_checkPoint1.GetHashCode() != _checkPoint2.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(_checkPoint2);
        }

        private void ConvertThroughProto(Checkpoint source)
        {
            Com.DigitalAsset.Ledger.Api.V1.Checkpoint protoValue = source.ToProto();
            Checkpoint target = Checkpoint.FromProto(protoValue);
            Assert.IsTrue(source == target);
        }
    }
}


