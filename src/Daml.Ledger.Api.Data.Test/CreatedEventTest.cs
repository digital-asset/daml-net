// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Api.Data.Test
{
    using Factories;

    public class CreatedEventTest
    {
#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {
            Assert.True(CreatedEventFactory.Event1.Equals(CreatedEventFactory.Event1));
            Assert.True(CreatedEventFactory.Event1 == CreatedEventFactory.Event1);

            Assert.True(CreatedEventFactory.Event1.Equals(CreatedEventFactory.Event3));
            Assert.True(CreatedEventFactory.Event1 == CreatedEventFactory.Event3);

            Assert.False(CreatedEventFactory.Event1.Equals(CreatedEventFactory.Event2));
            Assert.True(CreatedEventFactory.Event1 != CreatedEventFactory.Event2);
        }
#pragma warning restore CS1718

        [Fact]
        public void HashCodeHasValueSemantics()
        {
            Assert.True(CreatedEventFactory.Event1.GetHashCode() == CreatedEventFactory.Event3.GetHashCode());
            Assert.True(CreatedEventFactory.Event1.GetHashCode() != CreatedEventFactory.Event2.GetHashCode());
        }

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(CreatedEventFactory.Event1);
        }

        [Fact]
        public void CanConvertBetweenProtoUsingEventHelper()
        {
            ConvertThroughProtoUsingEventHelper(CreatedEventFactory.Event1);
        }

        private void ConvertThroughProto(CreatedEvent source)
        {
            Com.Daml.Ledger.Api.V1.CreatedEvent protoValue = source.ToProto();
            var target = CreatedEvent.FromProto(protoValue);
            Assert.True(source == target);
        }

        private void ConvertThroughProtoUsingEventHelper(CreatedEvent source)
        {
            Com.Daml.Ledger.Api.V1.Event protoValue = EventHelper.ToProtoEvent(source);
            var target = EventHelper.FromProtoEvent(protoValue);
            target.Should().BeOfType<CreatedEvent>();
            Assert.True(source == (CreatedEvent)target);
        }
    }
}

