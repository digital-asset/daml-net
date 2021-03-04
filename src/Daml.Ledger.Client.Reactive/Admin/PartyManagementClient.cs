// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Reactive.Linq;

namespace Daml.Ledger.Client.Reactive.Admin
{
    using Daml.Ledger.Client.Admin;
    using Daml.Ledger.Api.Data.Util;

    using PartyDetails = Com.Daml.Ledger.Api.V1.Admin.PartyDetails;
    using System.Collections.Generic;

    public class PartyManagementClient
    {
       private readonly IPartyManagementClient _partyManagementClient;

        public PartyManagementClient(IPartyManagementClient partyManagementClient)
        {
            _partyManagementClient = partyManagementClient;
        }

        public PartyDetails AllocateParty(string displayName, string partyIdHint, Optional<string> accessToken = null)
        {
            return _partyManagementClient.AllocateParty(displayName, partyIdHint, accessToken?.Reduce((string) null));
        }

        public string GetParticipantId(Optional<string> accessToken = null)
        {
            return _partyManagementClient.GetParticipantId(accessToken?.Reduce((string) null));
        }

        public IObservable<PartyDetails> GetParties(IList<string> parties, Optional<string> accessToken = null)
        {
            return _partyManagementClient.GetParties(parties, accessToken?.Reduce((string)null)).ToObservable();
        }

        public IObservable<PartyDetails> ListKnownParties(Optional<string> accessToken = null)
        {
            return _partyManagementClient.ListKnownParties(accessToken?.Reduce((string) null)).ToObservable();
        }
    }
}
