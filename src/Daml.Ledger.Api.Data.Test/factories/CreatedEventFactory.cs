// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;
using Com.Daml.Ledger.Api.Util;

namespace Com.Daml.Ledger.Api.Data.Test.Factories
{
    public static class CreatedEventFactory
    {
        public static CreatedEvent Event1 { get; } = new CreatedEvent(PartiesFactory.Witnesses1, "event1", IdentifierFactory.Id1, "contract1", RecordFactory.Record1, Optional.Of("agreement1"), Optional.Of((Value)new Text("key1")), PartiesFactory.Signatories1, PartiesFactory.Observers1);
        public static CreatedEvent Event2 { get; } = new CreatedEvent(PartiesFactory.Witnesses2, "event2", IdentifierFactory.Id2, "contract2", RecordFactory.Record2, Optional.Of("agreement2"), Optional.Of((Value)new Text("key2")), PartiesFactory.Signatories2, PartiesFactory.Observers2);
        public static CreatedEvent Event3 { get; } = new CreatedEvent(PartiesFactory.Witnesses1, "event1", IdentifierFactory.Id1, "contract1", RecordFactory.Record1, Optional.Of("agreement1"), Optional.Of((Value)new Text("key1")), PartiesFactory.Signatories1, PartiesFactory.Observers1);
        public static CreatedEvent Event4 { get; } = new CreatedEvent(PartiesFactory.Witnesses3, "event4", IdentifierFactory.Id4, "contract4", RecordFactory.Record4, Optional.Of("agreement4"), Optional.Of((Value)new Text("key4")), PartiesFactory.Signatories3, PartiesFactory.Observers3);
        public static CreatedEvent Event5 { get; } = new CreatedEvent(PartiesFactory.Witnesses4, "event5", IdentifierFactory.Id5, "contract5", RecordFactory.Record5, Optional.Of("agreement5"), Optional.Of((Value)new Text("key5")), PartiesFactory.Signatories4, PartiesFactory.Observers4);
        public static CreatedEvent Event6 { get; } = new CreatedEvent(PartiesFactory.Witnesses3, "event6", IdentifierFactory.Id6, "contract6", RecordFactory.Record6, Optional.Of("agreement6"), Optional.Of((Value)new Text("key6")), PartiesFactory.Signatories3, PartiesFactory.Observers3);

        public static Dictionary<string, TreeEvent> EventsById1 { get; } = (new[] { CreatedEventFactory.Event1, CreatedEventFactory.Event2 }).ToDictionary(e => e.EventId, e => (TreeEvent)e);
        public static Dictionary<string, TreeEvent> EventsById2 { get; } = (new[] { CreatedEventFactory.Event4, CreatedEventFactory.Event5, CreatedEventFactory.Event6 }).ToDictionary(e => e.EventId, e => (TreeEvent)e);
    }
}
