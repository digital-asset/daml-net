// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Api.Data
{
    using Util;

    public class GetLedgerEndResponse
    {
        public GetLedgerEndResponse(LedgerOffset offset)
        {
            Offset = offset;
        }

        public static GetLedgerEndResponse FromProto(Com.DigitalAsset.Ledger.Api.V1.GetLedgerEndResponse response) => new GetLedgerEndResponse(LedgerOffset.FromProto(response.Offset));

        public Com.DigitalAsset.Ledger.Api.V1.GetLedgerEndResponse ToProto() => new Com.DigitalAsset.Ledger.Api.V1.GetLedgerEndResponse { Offset = Offset.ToProto() };

        public LedgerOffset Offset { get; }

        public override string ToString() => $"GetLedgerEndResponse{{offset={Offset}}}";

        public override bool Equals(object obj) => this.Compare(obj, rhs => Offset == rhs.Offset);

        public override int GetHashCode() => Offset.GetHashCode();

        public static bool operator ==(GetLedgerEndResponse lhs, GetLedgerEndResponse rhs) => lhs.Compare(rhs);
        public static bool operator !=(GetLedgerEndResponse lhs, GetLedgerEndResponse rhs) => !(lhs == rhs);
    }
}
