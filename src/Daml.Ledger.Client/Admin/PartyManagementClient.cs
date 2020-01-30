// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1.Admin;
    using Grpc.Core;

    public class PartyManagementClient : IPartyManagementClient
    {
        private readonly PartyManagementService.PartyManagementServiceClient _partyManagementClient;

        public PartyManagementClient(Channel channel)
        {
            _partyManagementClient = new PartyManagementService.PartyManagementServiceClient(channel);
        }

        public PartyDetails AllocateParty(string displayName, string partyIdHint)
        {
            var request = new AllocatePartyRequest { DisplayName = displayName, PartyIdHint = partyIdHint };
            var response = _partyManagementClient.AllocateParty(request);
            return response.PartyDetails;
        }

        public async Task<PartyDetails> AllocatePartyAsync(string displayName, string partyIdHint)
        {
            var request = new AllocatePartyRequest { DisplayName = displayName, PartyIdHint = partyIdHint };
            var response = await _partyManagementClient.AllocatePartyAsync(request);
            return response.PartyDetails;
        }

        public string GetParticipantId()
        {
            var response = _partyManagementClient.GetParticipantId(new GetParticipantIdRequest());
            return response.ParticipantId;
        }

        public async Task<string> GetParticipantIdAsync()
        {
            var response = await _partyManagementClient.GetParticipantIdAsync(new GetParticipantIdRequest());
            return response.ParticipantId;
        }

        public IEnumerable<PartyDetails> ListKnownParties()
        {
            var response = _partyManagementClient.ListKnownParties(new ListKnownPartiesRequest());
            return response.PartyDetails;
        }

        public async Task<IEnumerable<PartyDetails>> ListKnownPartiesAsync()
        {
            var response = await _partyManagementClient.ListKnownPartiesAsync(new ListKnownPartiesRequest());
            return response.PartyDetails;
        }
    }
}
