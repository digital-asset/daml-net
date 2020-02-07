// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    using Daml.Ledger.Api.Data.Test.Factories;

    [TestFixture]
    public class CreatedEventTest
    {
#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            Assert.IsTrue(CreatedEventFactory.Event1.Equals(CreatedEventFactory.Event1));
            Assert.IsTrue(CreatedEventFactory.Event1 == CreatedEventFactory.Event1);

            Assert.IsTrue(CreatedEventFactory.Event1.Equals(CreatedEventFactory.Event3));
            Assert.IsTrue(CreatedEventFactory.Event1 == CreatedEventFactory.Event3);

            Assert.IsFalse(CreatedEventFactory.Event1.Equals(CreatedEventFactory.Event2));
            Assert.IsTrue(CreatedEventFactory.Event1 != CreatedEventFactory.Event2);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            Assert.IsTrue(CreatedEventFactory.Event1.GetHashCode() == CreatedEventFactory.Event3.GetHashCode());
            Assert.IsTrue(CreatedEventFactory.Event1.GetHashCode() != CreatedEventFactory.Event2.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(CreatedEventFactory.Event1);
        }

        [Test]
        public void CanConvertBetweenProtoUsingEventHelper()
        {
            ConvertThroughProtoUsingEventHelper(CreatedEventFactory.Event1);
        }

        private void ConvertThroughProto(CreatedEvent source)
        {
            Com.DigitalAsset.Ledger.Api.V1.CreatedEvent protoValue = source.ToProto();
            var target = CreatedEvent.FromProto(protoValue);
            Assert.IsTrue(source == target);
        }

        private void ConvertThroughProtoUsingEventHelper(CreatedEvent source)
        {
            Com.DigitalAsset.Ledger.Api.V1.Event protoValue = EventHelper.ToProtoEvent(source);
            var target = EventHelper.FromProtoEvent(protoValue);
            Assert.IsTrue(target is CreatedEvent);
            Assert.IsTrue(source == (CreatedEvent)target);
        }
    }
}

