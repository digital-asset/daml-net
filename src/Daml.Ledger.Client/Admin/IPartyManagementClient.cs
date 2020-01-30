// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1.Admin;

    public interface IPartyManagementClient
    {
        PartyDetails AllocateParty(string displayName, string partyIdHint);

        Task<PartyDetails> AllocatePartyAsync(string displayName, string partyIdHint);

        string GetParticipantId();

        Task<string> GetParticipantIdAsync();

        IEnumerable<PartyDetails> ListKnownParties();

        Task<IEnumerable<PartyDetails>> ListKnownPartiesAsync();
    }
}
