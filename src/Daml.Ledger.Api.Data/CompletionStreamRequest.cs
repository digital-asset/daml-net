// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Com.Daml.Ledger.Api.Util;

namespace Com.Daml.Ledger.Api.Data 
{
    public class CompletionStreamRequest
    {
        private readonly int _hashCode;

        public static CompletionStreamRequest FromProto(DigitalAsset.Ledger.Api.V1.CompletionStreamRequest request)
        {
            return new CompletionStreamRequest(request.LedgerId, request.ApplicationId, request.Parties, request.Offset == null ? None.Value : Optional.Of(Data.LedgerOffset.FromProto(request.Offset)));
        }

        public DigitalAsset.Ledger.Api.V1.CompletionStreamRequest ToProto()
        {
            var request = new DigitalAsset.Ledger.Api.V1.CompletionStreamRequest { LedgerId = LedgerId, ApplicationId = ApplicationId };

            LedgerOffset.IfPresent(l => request.Offset = l.ToProto());
            request.Parties.AddRange(Parties);

            return request;
        }

        public override string ToString() => $"CompletionStreamRequest{{ledgerId='{LedgerId}', applicationId='{ApplicationId}', parties={Parties}, offset={LedgerOffset}}}";

        public override bool Equals(object obj) => this.Compare(obj, rhs => _hashCode == rhs._hashCode && LedgerId == rhs.LedgerId && ApplicationId == rhs.ApplicationId && LedgerOffset == rhs.LedgerOffset && !Parties.Except(rhs.Parties).Any());

        public override int GetHashCode() => _hashCode;

        public static bool operator ==(CompletionStreamRequest lhs, CompletionStreamRequest rhs) => lhs.Compare(rhs);
        public static bool operator !=(CompletionStreamRequest lhs, CompletionStreamRequest rhs) => !(lhs == rhs);

        public string LedgerId { get; }

        public string ApplicationId {  get; }

        public IImmutableSet<string> Parties { get; }

        public Optional<LedgerOffset> LedgerOffset { get; }

        public CompletionStreamRequest(string ledgerId, string applicationId, IEnumerable<string> parties)
         : this(ledgerId, applicationId, parties, None.Value)
        { }

        public CompletionStreamRequest(string ledgerId, string applicationId, IEnumerable<string> parties, LedgerOffset offset)
         : this(ledgerId, applicationId, parties, Optional.Of(offset))
        { }

        private CompletionStreamRequest(string ledgerId, string applicationId, IEnumerable<string> parties, Optional<LedgerOffset> ledgerOffset)
        {
            LedgerId = ledgerId;
            ApplicationId = applicationId;
            Parties = ImmutableHashSet.CreateRange(parties);
            LedgerOffset = ledgerOffset;

            _hashCode = new HashCodeHelper().Add(LedgerId).Add(ApplicationId).AddRange(Parties).Add(LedgerOffset).ToHashCode();
        }
    }
}
