// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Api.Data.Test
{
    using Factories;
    using Util;

    public class CreateCommandTest
    {
        private readonly CreateCommand _command1 = new CreateCommand(IdentifierFactory.Id1, RecordFactory.Record1);
        private readonly CreateCommand _command2 = new CreateCommand(IdentifierFactory.Id2, RecordFactory.Record2);
        private readonly CreateCommand _command3 = new CreateCommand(IdentifierFactory.Id1, RecordFactory.Record1);

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
        public void CanRetrieveCreateCommandFromBase()
        {
            Command baseCommand = _command1;
            var maybeCreateCommand = baseCommand.AsCreateCommand();
            maybeCreateCommand.Should().BeOfType<Some<CreateCommand>>();
            Assert.True(maybeCreateCommand.Reduce(_command2) == _command1);
        }

        [Fact]
        public void CannotRetrieveExerciseCommandFromBase()
        {
            Command baseCommand = _command1;
            var maybeExerciseCommand = baseCommand.AsExerciseCommand();
            maybeExerciseCommand.Should().BeOfType<None<ExerciseCommand>>();
        }

        private void ConvertThroughProto(CreateCommand source)
        {
            Com.Daml.Ledger.Api.V1.CreateCommand protoValue = source.ToProto();
            var target = CreateCommand.FromProto(protoValue);
            Assert.True(source == target);
        }
    }
}

