// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Daml.Ledger.Api.Data
{
    public class EventHelper
    {
        public static Com.DigitalAsset.Ledger.Api.V1.Event ToProtoEvent(IEvent theEvent)
        {
            if (theEvent is ArchivedEvent)
                return new Com.DigitalAsset.Ledger.Api.V1.Event { Archived = ((ArchivedEvent) theEvent).ToProto() };

            if (theEvent is CreatedEvent)
                return new Com.DigitalAsset.Ledger.Api.V1.Event { Created = ((CreatedEvent) theEvent).ToProto() };

            throw new ApplicationException($"this should be ArchivedEvent or CreatedEvent or ExercisedEvent, found {theEvent}");
        }

        public static IEvent FromProtoEvent(Com.DigitalAsset.Ledger.Api.V1.Event ledgerEvent)
        {
            if (ledgerEvent.EventCase == Com.DigitalAsset.Ledger.Api.V1.Event.EventOneofCase.Created)
                return CreatedEvent.FromProto(ledgerEvent.Created);

            if (ledgerEvent.EventCase == Com.DigitalAsset.Ledger.Api.V1.Event.EventOneofCase.Archived)
                return ArchivedEvent.FromProto(ledgerEvent.Archived);

            throw new UnsupportedEventTypeException(ledgerEvent.ToString());
        }
    }
}
