// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Api.Data.Test
{
    using Factories;
    using Util;

    public class ExerciseByKeyCommandTest
    {
        private readonly ExerciseByKeyCommand _command1 = new ExerciseByKeyCommand(IdentifierFactory.Id1, new Text("key1"), "doStuff", new Text("arg1"));
        private readonly ExerciseByKeyCommand _command2 = new ExerciseByKeyCommand(IdentifierFactory.Id2, new Text("key2"), "doMoreStuff", new Text("arg2"));
        private readonly ExerciseByKeyCommand _command3 = new ExerciseByKeyCommand(IdentifierFactory.Id1, new Text("key1"), "doStuff", new Text("arg1"));

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
        public void CanRetrieveExerciseByKeyCommandFromBase()
        {
            Command baseCommand = _command1;
            var maybeExerciseByKeyCommand = baseCommand.AsExerciseByKeyCommand();
            maybeExerciseByKeyCommand.Should().BeOfType<Some<ExerciseByKeyCommand>>();
            Assert.True(maybeExerciseByKeyCommand.Reduce(_command2) == _command1);
        }

        [Fact]
        public void CannotRetrieveCreateCommandFromBase()
        {
            Command baseCommand = _command1;
            var maybeCreateCommand = baseCommand.AsCreateCommand();
            maybeCreateCommand.Should().BeOfType<None<CreateCommand>>();
        }

        private void ConvertThroughProto(ExerciseByKeyCommand source)
        {
            Com.Daml.Ledger.Api.V1.ExerciseByKeyCommand protoValue = source.ToProto();
            var target = ExerciseByKeyCommand.FromProto(protoValue);
            Assert.True(source == target);
        }
    }
}


