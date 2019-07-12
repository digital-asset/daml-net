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
        private readonly TransactionService.TransactionServiceClient transactionsClient;

        public TransactionsClient(Channel channel)
        {
            this.transactionsClient = new TransactionService.TransactionServiceClient(channel);
        }

        public GetFlatTransactionResponse GetFlatTransactionByEventId(string ledgerId, string eventId, IEnumerable<string> requestingParties, TraceContext traceContext = null)
        {
            var request = new GetTransactionByEventIdRequest { LedgerId = ledgerId, EventId = eventId, TraceContext = traceContext };
            request.RequestingParties.AddRange(requestingParties);
            var response = this.transactionsClient.GetFlatTransactionByEventId(request);
            return response;
        }

        public async Task<GetFlatTransactionResponse> GetFlatTransactionByEventIdAsync(string ledgerId, string eventId, IEnumerable<string> requestingParties, TraceContext traceContext = null)
        {
            var request = new GetTransactionByEventIdRequest { LedgerId = ledgerId, EventId = eventId, TraceContext = traceContext };
            request.RequestingParties.AddRange(requestingParties);
            var response = await this.transactionsClient.GetFlatTransactionByEventIdAsync(request);
            return response;
        }

        public GetFlatTransactionResponse GetFlatTransactionById(string ledgerId, string transactionId, IEnumerable<string> requestingParties, TraceContext traceContext = null)
        {
            var request = new GetTransactionByIdRequest { LedgerId = ledgerId, TransactionId = transactionId, TraceContext = traceContext };
            request.RequestingParties.AddRange(requestingParties);
            var response = this.transactionsClient.GetFlatTransactionById(request);
            return response;
        }

        public async Task<GetFlatTransactionResponse> GetFlatTransactionByIdAsync(string ledgerId, string transactionId, IEnumerable<string> requestingParties, TraceContext traceContext = null)
        {
            var request = new GetTransactionByIdRequest { LedgerId = ledgerId, TransactionId = transactionId, TraceContext = traceContext };
            request.RequestingParties.AddRange(requestingParties);
            var response = await this.transactionsClient.GetFlatTransactionByIdAsync(request);
            return response;
        }

        public LedgerOffset GetLedgerEnd(string ledgerId, TraceContext traceContext = null)
        {
            var request = new GetLedgerEndRequest { LedgerId = ledgerId, TraceContext = traceContext };
            var response = this.transactionsClient.GetLedgerEnd(request);
            return response.Offset;
        }

        public async Task<LedgerOffset> GetLedgerEndAsync(string ledgerId, TraceContext traceContext = null)
        {
            var request = new GetLedgerEndRequest { LedgerId = ledgerId, TraceContext = traceContext };
            var response = await this.transactionsClient.GetLedgerEndAsync(request);
            return response.Offset;
        }

        public GetTransactionResponse GetTransactionByEventId(string ledgerId, string eventId, IEnumerable<string> requestingParties, TraceContext traceContext = null)
        {
            var request = new GetTransactionByEventIdRequest { LedgerId = ledgerId, EventId = eventId, TraceContext = traceContext };
            request.RequestingParties.AddRange(requestingParties);
            var response = this.transactionsClient.GetTransactionByEventId(request);
            return response;
        }

        public async Task<GetTransactionResponse> GetTransactionByEventIdAsync(string ledgerId, string eventId, IEnumerable<string> requestingParties, TraceContext traceContext = null)
        {
            var request = new GetTransactionByEventIdRequest { LedgerId = ledgerId, EventId = eventId, TraceContext = traceContext };
            request.RequestingParties.AddRange(requestingParties);
            var response = await this.transactionsClient.GetTransactionByEventIdAsync(request);
            return response;
        }

        public GetTransactionResponse GetTransactionById(string ledgerId, string transactionId, IEnumerable<string> requestingParties, TraceContext traceContext = null)
        {
            var request = new GetTransactionByIdRequest { LedgerId = ledgerId, TransactionId = transactionId, TraceContext = traceContext };
            request.RequestingParties.AddRange(requestingParties);
            var response = this.transactionsClient.GetTransactionById(request);
            return response;
        }

        public async Task<GetTransactionResponse> GetTransactionByIdAsync(string ledgerId, string transactionId, IEnumerable<string> requestingParties, TraceContext traceContext = null)
        {
            var request = new GetTransactionByIdRequest { LedgerId = ledgerId, TransactionId = transactionId, TraceContext = traceContext };
            request.RequestingParties.AddRange(requestingParties);
            var response = await this.transactionsClient.GetTransactionByIdAsync(request);
            return response;
        }

        public IAsyncEnumerator<GetTransactionsResponse> GetTransactions(
            string ledgerId,
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            TraceContext traceContext = null)
        {
            var request = new GetTransactionsRequest
                {
                    LedgerId = ledgerId,
                    Filter = transactionFilter,
                    Begin = beginOffset,
                    End = endOffset,
                    Verbose = verbose,
                    TraceContext = traceContext
                };
            var call = this.transactionsClient.GetTransactions(request);
            return call.ResponseStream;
        }

        public IEnumerable<GetTransactionsResponse> GetTransactionsSync(
            string ledgerId,
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            TraceContext traceContext = null)
        {
            using (var stream = this.GetTransactions(ledgerId, transactionFilter, beginOffset, endOffset, verbose, traceContext))
            {
                while (stream.MoveNext().Result)
                {
                    yield return stream.Current;
                }
            }
        }

        public IAsyncEnumerator<GetTransactionTreesResponse> GetTransactionTrees(
            string ledgerId,
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            TraceContext traceContext = null)
        {
            var request = new GetTransactionsRequest
                {
                    LedgerId = ledgerId,
                    Filter = transactionFilter,
                    Begin = beginOffset,
                    End = endOffset,
                    Verbose = verbose,
                    TraceContext = traceContext
                };
            var call = this.transactionsClient.GetTransactionTrees(request);
            return call.ResponseStream;
        }

        public IEnumerable<GetTransactionTreesResponse> GetTransactionTreesSync(
            string ledgerId,
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            TraceContext traceContext = null)
        {
            using (var stream = this.GetTransactionTrees(ledgerId, transactionFilter, beginOffset, endOffset, verbose, traceContext))
            {
                while (stream.MoveNext().Result)
                {
                    yield return stream.Current;
                }
            }
        }
    }
}
