// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    using Daml.Ledger.Api.Data.Test.Factories;
    using Util;

    [TestFixture]
    public class ExerciseCommandTest
    {
        private readonly ExerciseCommand _command1 = new ExerciseCommand(IdentifierFactory.Id1, "contract1", "doStuff", new Text("arg1"));
        private readonly ExerciseCommand _command2 = new ExerciseCommand(IdentifierFactory.Id2, "contract2", "doMoreStuff", new Text("arg2"));
        private readonly ExerciseCommand _command3 = new ExerciseCommand(IdentifierFactory.Id1, "contract1", "doStuff", new Text("arg1"));


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
        public void CanRetrieveExerciseCommandFromBase()
        {
            Command baseCommand = _command1;
            var maybeExerciseCommand = baseCommand.AsExerciseCommand();
            Assert.AreEqual(typeof(Some<ExerciseCommand>), maybeExerciseCommand.GetType());
            Assert.IsTrue(maybeExerciseCommand.Reduce(_command2) == _command1);
        }

        [Test]
        public void CannotRetrieveCreateCommandFromBase()
        {
            Command baseCommand = _command1;
            var maybeCreateCommand = baseCommand.AsCreateCommand();
            Assert.AreEqual(typeof(None<CreateCommand>), maybeCreateCommand.GetType());
        }

        private void ConvertThroughProto(ExerciseCommand source)
        {
            Com.DigitalAsset.Ledger.Api.V1.ExerciseCommand protoValue = source.ToProto();
            var target = ExerciseCommand.FromProto(protoValue);
            Assert.IsTrue(source == target);
        }
    }
}


