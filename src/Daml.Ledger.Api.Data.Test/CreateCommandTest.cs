// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    using Daml.Ledger.Api.Data.Test.Factories;
    using Util;

    [TestFixture]
    public class CreateCommandTest
    {
        private readonly CreateCommand _command1 = new CreateCommand(IdentifierFactory.Id1, RecordFactory.Record1);
        private readonly CreateCommand _command2 = new CreateCommand(IdentifierFactory.Id2, RecordFactory.Record2);
        private readonly CreateCommand _command3 = new CreateCommand(IdentifierFactory.Id1, RecordFactory.Record1);

#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            Assert.IsTrue(_command1.Equals(_command1));
            Assert.IsTrue(_command1 == _command1);

            Assert.IsTrue(_command1.Equals(_command3));
            Assert.IsTrue(_command1 == _command3);

            Assert.IsFalse(_command1.Equals(_command2));
            Assert.IsTrue(_command1 != _command2);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            Assert.IsTrue(_command1.GetHashCode() == _command3.GetHashCode());
            Assert.IsTrue(_command1.GetHashCode() != _command2.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(_command1);
        }

        [Test]
        public void CanRetrieveCreateCommandFromBase()
        {
            Command baseCommand = _command1;
            var maybeCreateCommand = baseCommand.AsCreateCommand();
            Assert.AreEqual(typeof(Some<CreateCommand>), maybeCreateCommand.GetType());
            Assert.IsTrue(maybeCreateCommand.Reduce(_command2) == _command1);
        }

        [Test]
        public void CannotRetrieveExerciseCommandFromBase()
        {
            Command baseCommand = _command1;
            var maybeExerciseCommand = baseCommand.AsExerciseCommand();
            Assert.AreEqual(typeof(None<ExerciseCommand>), maybeExerciseCommand.GetType());
        }

        private void ConvertThroughProto(CreateCommand source)
        {
            Com.Daml.Ledger.Api.V1.CreateCommand protoValue = source.ToProto();
            var target = CreateCommand.FromProto(protoValue);
            Assert.IsTrue(source == target);
        }
    }
}

