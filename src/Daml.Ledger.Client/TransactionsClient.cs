// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1;
    using Grpc.Core;

    public class TransactionsClient : ITransactionsClient
    {
        private readonly string _ledgerId;
        private readonly TransactionService.TransactionServiceClient _transactionsClient;

        public TransactionsClient(string ledgerId, Channel channel)
        {
            _ledgerId = ledgerId;
            _transactionsClient = new TransactionService.TransactionServiceClient(channel);
        }

        public GetFlatTransactionResponse GetFlatTransactionByEventId(string eventId, IEnumerable<string> requestingParties, TraceContext traceContext = null)
        {
            var request = new GetTransactionByEventIdRequest { LedgerId = _ledgerId, EventId = eventId, TraceContext = traceContext };
            request.RequestingParties.AddRange(requestingParties);
            return _transactionsClient.GetFlatTransactionByEventId(request);
        }

        public async Task<GetFlatTransactionResponse> GetFlatTransactionByEventIdAsync(string eventId, IEnumerable<string> requestingParties, TraceContext traceContext = null)
        {
            var request = new GetTransactionByEventIdRequest { LedgerId = _ledgerId, EventId = eventId, TraceContext = traceContext };
            request.RequestingParties.AddRange(requestingParties);
            return await _transactionsClient.GetFlatTransactionByEventIdAsync(request);
        }

        public GetFlatTransactionResponse GetFlatTransactionById(string transactionId, IEnumerable<string> requestingParties, TraceContext traceContext = null)
        {
            var request = new GetTransactionByIdRequest { LedgerId = _ledgerId, TransactionId = transactionId, TraceContext = traceContext };
            request.RequestingParties.AddRange(requestingParties);
            return _transactionsClient.GetFlatTransactionById(request);
        }

        public async Task<GetFlatTransactionResponse> GetFlatTransactionByIdAsync(string transactionId, IEnumerable<string> requestingParties, TraceContext traceContext = null)
        {
            var request = new GetTransactionByIdRequest { LedgerId = _ledgerId, TransactionId = transactionId, TraceContext = traceContext };
            request.RequestingParties.AddRange(requestingParties);
            return await _transactionsClient.GetFlatTransactionByIdAsync(request);
        }

        public LedgerOffset GetLedgerEnd(TraceContext traceContext = null)
        {
            var request = new GetLedgerEndRequest { LedgerId = _ledgerId, TraceContext = traceContext };
            var response = _transactionsClient.GetLedgerEnd(request);
            return response.Offset;
        }

        public async Task<LedgerOffset> GetLedgerEndAsync(TraceContext traceContext = null)
        {
            var request = new GetLedgerEndRequest { LedgerId = _ledgerId, TraceContext = traceContext };
            var response = await _transactionsClient.GetLedgerEndAsync(request);
            return response.Offset;
        }

        public GetTransactionResponse GetTransactionByEventId(string eventId, IEnumerable<string> requestingParties, TraceContext traceContext = null)
        {
            var request = new GetTransactionByEventIdRequest { LedgerId = _ledgerId, EventId = eventId, TraceContext = traceContext };
            request.RequestingParties.AddRange(requestingParties);
            return _transactionsClient.GetTransactionByEventId(request);
        }

        public async Task<GetTransactionResponse> GetTransactionByEventIdAsync(string eventId, IEnumerable<string> requestingParties, TraceContext traceContext = null)
        {
            var request = new GetTransactionByEventIdRequest { LedgerId = _ledgerId, EventId = eventId, TraceContext = traceContext };
            request.RequestingParties.AddRange(requestingParties);
            return await _transactionsClient.GetTransactionByEventIdAsync(request);
        }

        public GetTransactionResponse GetTransactionById(string transactionId, IEnumerable<string> requestingParties, TraceContext traceContext = null)
        {
            var request = new GetTransactionByIdRequest { LedgerId = _ledgerId, TransactionId = transactionId, TraceContext = traceContext };
            request.RequestingParties.AddRange(requestingParties);
            return _transactionsClient.GetTransactionById(request);
        }

        public async Task<GetTransactionResponse> GetTransactionByIdAsync(string transactionId, IEnumerable<string> requestingParties, TraceContext traceContext = null)
        {
            var request = new GetTransactionByIdRequest { LedgerId = _ledgerId, TransactionId = transactionId, TraceContext = traceContext };
            request.RequestingParties.AddRange(requestingParties);
            return await _transactionsClient.GetTransactionByIdAsync(request);
        }

        public IAsyncEnumerator<GetTransactionsResponse> GetTransactions(
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            TraceContext traceContext = null)
        {
            var request = new GetTransactionsRequest
                {
                    LedgerId = _ledgerId,
                    Filter = transactionFilter,
                    Begin = beginOffset,
                    End = endOffset,
                    Verbose = verbose,
                    TraceContext = traceContext
                };
            var response = _transactionsClient.GetTransactions(request);
            return response.ResponseStream;
        }

        public IEnumerable<GetTransactionsResponse> GetTransactionsSync(
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            TraceContext traceContext = null)
        {
            using (var stream = GetTransactions(transactionFilter, beginOffset, endOffset, verbose, traceContext))
            {
                while (stream.MoveNext().Result)
                {
                    yield return stream.Current;
                }
            }
        }

        public IAsyncEnumerator<GetTransactionTreesResponse> GetTransactionTrees(
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            TraceContext traceContext = null)
        {
            var request = new GetTransactionsRequest
                {
                    LedgerId = _ledgerId,
                    Filter = transactionFilter,
                    Begin = beginOffset,
                    End = endOffset,
                    Verbose = verbose,
                    TraceContext = traceContext
                };
            var response = _transactionsClient.GetTransactionTrees(request);
            return response.ResponseStream;
        }

        public IEnumerable<GetTransactionTreesResponse> GetTransactionTreesSync(
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            TraceContext traceContext = null)
        {
            using (var stream = GetTransactionTrees(transactionFilter, beginOffset, endOffset, verbose, traceContext))
            {
                while (stream.MoveNext().Result)
                {
                    yield return stream.Current;
                }
            }
        }
    }
}
