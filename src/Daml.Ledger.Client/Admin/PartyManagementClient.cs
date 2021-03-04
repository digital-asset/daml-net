// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;

namespace Daml.Ledger.Client.Admin
{
    using Com.DigitalAsset.Ledger.Api.V1.Admin;
    using Daml.Ledger.Client.Auth.Client;

    public class PartyManagementClient : IPartyManagementClient
    {
        private readonly ClientStub<PartyManagementService.PartyManagementServiceClient> _partyManagementClient;

        public PartyManagementClient(Channel channel, string accessToken)
        {
            _partyManagementClient = new ClientStub<PartyManagementService.PartyManagementServiceClient>(new PartyManagementService.PartyManagementServiceClient(channel), accessToken);
        }

        public PartyDetails AllocateParty(string displayName, string partyIdHint, string accessToken = null)
        {
            var request = new AllocatePartyRequest { DisplayName = displayName, PartyIdHint = partyIdHint };
            var response = _partyManagementClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.AllocateParty(r, co));
            return response.PartyDetails;
        }

        public async Task<PartyDetails> AllocatePartyAsync(string displayName, string partyIdHint, string accessToken = null)
        {
            var request = new AllocatePartyRequest { DisplayName = displayName, PartyIdHint = partyIdHint };
            var response = await _partyManagementClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.AllocatePartyAsync(r, co));
            return response.PartyDetails;
        }

        public string GetParticipantId(string accessToken = null)
        {
            var response = _partyManagementClient.WithAccess(accessToken).Dispatch(new GetParticipantIdRequest(), (c, r, co) => c.GetParticipantId(r, co));
            return response.ParticipantId;
        }

        public async Task<string> GetParticipantIdAsync(string accessToken = null)
        {
            var response = await _partyManagementClient.WithAccess(accessToken).Dispatch(new GetParticipantIdRequest(), (c, r, co) => c.GetParticipantIdAsync(r, co));
            return response.ParticipantId;
        }

        public IEnumerable<PartyDetails> ListKnownParties(string accessToken = null)
        {
            var response = _partyManagementClient.WithAccess(accessToken).Dispatch(new ListKnownPartiesRequest(), (c, r, co) => c.ListKnownParties(r, co));
            return response.PartyDetails;
        }

        public async Task<IEnumerable<PartyDetails>> ListKnownPartiesAsync(string accessToken = null)
        {
            var response = await _partyManagementClient.WithAccess(accessToken).Dispatch(new ListKnownPartiesRequest(), (c, r, co) => c.ListKnownPartiesAsync(r, co));
            return response.PartyDetails;
        }
    }
}
