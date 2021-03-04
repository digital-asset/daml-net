// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Api.Data 
{
    using Util;

    public class CreateAndExerciseCommand : Command
    {
        public CreateAndExerciseCommand(Identifier templateId, Record createArguments, string choice, Value choiceArgument)
        {
            TemplateId = templateId;
            CreateArguments = createArguments;
            Choice = choice;
            ChoiceArgument = choiceArgument;
        }

        public static CreateAndExerciseCommand FromProto(Com.Daml.Ledger.Api.V1.CreateAndExerciseCommand command)
        {
            return new CreateAndExerciseCommand(Identifier.FromProto(command.TemplateId), Record.FromProto(command.CreateArguments), command.Choice, Value.FromProto(command.ChoiceArgument));
        }

        public Com.Daml.Ledger.Api.V1.CreateAndExerciseCommand ToProto()
        {
            return new Com.Daml.Ledger.Api.V1.CreateAndExerciseCommand { TemplateId = TemplateId.ToProto(), CreateArguments = CreateArguments.ToProtoRecord(),
                                                                             Choice = Choice, ChoiceArgument = ChoiceArgument.ToProto() };
        }

        public override Identifier TemplateId { get; }

        public Record CreateArguments { get; }

        public string Choice { get; }

        public Value ChoiceArgument { get; }

        public override string ToString() => $"CreateAndExerciseCommand{{templateId={TemplateId}, createArguments={CreateArguments}, choice='{Choice}', choiceArgument={ChoiceArgument}}}";

        public override bool Equals(Command obj) => this.Compare(obj, rhs => TemplateId == rhs.TemplateId && CreateArguments == rhs.CreateArguments && Choice == rhs.Choice && ChoiceArgument == rhs.ChoiceArgument);

        public override bool Equals(object obj) => Equals((Command)obj);

        public override int GetHashCode() => ( TemplateId, CreateArguments, Choice, ChoiceArgument ).GetHashCode();

        public static bool operator ==(CreateAndExerciseCommand lhs, CreateAndExerciseCommand rhs) => lhs.Compare(rhs);
        public static bool operator !=(CreateAndExerciseCommand lhs, CreateAndExerciseCommand rhs) => !(lhs == rhs);
    }
} 
