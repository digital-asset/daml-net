// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Api.Data.Test
{
    using Factories;
    using Util;

    public class CreateAndExerciseCommandTest
    {
        private readonly CreateAndExerciseCommand _command1 = new CreateAndExerciseCommand(IdentifierFactory.Id1, RecordFactory.Record1, "doStuff", DamlOptional.Of(Optional.Of((Value)new Text("doStuffArgument"))));
        private readonly CreateAndExerciseCommand _command2 = new CreateAndExerciseCommand(IdentifierFactory.Id2, RecordFactory.Record2, "doMoreStuff", DamlOptional.Of(Optional.Of((Value)new Text("doMoreStuffArgument"))));
        private readonly CreateAndExerciseCommand _command3 = new CreateAndExerciseCommand(IdentifierFactory.Id1, RecordFactory.Record1, "doStuff", DamlOptional.Of(Optional.Of((Value)new Text("doStuffArgument"))));

#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {
            Assert.True(_command1.Equals(_command1));
            Assert.True(_command1 == _command1);

            Assert.True(_command1.Equals(_command3));
            Assert.True(_command1 == _command3);

            Assert.False(_command1.Equals(_command2));
            Assert.True(_command1 != _command2);
        }
#pragma warning restore CS1718

        [Fact]
        public void HashCodeHasValueSemantics()
        {
            Assert.True(_command1.GetHashCode() == _command3.GetHashCode());
            Assert.True(_command1.GetHashCode() != _command2.GetHashCode());
        }

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(_command1);
        }

        [Fact]
        public void CanRetrieveCreateAndExerciseCommandFromBase()
        {
            Command baseCommand = _command1;
            var maybeCreateAndExerciseCommand = baseCommand.AsCreateAndExerciseCommand();
            maybeCreateAndExerciseCommand.Should().BeOfType<Some<CreateAndExerciseCommand>>();
            Assert.True(maybeCreateAndExerciseCommand.Reduce(_command2) == _command1);
        }

        [Fact]
        public void CannotRetrieveCreateCommandFromBase()
        {
            Command baseCommand = _command1;
            var maybeCreateCommand = baseCommand.AsCreateCommand();
            maybeCreateCommand.Should().BeOfType<None<CreateCommand>>();
        }

        private void ConvertThroughProto(CreateAndExerciseCommand source)
        {
            Com.Daml.Ledger.Api.V1.CreateAndExerciseCommand protoValue = source.ToProto();
            CreateAndExerciseCommand target = CreateAndExerciseCommand.FromProto(protoValue);
            Assert.True(source == target);
        }
    }
}




