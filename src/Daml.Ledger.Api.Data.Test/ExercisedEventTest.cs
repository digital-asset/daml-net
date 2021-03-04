// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Xunit;

namespace Daml.Ledger.Api.Data.Test
{
    using Factories;

    public class ExercisedEventTest
    {
        private static readonly string[] _eventIds1 = { "event1", "event2", "event3" };
        private static readonly string[] _eventIds2 = { "event4", "event5", "event6" };

        private static readonly ExercisedEvent _event1 = new ExercisedEvent(PartiesFactory.Witnesses1, "event1", IdentifierFactory.Id1, "contract1", "doStuff", new Text("arg1"), PartiesFactory.ActingParties1, true, _eventIds1, Bool.True);
        private static readonly ExercisedEvent _event2 = new ExercisedEvent(PartiesFactory.Witnesses2, "event2", IdentifierFactory.Id2, "contract2", "doMoreStuff", new Text("arg2"), PartiesFactory.ActingParties2, false, _eventIds2, Bool.False);
        private static readonly ExercisedEvent _event3 = new ExercisedEvent(PartiesFactory.Witnesses1, "event1", IdentifierFactory.Id1, "contract1", "doStuff", new Text("arg1"), PartiesFactory.ActingParties1, true, _eventIds1, Bool.True);

#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {
            Assert.True(_event1.Equals(_event1));
            Assert.True(_event1 == _event1);

            Assert.True(_event1.Equals(_event3));
            Assert.True(_event1 == _event3);

            Assert.False(_event1.Equals(_event2));
            Assert.True(_event1 != _event2);
        }
#pragma warning restore CS1718

        [Fact]
        public void HashCodeHasValueSemantics()
        {

            Assert.True(_event1.GetHashCode() == _event3.GetHashCode());
            Assert.True(_event1.GetHashCode() != _event2.GetHashCode());
        }

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(_event1);
        }

        private void ConvertThroughProto(ExercisedEvent source)
        {
            Com.Daml.Ledger.Api.V1.ExercisedEvent protoValue = source.ToProto();
            var target = ExercisedEvent.FromProto(protoValue);
            Assert.True(source == target);
        }
    }
}


