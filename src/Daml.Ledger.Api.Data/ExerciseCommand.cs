// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Api.Data 
{
    using Util;

    public class ExerciseCommand : Command
    {
        public ExerciseCommand(Identifier templateId, string contractId, string choice, Value choiceArgument)
        {
            TemplateId = templateId;
            ContractId = contractId;
            Choice = choice;
            ChoiceArgument = choiceArgument;
        }

        public static ExerciseCommand FromProto(Com.DigitalAsset.Ledger.Api.V1.ExerciseCommand command)
        {
            return new ExerciseCommand(Identifier.FromProto(command.TemplateId), command.ContractId, command.Choice, Value.FromProto(command.ChoiceArgument));
        }

        public Com.DigitalAsset.Ledger.Api.V1.ExerciseCommand ToProto()
        {
            return new Com.DigitalAsset.Ledger.Api.V1.ExerciseCommand { TemplateId = TemplateId.ToProto(), ContractId = ContractId, Choice = Choice, ChoiceArgument = ChoiceArgument.ToProto() };
        }

        public override Identifier TemplateId {  get; }

        public string ContractId { get; }

        public string Choice { get; }

        public Value ChoiceArgument { get; }

        public override string ToString() => $"ExerciseCommand{{templateId={TemplateId}, contractId='{ContractId}', choice='{Choice}', choiceArgument={ChoiceArgument}}}";

        public override bool Equals(Command obj) => this.Compare(obj, rhs => TemplateId == rhs.TemplateId && ContractId == rhs.ContractId && Choice == rhs.Choice && ChoiceArgument == rhs.ChoiceArgument);

        public override bool Equals(object obj) => Equals((Command)obj);

        public override int GetHashCode() => (TemplateId, ContractId, Choice, ChoiceArgument).GetHashCode();

        public static bool operator ==(ExerciseCommand lhs, ExerciseCommand rhs) => lhs.Compare(rhs);
        public static bool operator !=(ExerciseCommand lhs, ExerciseCommand rhs) => !(lhs == rhs);
    }
} 
