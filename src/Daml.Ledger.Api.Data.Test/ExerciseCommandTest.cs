// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Api.Data.Test
{
    using Factories;
    using Util;

    public class ExerciseCommandTest
    {
        private readonly ExerciseCommand _command1 = new ExerciseCommand(IdentifierFactory.Id1, "contract1", "doStuff", new Text("arg1"));
        private readonly ExerciseCommand _command2 = new ExerciseCommand(IdentifierFactory.Id2, "contract2", "doMoreStuff", new Text("arg2"));
        private readonly ExerciseCommand _command3 = new ExerciseCommand(IdentifierFactory.Id1, "contract1", "doStuff", new Text("arg1"));


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
        public void CanRetrieveExerciseCommandFromBase()
        {
            Command baseCommand = _command1;
            var maybeExerciseCommand = baseCommand.AsExerciseCommand();
            maybeExerciseCommand.Should().BeOfType<Some<ExerciseCommand>>();
            Assert.True(maybeExerciseCommand.Reduce(_command2) == _command1);
        }

        [Fact]
        public void CannotRetrieveCreateCommandFromBase()
        {
            Command baseCommand = _command1;
            var maybeCreateCommand = baseCommand.AsCreateCommand();
            maybeCreateCommand.Should().BeOfType<None<CreateCommand>>();
        }

        private void ConvertThroughProto(ExerciseCommand source)
        {
            Com.Daml.Ledger.Api.V1.ExerciseCommand protoValue = source.ToProto();
            var target = ExerciseCommand.FromProto(protoValue);
            Assert.True(source == target);
        }
    }
}


