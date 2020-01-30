// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1.Admin;
    using Daml.Ledger.Client.Auth.Client;
    using Grpc.Core;

    public class PartyManagementClient : IPartyManagementClient
    {
        private readonly ClientStub<PartyManagementService.PartyManagementServiceClient> _partyManagementClient;

        public PartyManagementClient(Channel channel)
        {
            _partyManagementClient = new ClientStub<PartyManagementService.PartyManagementServiceClient>(new PartyManagementService.PartyManagementServiceClient(channel));
        }

        public PartyDetails AllocateParty(string displayName, string partyIdHint)
        {
            var request = new AllocatePartyRequest { DisplayName = displayName, PartyIdHint = partyIdHint };
            var response = _partyManagementClient.Dispatch(request, (c, r, co) => c.AllocateParty(r, co));
            return response.PartyDetails;
        }

        public async Task<PartyDetails> AllocatePartyAsync(string displayName, string partyIdHint)
        {
            var request = new AllocatePartyRequest { DisplayName = displayName, PartyIdHint = partyIdHint };
            var response = await _partyManagementClient.Dispatch(request, (c, r, co) => c.AllocatePartyAsync(r, co));
            return response.PartyDetails;
        }

        public string GetParticipantId()
        {
            var response = _partyManagementClient.Dispatch(new GetParticipantIdRequest(), (c, r, co) => c.GetParticipantId(r, co));
            return response.ParticipantId;
        }

        public async Task<string> GetParticipantIdAsync()
        {
            var response = await _partyManagementClient.Dispatch(new GetParticipantIdRequest(), (c, r, co) => c.GetParticipantIdAsync(r, co));
            return response.ParticipantId;
        }

        public IEnumerable<PartyDetails> ListKnownParties()
        {
            var response = _partyManagementClient.Dispatch(new ListKnownPartiesRequest(), (c, r, co) => c.ListKnownParties(r, co));
            return response.PartyDetails;
        }

        public async Task<IEnumerable<PartyDetails>> ListKnownPartiesAsync()
        {
            var response = await _partyManagementClient.Dispatch(new ListKnownPartiesRequest(), (c, r, co) => c.ListKnownPartiesAsync(r, co));
            return response.PartyDetails;
        }
    }
}
