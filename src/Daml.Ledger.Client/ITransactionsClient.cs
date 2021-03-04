// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daml.Ledger.Client
{
    using Com.Daml.Ledger.Api.V1;

    public interface ITransactionsClient
    {
        string LedgerId { get; }

        GetFlatTransactionResponse GetFlatTransactionByEventId(string eventId, IEnumerable<string> requestingParties, string accessToken = null, TraceContext traceContext = null);

        Task<GetFlatTransactionResponse> GetFlatTransactionByEventIdAsync(string eventId, IEnumerable<string> requestingParties, string accessToken = null, TraceContext traceContext = null);

        GetFlatTransactionResponse GetFlatTransactionById(string transactionId, IEnumerable<string> requestingParties, string accessToken = null, TraceContext traceContext = null);

        Task<GetFlatTransactionResponse> GetFlatTransactionByIdAsync(string transactionId, IEnumerable<string> requestingParties, string accessToken = null, TraceContext traceContext = null);

        LedgerOffset GetLedgerEnd(string accessToken = null, TraceContext traceContext = null);

        Task<LedgerOffset> GetLedgerEndAsync(string accessToken = null, TraceContext traceContext = null);

        GetTransactionResponse GetTransactionByEventId(string eventId, IEnumerable<string> requestingParties, string accessToken = null, TraceContext traceContext = null);

        Task<GetTransactionResponse> GetTransactionByEventIdAsync(string eventId, IEnumerable<string> requestingParties, string accessToken = null, TraceContext traceContext = null);

        GetTransactionResponse GetTransactionById(string transactionId, IEnumerable<string> requestingParties, string accessToken = null, TraceContext traceContext = null);

        Task<GetTransactionResponse> GetTransactionByIdAsync(string transactionId, IEnumerable<string> requestingParties, string accessToken = null, TraceContext traceContext = null);

        IAsyncEnumerator<GetTransactionsResponse> GetTransactions(
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            string accessToken = null,
            TraceContext traceContext = null);

        IEnumerable<GetTransactionsResponse> GetTransactionsSync(
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            string accessToken = null,
            TraceContext traceContext = null);

        IAsyncEnumerator<GetTransactionTreesResponse> GetTransactionTrees(
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            string accessToken = null,
            TraceContext traceContext = null);

        IEnumerable<GetTransactionTreesResponse> GetTransactionTreesSync(
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            string accessToken = null,
            TraceContext traceContext = null);
    }
}
