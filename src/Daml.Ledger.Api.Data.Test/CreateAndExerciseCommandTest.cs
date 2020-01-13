// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Com.Daml.Ledger.Api.Data.Test.Factories;
using Com.Daml.Ledger.Api.Util;
using NUnit.Framework;

namespace Com.Daml.Ledger.Api.Data.Test
{
    [TestFixture]
    public class CreateAndExerciseCommandTest
    {
        private readonly CreateAndExerciseCommand _command1 = new CreateAndExerciseCommand(IdentifierFactory.Id1, RecordFactory.Record1, "doStuff", DamlOptional.Of(Optional.Of((Value)new Text("doStuffArgument"))));
        private readonly CreateAndExerciseCommand _command2 = new CreateAndExerciseCommand(IdentifierFactory.Id2, RecordFactory.Record2, "doMoreStuff", DamlOptional.Of(Optional.Of((Value)new Text("doMoreStuffArgument"))));
        private readonly CreateAndExerciseCommand _command3 = new CreateAndExerciseCommand(IdentifierFactory.Id1, RecordFactory.Record1, "doStuff", DamlOptional.Of(Optional.Of((Value)new Text("doStuffArgument"))));

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
        public void CanRetrieveCreateAndExerciseCommandFromBase()
        {
            Command baseCommand = _command1;
            var maybeCreateAndExerciseCommand = baseCommand.AsCreateAndExerciseCommand();
            Assert.AreEqual(typeof(Some<CreateAndExerciseCommand>), maybeCreateAndExerciseCommand.GetType());
            Assert.IsTrue(maybeCreateAndExerciseCommand.Reduce(_command2) == _command1);
        }

        [Test]
        public void CannotRetrieveCreateCommandFromBase()
        {
            Command baseCommand = _command1;
            var maybeCreateCommand = baseCommand.AsCreateCommand();
            Assert.AreEqual(typeof(None<CreateCommand>), maybeCreateCommand.GetType());
        }

        private void ConvertThroughProto(CreateAndExerciseCommand source)
        {
            DigitalAsset.Ledger.Api.V1.CreateAndExerciseCommand protoValue = source.ToProto();
            CreateAndExerciseCommand target = CreateAndExerciseCommand.FromProto(protoValue);
            Assert.IsTrue(source == target);
        }
    }
}




