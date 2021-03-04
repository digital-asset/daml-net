// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;
using System.Linq;

namespace Daml.Ledger.Api.Data.Test
{
    using Daml.Ledger.Api.Data.Test.Factories;

    [TestFixture]
    public class ArchivedEventTest
    {
        private static readonly ArchivedEvent _event1 = new ArchivedEvent(PartiesFactory.Witnesses1, "event1", IdentifierFactory.Id1, "contract1");
        private static readonly ArchivedEvent _event2 = new ArchivedEvent(PartiesFactory.Witnesses2, "event2", IdentifierFactory.Id2, "contract2");
        private static readonly ArchivedEvent _event3 = new ArchivedEvent(PartiesFactory.Witnesses1, "event1", IdentifierFactory.Id1, "contract1");

#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            Assert.IsTrue(_event1.Equals(_event1));
            Assert.IsTrue(_event1 == _event1);

            Assert.IsTrue(_event1.Equals(_event3));
            Assert.IsTrue(_event1 == _event3);

            Assert.IsFalse(_event1.Equals(_event2));
            Assert.IsTrue(_event1 != _event2);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            Assert.IsTrue(_event1.GetHashCode() == _event3.GetHashCode());
            Assert.IsTrue(_event1.GetHashCode() != _event2.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(_event1);
            ConvertThroughProto(new ArchivedEvent(new string[] { }, "_event1", IdentifierFactory.Id1, "contract1"));
        }

        [Test]
        public void CanConvertBetweenProtoUsingEventHelper()
        {
            ConvertThroughProtoUsingEventHelper(_event1);
            ConvertThroughProtoUsingEventHelper(new ArchivedEvent(new string[] { }, "_event1", IdentifierFactory.Id1, "contract1"));
        }

        [Test]
        public void EqualityIgnoresWitnessPartyOrder()
        {
            var event2 = new ArchivedEvent(PartiesFactory.Witnesses1.Reverse(), "event1", IdentifierFactory.Id1, "contract1");

            Assert.IsTrue(_event1.Equals(event2));
            Assert.IsTrue(_event1 == event2);
        }

        private void ConvertThroughProto(ArchivedEvent source)
        {
            Com.Daml.Ledger.Api.V1.ArchivedEvent protoValue = source.ToProto();
            ArchivedEvent target = ArchivedEvent.FromProto(protoValue);
            Assert.IsTrue(source == target);
        }

        private void ConvertThroughProtoUsingEventHelper(ArchivedEvent source)
        {
            Com.Daml.Ledger.Api.V1.Event protoValue = EventHelper.ToProtoEvent(source);
            var target = EventHelper.FromProtoEvent(protoValue);
            Assert.IsTrue(target is ArchivedEvent);
            Assert.IsTrue(source == (ArchivedEvent) target);
        }
    }
}