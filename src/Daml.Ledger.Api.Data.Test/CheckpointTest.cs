// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using Xunit;

namespace Daml.Ledger.Api.Data.Test
{
    public class CheckpointTest
    {
        private static readonly Checkpoint _checkPoint1 = new Checkpoint(DateTimeOffset.UnixEpoch, LedgerOffset.LedgerBegin.Instance);
        private static readonly Checkpoint _checkPoint2 = new Checkpoint(DateTimeOffset.UtcNow, LedgerOffset.LedgerEnd.Instance);
        private static readonly Checkpoint _checkPoint3 = new Checkpoint(DateTimeOffset.UnixEpoch, LedgerOffset.LedgerBegin.Instance);

#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {
            Assert.True(_checkPoint1.Equals(_checkPoint1));
            Assert.True(_checkPoint1 == _checkPoint1);

            Assert.True(_checkPoint1.Equals(_checkPoint3));
            Assert.True(_checkPoint1 == _checkPoint3);

            Assert.False(_checkPoint1.Equals(_checkPoint2));
            Assert.True(_checkPoint1 != _checkPoint2);
        }
#pragma warning restore CS1718

        [Fact]
        public void HashCodeHasValueSemantics()
        {
            Assert.True(_checkPoint1.GetHashCode() == _checkPoint3.GetHashCode());
            Assert.True(_checkPoint1.GetHashCode() != _checkPoint2.GetHashCode());
        }

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(_checkPoint2);
        }

        private void ConvertThroughProto(Checkpoint source)
        {
            Com.Daml.Ledger.Api.V1.Checkpoint protoValue = source.ToProto();
            Checkpoint target = Checkpoint.FromProto(protoValue);
            Assert.True(source == target);
        }
    }
}


