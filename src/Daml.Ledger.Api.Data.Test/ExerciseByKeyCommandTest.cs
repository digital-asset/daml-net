// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    using Daml.Ledger.Api.Data.Test.Factories;
    using Util;

    [TestFixture]
    public class ExerciseByKeyCommandTest
    {
        private readonly ExerciseByKeyCommand _command1 = new ExerciseByKeyCommand(IdentifierFactory.Id1, new Text("key1"), "doStuff", new Text("arg1"));
        private readonly ExerciseByKeyCommand _command2 = new ExerciseByKeyCommand(IdentifierFactory.Id2, new Text("key2"), "doMoreStuff", new Text("arg2"));
        private readonly ExerciseByKeyCommand _command3 = new ExerciseByKeyCommand(IdentifierFactory.Id1, new Text("key1"), "doStuff", new Text("arg1"));

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
        public void CanRetrieveExerciseByKeyCommandFromBase()
        {
            Command baseCommand = _command1;
            var maybeExerciseByKeyCommand = baseCommand.AsExerciseByKeyCommand();
            Assert.AreEqual(typeof(Some<ExerciseByKeyCommand>), maybeExerciseByKeyCommand.GetType());
            Assert.IsTrue(maybeExerciseByKeyCommand.Reduce(_command2) == _command1);
        }

        [Test]
        public void CannotRetrieveCreateCommandFromBase()
        {
            Command baseCommand = _command1;
            var maybeCreateCommand = baseCommand.AsCreateCommand();
            Assert.AreEqual(typeof(None<CreateCommand>), maybeCreateCommand.GetType());
        }

        private void ConvertThroughProto(ExerciseByKeyCommand source)
        {
            Com.DigitalAsset.Ledger.Api.V1.ExerciseByKeyCommand protoValue = source.ToProto();
            var target = ExerciseByKeyCommand.FromProto(protoValue);
            Assert.IsTrue(source == target);
        }
    }
}


