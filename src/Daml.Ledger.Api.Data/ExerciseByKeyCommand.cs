// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Api.Data 
{
    using Util;

    public class ExerciseByKeyCommand : Command
    {
        public ExerciseByKeyCommand(Identifier templateId, Value contractKey, string choice, Value choiceArgument)
        {
            TemplateId = templateId;
            ContractKey = contractKey;
            Choice = choice;
            ChoiceArgument = choiceArgument;
        }

        public static ExerciseByKeyCommand FromProto(Com.DigitalAsset.Ledger.Api.V1.ExerciseByKeyCommand command)
        {
            return new ExerciseByKeyCommand(Identifier.FromProto(command.TemplateId), Value.FromProto(command.ContractKey), command.Choice, Value.FromProto(command.ChoiceArgument));
        }

        public Com.DigitalAsset.Ledger.Api.V1.ExerciseByKeyCommand ToProto()
        {
            return new Com.DigitalAsset.Ledger.Api.V1.ExerciseByKeyCommand { TemplateId = TemplateId.ToProto(), ContractKey = ContractKey.ToProto(), Choice = Choice, ChoiceArgument = ChoiceArgument.ToProto() };
        }

        public override Identifier TemplateId {  get; }

        public Value ContractKey {  get; }

        public string Choice { get; }

        public Value ChoiceArgument {  get; }

        public override string ToString() => $"ExerciseByKeyCommand{{templateId={TemplateId}, contractKey='{ContractKey}', choice='{Choice}', choiceArgument={ChoiceArgument}}}";

        public override bool Equals(Command obj) => this.Compare(obj, rhs => TemplateId == rhs.TemplateId && ContractKey == rhs.ContractKey && Choice == rhs.Choice && ChoiceArgument == rhs.ChoiceArgument);

        public override bool Equals(object obj) => Equals((Command)obj);

        public override int GetHashCode() => (TemplateId, ContractKey, Choice, ChoiceArgument).GetHashCode();

        public static bool operator ==(ExerciseByKeyCommand lhs, ExerciseByKeyCommand rhs) => lhs.Compare(rhs);
        public static bool operator !=(ExerciseByKeyCommand lhs, ExerciseByKeyCommand rhs) => !(lhs == rhs);
    }
} 
