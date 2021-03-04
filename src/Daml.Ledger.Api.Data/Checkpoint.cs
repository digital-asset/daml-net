// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Daml.Ledger.Api.Data 
{
    using Util;

    public class Checkpoint
    {
        public Checkpoint(DateTimeOffset recordTime, LedgerOffset offset)
        {
            RecordTime = recordTime;
            Offset = offset;
        }

        public static Checkpoint FromProto(Com.Daml.Ledger.Api.V1.Checkpoint checkpoint) => new Checkpoint(checkpoint.RecordTime.ToDateTimeOffset(), LedgerOffset.FromProto(checkpoint.Offset));

        public Com.Daml.Ledger.Api.V1.Checkpoint ToProto() => new Com.Daml.Ledger.Api.V1.Checkpoint { RecordTime = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(RecordTime), Offset = Offset.ToProto() };

        public DateTimeOffset RecordTime { get; }

        public LedgerOffset Offset { get; }

        public override string ToString() => $"Checkpoint{{recordTime={RecordTime}, offset={Offset}}}";

        public override bool Equals(object obj) => this.Compare(obj, rhs => RecordTime == rhs.RecordTime && Offset == rhs.Offset);

        public override int GetHashCode() => ( RecordTime, Offset ).GetHashCode();

        public static bool operator ==(Checkpoint lhs, Checkpoint rhs) => lhs.Compare(rhs);
        public static bool operator !=(Checkpoint lhs, Checkpoint rhs) => !(lhs == rhs);
    }
}
