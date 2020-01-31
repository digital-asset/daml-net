// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daml.Ledger.Client.Admin
{
    using Com.DigitalAsset.Ledger.Api.V1.Admin;

    public interface IPartyManagementClient
    {
        PartyDetails AllocateParty(string displayName, string partyIdHint, string accessToken = null);

        Task<PartyDetails> AllocatePartyAsync(string displayName, string partyIdHint, string accessToken = null);

        string GetParticipantId(string accessToken = null);

        Task<string> GetParticipantIdAsync(string accessToken = null);

        IEnumerable<PartyDetails> ListKnownParties(string accessToken = null);

        Task<IEnumerable<PartyDetails>> ListKnownPartiesAsync(string accessToken = null);
    }
}
