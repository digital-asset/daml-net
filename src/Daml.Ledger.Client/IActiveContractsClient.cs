// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;

namespace Daml.Ledger.Client
{
    using Com.Daml.Ledger.Api.V1;

    public interface IActiveContractsClient
    {
        string LedgerId { get; }

        IAsyncEnumerator<GetActiveContractsResponse> GetActiveContracts(
            TransactionFilter transactionFilter,
            bool verbose = true,
            string accessToken = null,
            TraceContext traceContext = null);

        IEnumerable<GetActiveContractsResponse> GetActiveContractsSync(
                TransactionFilter transactionFilter,
                bool verbose = true,
                string accessToken = null,
                TraceContext traceContext = null);
    }
}
