// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;

namespace Daml.Ledger.Api.Data 
{
    using Util;

    public abstract class TreeEvent : IComparable<TreeEvent>, IEquatable<TreeEvent>
    {
        public abstract IReadOnlyList<string> WitnessParties { get; }
        public abstract string EventId { get; }
        public abstract Identifier TemplateId { get; }
        public abstract string ContractId { get; }

        public Com.DigitalAsset.Ledger.Api.V1.TreeEvent ToProtoTreeEvent()
        {
            if (this is CreatedEvent)
                return new Com.DigitalAsset.Ledger.Api.V1.TreeEvent { Created = ((CreatedEvent) this).ToProto()};

            if (this is ExercisedEvent) 
                return new Com.DigitalAsset.Ledger.Api.V1.TreeEvent { Exercised = ((ExercisedEvent) this).ToProto() };

            throw new ApplicationException($"this should be CreatedEvent or ExercisedEvent, found {ToString()}");
        }

        public static TreeEvent FromProtoTreeEvent(Com.DigitalAsset.Ledger.Api.V1.TreeEvent treeEvent)
        {
            if (treeEvent.KindCase == Com.DigitalAsset.Ledger.Api.V1.TreeEvent.KindOneofCase.Created)
                return CreatedEvent.FromProto(treeEvent.Created);
            
            if (treeEvent.KindCase == Com.DigitalAsset.Ledger.Api.V1.TreeEvent.KindOneofCase.Exercised)
                return ExercisedEvent.FromProto(treeEvent.Exercised);
            
            throw new UnsupportedEventTypeException($"Event is neither created not exercised: {treeEvent}");
        }

        public static bool operator ==(TreeEvent lhs, TreeEvent rhs) => lhs.Compare(rhs);
        public static bool operator !=(TreeEvent lhs, TreeEvent rhs) => !(lhs == rhs);

        public int CompareTo(TreeEvent rhs) => GetHashCode().CompareTo(rhs.GetHashCode());

        public override bool Equals(object obj) => Equals((TreeEvent)obj);
        public abstract bool Equals(TreeEvent other);

        public abstract override int GetHashCode();
    }
} 
