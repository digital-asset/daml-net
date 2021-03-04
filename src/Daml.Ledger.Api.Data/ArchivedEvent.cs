// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Daml.Ledger.Api.Data 
{
    using Util;

    public sealed class ArchivedEvent : IEvent
    {
        private readonly int _hashCode;

        public ArchivedEvent(IEnumerable<string> witnessParties,  string eventId,  Identifier templateId,  string contractId)
        {
            WitnessParties = witnessParties.ToList().AsReadOnly();
            EventId = eventId;
            TemplateId = templateId;
            ContractId = contractId;

            _hashCode = new HashCodeHelper().AddRange(WitnessParties).Add(EventId).Add(TemplateId).Add(ContractId).ToHashCode();
        }

        public IReadOnlyList<string> WitnessParties { get; }
        public string EventId { get; }
        public Identifier TemplateId { get; }
        public string ContractId { get; }

        public override bool Equals(object obj) => Equals((IEvent)obj);
        public bool Equals(IEvent obj) => this.Compare(obj, rhs => _hashCode == rhs._hashCode && EventId == rhs.EventId && TemplateId == rhs.TemplateId && ContractId == rhs.ContractId && !WitnessParties.Except(rhs.WitnessParties).Any());

        public int CompareTo(IEvent rhs) => GetHashCode().CompareTo(rhs.GetHashCode());

        public override int GetHashCode() => _hashCode;

        public static bool operator ==(ArchivedEvent lhs, ArchivedEvent rhs) => lhs.Compare(rhs);
        public static bool operator !=(ArchivedEvent lhs, ArchivedEvent rhs) => !(lhs == rhs);

        public override string ToString() => $"ArchivedEvent{{witnessParties={WitnessParties}, eventId='{EventId}', templateId={TemplateId}, contractId='{ContractId}'}}";

        public Com.DigitalAsset.Ledger.Api.V1.ArchivedEvent ToProto()
        {
            var archivedEvent = new Com.DigitalAsset.Ledger.Api.V1.ArchivedEvent { ContractId = ContractId, EventId = EventId, TemplateId = TemplateId.ToProto() };
            archivedEvent.WitnessParties.AddRange(WitnessParties);
            return archivedEvent;
        }

        public static ArchivedEvent FromProto(Com.DigitalAsset.Ledger.Api.V1.ArchivedEvent archivedEvent) => new ArchivedEvent(archivedEvent.WitnessParties, archivedEvent.EventId, Identifier.FromProto(archivedEvent.TemplateId), archivedEvent.ContractId);
    }
} 
