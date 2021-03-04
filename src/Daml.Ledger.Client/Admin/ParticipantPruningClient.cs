// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Threading.Tasks;
using Grpc.Core;

namespace Daml.Ledger.Client.Admin
{
    using Com.Daml.Ledger.Api.V1.Admin;
    using Daml.Ledger.Client.Auth.Client;

    public class ParticipantPruningClient : IParticipantPruningClient
    {
        private readonly ClientStub<ParticipantPruningService.ParticipantPruningServiceClient> _participantPruningClient;

        public ParticipantPruningClient(Channel channel, string accessToken)
        {
            _participantPruningClient = new ClientStub<ParticipantPruningService.ParticipantPruningServiceClient>(new ParticipantPruningService.ParticipantPruningServiceClient(channel), accessToken);
        }

        public PruneResponse Prune(string pruneUpTo, string submissionId = null, string accessToken = null)
        {
            var request = new PruneRequest() { PruneUpTo= pruneUpTo };
            if (submissionId != null)
                request.SubmissionId = submissionId;
            return _participantPruningClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.Prune(r, co));
        }

        public async Task<PruneResponse> PruneAsync(string pruneUpTo, string submissionId = null, string accessToken = null)
        {
            var request = new PruneRequest() { PruneUpTo = pruneUpTo };
            if (submissionId != null)
                request.SubmissionId = submissionId;
            return await _participantPruningClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.PruneAsync(r, co));
        }
    }
}
