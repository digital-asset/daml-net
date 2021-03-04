// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daml.Ledger.Client.Admin
{
    using Com.Daml.Ledger.Api.V1.Admin;

    public interface IParticipantPruningClient
    {
        PruneResponse Prune(string pruneUpTo, string submissionId = null, string accessToken = null);
  
        Task<PruneResponse> PruneAsync(string pruneUpTo, string submissionId = null, string accessToken = null);
    }
}
