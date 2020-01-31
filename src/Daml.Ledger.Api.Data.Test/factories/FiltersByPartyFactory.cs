// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;

namespace Daml.Ledger.Api.Data.Test.Factories
{
    public static class FiltersByPartyFactory
    {
        public static Dictionary<string, Filter> PartyToFilters1 { get; } = new Dictionary<string, Filter>()
        {
            { "party1", new InclusiveFilter(new [] { IdentifierFactory.Id1 } ) },
            { "party2", new InclusiveFilter(new [] { IdentifierFactory.Id2 } ) },
            { "party3", new InclusiveFilter(new [] { IdentifierFactory.Id3 } ) }
        };

        public static Dictionary<string, Filter> PartyToFilters2 { get; } = new Dictionary<string, Filter>()
        {
            { "party4", new InclusiveFilter(new [] { IdentifierFactory.Id4 } ) },
            { "party5", new InclusiveFilter(new [] { IdentifierFactory.Id5 } ) },
            { "party6", new InclusiveFilter(new [] { IdentifierFactory.Id6 } ) }
        };

        public static FiltersByParty Filters1 { get; } = new FiltersByParty(PartyToFilters1);

        public static FiltersByParty Filters2 { get; } = new FiltersByParty(PartyToFilters2);

        public static FiltersByParty Filters3 { get; } = new FiltersByParty(PartyToFilters1);
    }
}
