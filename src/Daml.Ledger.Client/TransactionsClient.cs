// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Utils;

namespace Daml.Ledger.Client
{
    using Com.Daml.Ledger.Api.V1;
    using Daml.Ledger.Client.Auth.Client;

    public class TransactionsClient : ITransactionsClient
    {
        private readonly ClientStub<TransactionService.TransactionServiceClient> _transactionsClient;

        public TransactionsClient(string ledgerId, Channel channel, string accessToken)
        {
            LedgerId = ledgerId;
            _transactionsClient = new ClientStub<TransactionService.TransactionServiceClient>(new TransactionService.TransactionServiceClient(channel), accessToken);
        }

        public string LedgerId { get; }

        public GetFlatTransactionResponse GetFlatTransactionByEventId(string eventId, IEnumerable<string> requestingParties, string accessToken = null, TraceContext traceContext = null)
        {
            var request = new GetTransactionByEventIdRequest { LedgerId = LedgerId, EventId = eventId, TraceContext = traceContext };
            request.RequestingParties.AddRange(requestingParties);
            return _transactionsClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.GetFlatTransactionByEventId(r, co));
        }

        public async Task<GetFlatTransactionResponse> GetFlatTransactionByEventIdAsync(string eventId, IEnumerable<string> requestingParties, string accessToken = null, TraceContext traceContext = null)
        {
            var request = new GetTransactionByEventIdRequest { LedgerId = LedgerId, EventId = eventId, TraceContext = traceContext };
            request.RequestingParties.AddRange(requestingParties);
            return await _transactionsClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.GetFlatTransactionByEventIdAsync(r, co));
        }

        public GetFlatTransactionResponse GetFlatTransactionById(string transactionId, IEnumerable<string> requestingParties, string accessToken = null, TraceContext traceContext = null)
        {
            var request = new GetTransactionByIdRequest { LedgerId = LedgerId, TransactionId = transactionId, TraceContext = traceContext };
            request.RequestingParties.AddRange(requestingParties);
            return _transactionsClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.GetFlatTransactionById(r, co));
        }

        public async Task<GetFlatTransactionResponse> GetFlatTransactionByIdAsync(string transactionId, IEnumerable<string> requestingParties, string accessToken = null, TraceContext traceContext = null)
        {
            var request = new GetTransactionByIdRequest { LedgerId = LedgerId, TransactionId = transactionId, TraceContext = traceContext };
            request.RequestingParties.AddRange(requestingParties);
            return await _transactionsClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.GetFlatTransactionByIdAsync(r, co));
        }

        public LedgerOffset GetLedgerEnd(string accessToken = null, TraceContext traceContext = null)
        {
            var request = new GetLedgerEndRequest { LedgerId = LedgerId, TraceContext = traceContext };
            var response = _transactionsClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.GetLedgerEnd(r, co));
            return response.Offset;
        }

        public async Task<LedgerOffset> GetLedgerEndAsync(string accessToken = null, TraceContext traceContext = null)
        {
            var request = new GetLedgerEndRequest { LedgerId = LedgerId, TraceContext = traceContext };
            var response = await _transactionsClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.GetLedgerEndAsync(r, co));
            return response.Offset;
        }

        public GetTransactionResponse GetTransactionByEventId(string eventId, IEnumerable<string> requestingParties, string accessToken = null, TraceContext traceContext = null)
        {
            var request = new GetTransactionByEventIdRequest { LedgerId = LedgerId, EventId = eventId, TraceContext = traceContext };
            request.RequestingParties.AddRange(requestingParties);
            return _transactionsClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.GetTransactionByEventId(r, co));
        }

        public async Task<GetTransactionResponse> GetTransactionByEventIdAsync(string eventId, IEnumerable<string> requestingParties, string accessToken = null, TraceContext traceContext = null)
        {
            var request = new GetTransactionByEventIdRequest { LedgerId = LedgerId, EventId = eventId, TraceContext = traceContext };
            request.RequestingParties.AddRange(requestingParties);
            return await _transactionsClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.GetTransactionByEventIdAsync(r, co));
        }

        public GetTransactionResponse GetTransactionById(string transactionId, IEnumerable<string> requestingParties, string accessToken = null, TraceContext traceContext = null)
        {
            var request = new GetTransactionByIdRequest { LedgerId = LedgerId, TransactionId = transactionId, TraceContext = traceContext };
            request.RequestingParties.AddRange(requestingParties);
            return _transactionsClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.GetTransactionById(r, co));
        }

        public async Task<GetTransactionResponse> GetTransactionByIdAsync(string transactionId, IEnumerable<string> requestingParties, string accessToken = null, TraceContext traceContext = null)
        {
            var request = new GetTransactionByIdRequest { LedgerId = LedgerId, TransactionId = transactionId, TraceContext = traceContext };
            request.RequestingParties.AddRange(requestingParties);
            return await _transactionsClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.GetTransactionByIdAsync(r, co));
        }

        public IAsyncEnumerator<GetTransactionsResponse> GetTransactions(
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            string accessToken = null,
            TraceContext traceContext = null)
        {
            return GetTransactionsImpl(transactionFilter, beginOffset, endOffset, verbose, accessToken, traceContext).ReadAllAsync().GetAsyncEnumerator();
        }

        public IEnumerable<GetTransactionsResponse> GetTransactionsSync(
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            string accessToken = null,
            TraceContext traceContext = null)
        {
            return GetTransactionsImpl(transactionFilter, beginOffset, endOffset, verbose, accessToken, traceContext).ToListAsync().Result;
        }

        public IAsyncEnumerator<GetTransactionTreesResponse> GetTransactionTrees(
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            string accessToken = null,
            TraceContext traceContext = null)
        {
            return GetTransactionTreesImpl(transactionFilter, beginOffset, endOffset, verbose, accessToken, traceContext).ReadAllAsync().GetAsyncEnumerator();
        }

        public IEnumerable<GetTransactionTreesResponse> GetTransactionTreesSync(
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            string accessToken = null,
            TraceContext traceContext = null)
        {
            return GetTransactionTreesImpl(transactionFilter, beginOffset, endOffset, verbose, accessToken, traceContext).ToListAsync().Result;
        }

        private IAsyncStreamReader<GetTransactionsResponse> GetTransactionsImpl(TransactionFilter transactionFilter, LedgerOffset beginOffset, LedgerOffset endOffset, bool verbose, string accessToken, TraceContext traceContext)
            {
            var request = new GetTransactionsRequest
                {
                LedgerId = LedgerId,
                Filter = transactionFilter,
                Begin = beginOffset,
                End = endOffset,
                Verbose = verbose,
                TraceContext = traceContext
            };

            var response = _transactionsClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.GetTransactions(r, co));
            return response.ResponseStream;
            }

        private IAsyncStreamReader<GetTransactionTreesResponse> GetTransactionTreesImpl(TransactionFilter transactionFilter, LedgerOffset beginOffset, LedgerOffset endOffset, bool verbose, string accessToken, TraceContext traceContext)
        {
            var request = new GetTransactionsRequest
            {
                LedgerId = LedgerId,
                Filter = transactionFilter,
                Begin = beginOffset,
                End = endOffset,
                Verbose = verbose,
                TraceContext = traceContext
            };
            
            var response = _transactionsClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.GetTransactionTrees(r, co));
            return response.ResponseStream;
        }
    }
}
