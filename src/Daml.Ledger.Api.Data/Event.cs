// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Daml.Ledger.Api.Data 
{
    using System.Collections.Generic;
    
    public interface IEvent : IComparable<IEvent>, IEquatable<IEvent>
    {
        IReadOnlyList<string> WitnessParties { get; }
        string EventId { get; }
        Identifier TemplateId { get; }
        string ContractId { get; }

        // See EventHelper.cs for default ToProtoEvent method and static FromProtoEvent method.
    }
} 
