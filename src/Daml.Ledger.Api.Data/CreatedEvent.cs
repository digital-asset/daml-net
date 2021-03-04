// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Daml.Ledger.Api.Data 
{
    using Util;

    public sealed class CreatedEvent : TreeEvent, IEvent
    {
        private readonly int _hashCode;

        public override IReadOnlyList<string> WitnessParties { get; }
        public override string EventId { get; }
        public override Identifier TemplateId { get; }
        public override string ContractId { get; }
        public Record Arguments { get; }
        public Optional<string> AgreementText { get; }
        public Optional<Value> ContractKey { get; }
        public IImmutableSet<string> Signatories { get; }
        public IImmutableSet<string> Observers { get; }

        public CreatedEvent(IEnumerable<string> witnessParties, string eventId, Identifier templateId, string contractId, Record arguments, Optional<string> agreementText, 
                            Optional<Value> contractKey, IEnumerable<string> signatories, IEnumerable<string> observers)
        {
            WitnessParties = witnessParties.ToList().AsReadOnly();
            EventId = eventId;
            TemplateId = templateId;
            ContractId = contractId;
            Arguments = arguments;
            AgreementText = agreementText;
            ContractKey = contractKey;
            Signatories = ImmutableHashSet.CreateRange(signatories);
            Observers = ImmutableHashSet.CreateRange(observers);

            _hashCode = new HashCodeHelper().AddRange(WitnessParties).Add(EventId).Add(TemplateId).Add(ContractId).Add(Arguments).Add(AgreementText).Add(ContractKey).AddRange(Signatories).AddRange(Observers).ToHashCode();
        }

        public override bool Equals(object obj) => Equals((TreeEvent)obj);
        public bool Equals(IEvent obj) => Equals((TreeEvent)obj);
        public override bool Equals(TreeEvent obj) => this.Compare(obj, rhs => _hashCode == rhs._hashCode &&
                                                                               EventId == rhs.EventId &&
                                                                               TemplateId == rhs.TemplateId &&
                                                                               ContractId == rhs.ContractId &&
                                                                               Arguments == rhs.Arguments &&
                                                                               AgreementText == rhs.AgreementText &&
                                                                               ContractKey == rhs.ContractKey &&
                                                                               !WitnessParties.Except(rhs.WitnessParties).Any() &&
                                                                               !Signatories.Except(rhs.Signatories).Any() &&
                                                                               !Observers.Except(rhs.Observers).Any());

        public override int GetHashCode() => _hashCode;

        public int CompareTo(IEvent rhs) => GetHashCode().CompareTo(rhs.GetHashCode());

        public static bool operator ==(CreatedEvent lhs, CreatedEvent rhs) => lhs.Compare(rhs);
        public static bool operator !=(CreatedEvent lhs, CreatedEvent rhs) => !(lhs == rhs);

        public override string ToString()
        {
            return $"CreatedEvent{{witnessParties={WitnessParties}, eventId='{EventId}', templateId={TemplateId}, contractId='{ContractId}', arguments={Arguments}, agreementText='{AgreementText}', contractKey={ContractKey}, signatories={Signatories}, observers={Observers}}}";
        }

        public Com.DigitalAsset.Ledger.Api.V1.CreatedEvent ToProto()
        {
            var createdEvent = new Com.DigitalAsset.Ledger.Api.V1.CreatedEvent
            {
                ContractId = ContractId,
                CreateArguments = Arguments.ToProtoRecord(),
                EventId = EventId,
                TemplateId = TemplateId.ToProto()
            };

            AgreementText.IfPresent(t => createdEvent.AgreementText = t);
            ContractKey.IfPresent(k => createdEvent.ContractKey = k.ToProto());

            createdEvent.WitnessParties.AddRange(WitnessParties);
            createdEvent.Signatories.AddRange(Signatories);
            createdEvent.Observers.AddRange(Observers);

            return createdEvent;
        }

        public static CreatedEvent FromProto(Com.DigitalAsset.Ledger.Api.V1.CreatedEvent createdEvent)
        {
            return new CreatedEvent(createdEvent.WitnessParties, 
                                    createdEvent.EventId, 
                                    Identifier.FromProto(createdEvent.TemplateId), 
                                    createdEvent.ContractId,
                                    Record.FromProto(createdEvent.CreateArguments), 
                                    createdEvent.AgreementText,
                                    createdEvent.ContractKey != null ? Optional.Of(Value.FromProto(createdEvent.ContractKey)) : None.Value, 
                                    createdEvent.Signatories,
                                    createdEvent.Observers);
        }
    }
} 
