// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;
    
namespace Daml.Ledger.Api.Data 
{
    using Util;

    public class ExercisedEvent : TreeEvent // Doesn't derive from IEvent as not represented by Event type in base API, instead it has it's own type in the base API : 'ExercisedEvent'
    {
        private readonly int _hashCode;

        public ExercisedEvent(IEnumerable<string> witnessParties,
                              string eventId,
                              Identifier templateId,
                              string contractId,
                              string choice,
                              Value choiceArgument,
                              IEnumerable<string> actingParties,
                              bool consuming,
                              IEnumerable<string> childEventIds,
                              Value exerciseResult)
        {
            WitnessParties = witnessParties.ToList().AsReadOnly();
            EventId = eventId;
            TemplateId = templateId;
            ContractId = contractId;
            Choice = choice;
            ChoiceArgument = choiceArgument;
            ActingParties = actingParties.ToList().AsReadOnly();
            Consuming = consuming;
            ChildEventIds = childEventIds.ToList().AsReadOnly();
            ExerciseResult = exerciseResult;

            _hashCode = new HashCodeHelper().AddRange(WitnessParties).Add(EventId).Add(TemplateId).Add(ContractId).Add(Choice).Add(ChoiceArgument).AddRange(ActingParties).Add(Consuming).AddRange(ChildEventIds).Add(ExerciseResult).ToHashCode();
        }

        public override IReadOnlyList<string> WitnessParties { get; }

        public override string EventId {  get; }

        public override Identifier TemplateId {  get; }

        public override string ContractId {  get; }

        public string Choice {  get; }

        public IReadOnlyList<string> ChildEventIds { get; }

        public Value ChoiceArgument { get; }

        public IReadOnlyList<string> ActingParties {  get; }

        public bool Consuming { get; }

        public Value ExerciseResult { get; }

        public override bool Equals(object obj) => Equals((TreeEvent)obj);
        public override bool Equals(TreeEvent obj) => this.Compare(obj, rhs => _hashCode == rhs._hashCode &&
                                                                               Consuming == rhs.Consuming &&
                                                                               EventId == rhs.EventId &&
                                                                               TemplateId == rhs.TemplateId &&
                                                                               ContractId == rhs.ContractId &&
                                                                               Choice == rhs.Choice &&
                                                                               ChoiceArgument == rhs.ChoiceArgument &&
                                                                               ExerciseResult == rhs.ExerciseResult &&
                                                                               !WitnessParties.Except(rhs.WitnessParties).Any() &&
                                                                               !ActingParties.Except(rhs.ActingParties).Any());

        public override int GetHashCode() => _hashCode;

        public static bool operator ==(ExercisedEvent lhs, ExercisedEvent rhs) => lhs.Compare(rhs);
        public static bool operator !=(ExercisedEvent lhs, ExercisedEvent rhs) => !(lhs == rhs);

        public override string ToString() => $"ExercisedEvent{{witnessParties={WitnessParties}, eventId='{EventId}', templateId={TemplateId}, contractId='{ContractId}', choice='{Choice}', choiceArgument={ChoiceArgument}, actingParties={ActingParties}, consuming={Consuming}, childEventIds={ChildEventIds}, exerciseResult={ExerciseResult}}}";

        public Com.DigitalAsset.Ledger.Api.V1.ExercisedEvent ToProto()
        {
            var exercisedEvent = new Com.DigitalAsset.Ledger.Api.V1.ExercisedEvent { EventId = EventId, Choice = Choice, ChoiceArgument = ChoiceArgument.ToProto(), Consuming = Consuming,
                                                                                 ContractId = ContractId, TemplateId = TemplateId.ToProto(), ExerciseResult = ExerciseResult.ToProto() };
            exercisedEvent.ActingParties.AddRange(ActingParties);
            exercisedEvent.WitnessParties.AddRange(WitnessParties);
            exercisedEvent.ChildEventIds.AddRange(ChildEventIds);

            return exercisedEvent;
        }

        public static ExercisedEvent FromProto(Com.DigitalAsset.Ledger.Api.V1.ExercisedEvent exercisedEvent)
        {
            return new ExercisedEvent(exercisedEvent.WitnessParties.ToList(), 
                                     exercisedEvent.EventId, 
                                     Identifier.FromProto(exercisedEvent.TemplateId),
                                     exercisedEvent.ContractId,
                                     exercisedEvent.Choice,
                                     Value.FromProto(exercisedEvent.ChoiceArgument),
                                     exercisedEvent.ActingParties.ToList(),
                                     exercisedEvent.Consuming,
                                     exercisedEvent.ChildEventIds.ToList(),
                                     Value.FromProto(exercisedEvent.ExerciseResult));
        }
    }
} 
