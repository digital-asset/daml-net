// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Daml.Ledger.Api.Data 
{
    using Util;

    public abstract class Command : IComparable<Command>, IEquatable<Command>
    {
        public abstract Identifier TemplateId { get; }

        public static Command FromProtoCommand(Com.DigitalAsset.Ledger.Api.V1.Command command)
        {
            switch (command.CommandCase)
            {
                case Com.DigitalAsset.Ledger.Api.V1.Command.CommandOneofCase.Create:
                    return CreateCommand.FromProto(command.Create);
                case Com.DigitalAsset.Ledger.Api.V1.Command.CommandOneofCase.Exercise:
                    return ExerciseCommand.FromProto(command.Exercise);
                case Com.DigitalAsset.Ledger.Api.V1.Command.CommandOneofCase.CreateAndExercise:
                    return CreateAndExerciseCommand.FromProto(command.CreateAndExercise);
                case Com.DigitalAsset.Ledger.Api.V1.Command.CommandOneofCase.ExerciseByKey:
                    return ExerciseByKeyCommand.FromProto(command.ExerciseByKey);
                case Com.DigitalAsset.Ledger.Api.V1.Command.CommandOneofCase.None:
                default:
                    throw new ProtoCommandUnknown(command);
            }
        }

        public Com.DigitalAsset.Ledger.Api.V1.Command ToProtoCommand()
        {
            Com.DigitalAsset.Ledger.Api.V1.Command command = new Com.DigitalAsset.Ledger.Api.V1.Command();

            if (this is CreateCommand) 
                command.Create = ((CreateCommand)this).ToProto();
            else if (this is ExerciseCommand)
                command.Exercise = ((ExerciseCommand)this).ToProto();
            else if (this is CreateAndExerciseCommand) 
                command.CreateAndExercise = ((CreateAndExerciseCommand)this).ToProto();
            else if (this is ExerciseByKeyCommand)
                command.ExerciseByKey = ((ExerciseByKeyCommand)this).ToProto();
            else 
                throw new CommandUnknown(this);

            return command;
        }

        public Optional<CreateCommand> AsCreateCommand() => this is CreateCommand ? Optional.Of((CreateCommand)this) : None.Value;

        public Optional<ExerciseCommand> AsExerciseCommand() => this is ExerciseCommand ? Optional.Of((ExerciseCommand)this) : None.Value;

        public Optional<CreateAndExerciseCommand> AsCreateAndExerciseCommand() => this is CreateAndExerciseCommand ? Optional.Of((CreateAndExerciseCommand)this) : None.Value;

        public Optional<ExerciseByKeyCommand> AsExerciseByKeyCommand() => this is ExerciseByKeyCommand ? Optional.Of((ExerciseByKeyCommand)this) : None.Value;

        public int CompareTo(Command rhs) => GetHashCode().CompareTo(rhs.GetHashCode());

        public override bool Equals(object obj) => Equals((Command)obj);
        public abstract bool Equals(Command other);

        public abstract override int GetHashCode();
    }

    class CommandUnknown : Exception
    {
        public CommandUnknown(Command command)
         : base($"Command unknown {command}")
        { }
    }

    class ProtoCommandUnknown : Exception
    {
        public ProtoCommandUnknown(Com.DigitalAsset.Ledger.Api.V1.Command command)
         : base($"Command unknown {command}")
        { }
    }
} 
