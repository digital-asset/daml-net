// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1;

    public interface ITransactionsClient
    {
        GetFlatTransactionResponse GetFlatTransactionByEventId(string ledgerId, string eventId, IEnumerable<string> requestingParties, TraceContext traceContext = null);

        Task<GetFlatTransactionResponse> GetFlatTransactionByEventIdAsync(string ledgerId, string eventId, IEnumerable<string> requestingParties, TraceContext traceContext = null);

        GetFlatTransactionResponse GetFlatTransactionById(string ledgerId, string transactionId, IEnumerable<string> requestingParties, TraceContext traceContext = null);

        Task<GetFlatTransactionResponse> GetFlatTransactionByIdAsync(string ledgerId, string transactionId, IEnumerable<string> requestingParties, TraceContext traceContext = null);

        LedgerOffset GetLedgerEnd(string ledgerId, TraceContext traceContext = null);

        Task<LedgerOffset> GetLedgerEndAsync(string ledgerId, TraceContext traceContext = null);

        GetTransactionResponse GetTransactionByEventId(string ledgerId, string eventId, IEnumerable<string> requestingParties, TraceContext traceContext = null);

        Task<GetTransactionResponse> GetTransactionByEventIdAsync(string ledgerId, string eventId, IEnumerable<string> requestingParties, TraceContext traceContext = null);

        GetTransactionResponse GetTransactionById(string ledgerId, string transactionId, IEnumerable<string> requestingParties, TraceContext traceContext = null);

        Task<GetTransactionResponse> GetTransactionByIdAsync(string ledgerId, string transactionId, IEnumerable<string> requestingParties, TraceContext traceContext = null);

        IAsyncEnumerator<GetTransactionsResponse> GetTransactions(
            string ledgerId,
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            TraceContext traceContext = null);

        IEnumerable<GetTransactionsResponse> GetTransactionsSync(
            string ledgerId,
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            TraceContext traceContext = null);

        IAsyncEnumerator<GetTransactionTreesResponse> GetTransactionTrees(
            string ledgerId,
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            TraceContext traceContext = null);

        IEnumerable<GetTransactionTreesResponse> GetTransactionTreesSync(
            string ledgerId,
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            TraceContext traceContext = null);
    }
}
