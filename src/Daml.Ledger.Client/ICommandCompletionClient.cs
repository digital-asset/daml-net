// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daml.Ledger.Client
{
    using Com.DigitalAsset.Ledger.Api.V1;

    public interface ICommandCompletionClient
    {
        IAsyncEnumerator<CompletionStreamResponse> CompletionStream(string applicationId, LedgerOffset offset, IEnumerable<string> parties, string accessToken = null);

        IEnumerable<CompletionStreamResponse> CompletionStreamSync(string applicationId, LedgerOffset offset, IEnumerable<string> parties, string accessToken = null);

        LedgerOffset CompletionEnd(string accessToken = null, TraceContext traceContext = null);

        Task<LedgerOffset> CompletionEndAsync(string accessToken = null, TraceContext traceContext = null);
    }
}
