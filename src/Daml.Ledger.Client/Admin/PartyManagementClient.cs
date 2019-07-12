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
        private readonly PartyManagementService.PartyManagementServiceClient partyManagementClient;

        public PartyManagementClient(Channel channel)
        {
            this.partyManagementClient = new PartyManagementService.PartyManagementServiceClient(channel);
        }

        public PartyDetails AllocateParty(string displayName, string partyIdHint)
        {
            var request = new AllocatePartyRequest { DisplayName = displayName, PartyIdHint = partyIdHint };
            var response = this.partyManagementClient.AllocateParty(request);
            return response.PartyDetails;
        }

        public async Task<PartyDetails> AllocatePartyAsync(string displayName, string partyIdHint)
        {
            var request = new AllocatePartyRequest { DisplayName = displayName, PartyIdHint = partyIdHint };
            var response = await this.partyManagementClient.AllocatePartyAsync(request);
            return response.PartyDetails;
        }

        public string GetParticipantId()
        {
            var request = new GetParticipantIdRequest();
            var response = this.partyManagementClient.GetParticipantId(request);
            return response.ParticipantId;
        }

        public async Task<string> GetParticipantIdAsync()
        {
            var request = new GetParticipantIdRequest();
            var response = await this.partyManagementClient.GetParticipantIdAsync(request);
            return response.ParticipantId;
        }

        public IEnumerable<PartyDetails> ListKnownParties()
        {
            var request = new ListKnownPartiesRequest();
            var response = this.partyManagementClient.ListKnownParties(request);
            return response.PartyDetails;
        }

        public async Task<IEnumerable<PartyDetails>> ListKnownPartiesAsync()
        {
            var request = new ListKnownPartiesRequest();
            var response = await this.partyManagementClient.ListKnownPartiesAsync (request);
            return response.PartyDetails;
        }
    }
}
