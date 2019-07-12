// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1;

    public interface ICommandCompletionClient
    {
        IAsyncEnumerator<CompletionStreamResponse> CompletionStream(string ledgerId, string applicationId, LedgerOffset offset, IEnumerable<string> parties);

        IEnumerable<CompletionStreamResponse> CompletionStreamSync(string ledgerId, string applicationId, LedgerOffset offset, IEnumerable<string> parties);

        LedgerOffset CompletionEnd(string ledgerId, TraceContext traceContext = null);

        Task<LedgerOffset> CompletionEndAsync(string ledgerId, TraceContext traceContext = null);
    }
}
