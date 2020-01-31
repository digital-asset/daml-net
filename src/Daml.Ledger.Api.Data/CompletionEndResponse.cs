// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Api.Data 
{
    using Util;

    public sealed class CompletionEndResponse
    {
        public CompletionEndResponse(LedgerOffset offset)
        {
            Offset = offset;
        }

        public static CompletionEndResponse FromProto(Com.DigitalAsset.Ledger.Api.V1.CompletionEndResponse response) => new CompletionEndResponse(LedgerOffset.FromProto(response.Offset));

        public LedgerOffset Offset { get; }

        public override string ToString() => $"CompletionEndResponse{{offset={Offset}}}";

        public override bool Equals(object obj) => this.Compare(obj, rhs => Offset == rhs.Offset);

        public override int GetHashCode() => Offset.GetHashCode();

        public static bool operator ==(CompletionEndResponse lhs, CompletionEndResponse rhs) => lhs.Compare(rhs);
        public static bool operator !=(CompletionEndResponse lhs, CompletionEndResponse rhs) => !(lhs == rhs);
    }
}
