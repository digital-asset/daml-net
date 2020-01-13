// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Grpc.Core;

namespace Daml.Ledger.Client.Reactive
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Concurrency;
    using System.Reactive.Linq;
    using Com.Daml.Ledger.Api.Data;
    using Com.Daml.Ledger.Api.Util;
    using Util;

    using Single = Com.Daml.Ledger.Api.Util.Single;
    using Transaction = Com.Daml.Ledger.Api.Data.Transaction;
    using LedgerOffset = Com.Daml.Ledger.Api.Data.LedgerOffset;
    using TransactionFilter = Com.Daml.Ledger.Api.Data.TransactionFilter;
    using TransactionTree = Com.Daml.Ledger.Api.Data.TransactionTree;
    using TraceContext = Com.DigitalAsset.Ledger.Api.V1.TraceContext;

    public class TransactionsClient
    {
        private readonly ITransactionsClient _transactionClient;
        private readonly IScheduler _scheduler;

        public TransactionsClient(string ledgerId, Channel channel, Optional<string> accessToken, IScheduler scheduler = null)
        {
            _transactionClient = new Client.TransactionsClient(ledgerId, channel, accessToken.Reduce((string) null));
            _scheduler = scheduler;
        }

        public IObservable<Transaction> GetTransactions(TransactionFilter transactionFilter, LedgerOffset beginOffset, LedgerOffset endOffset = null, bool verbose = true, Optional<string> accessToken = null, TraceContext traceContext = null)
        {
            return _transactionClient.GetTransactions(transactionFilter?.ToProto(), beginOffset?.ToProto(), endOffset?.ToProto(), verbose, accessToken?.Reduce((string) null), traceContext).CreateAsyncObservable(_scheduler).Select(GetTransactionsResponse.FromProto).SelectMany(r => r.Transactions);
        }

        public IObservable<TransactionTree> GetTransactionsTrees(TransactionFilter transactionFilter, LedgerOffset beginOffset, LedgerOffset endOffset = null, bool verbose = true, Optional<string> accessToken = null, TraceContext traceContext = null)
        {
            return _transactionClient.GetTransactionTrees(transactionFilter?.ToProto(), beginOffset?.ToProto(), endOffset?.ToProto(), verbose, accessToken?.Reduce((string) null), traceContext).CreateAsyncObservable(_scheduler).Select(GetTransactionTreesResponse.FromProto).SelectMany(r => r.Transactions);
        }

        public Single<TransactionTree> GetTransactionByEventId(string eventId, IEnumerable<string> requestingParties, Optional<string> accessToken = null, TraceContext traceContext = null)
        {
            return Single.Just(TransactionTree.FromProto(_transactionClient.GetTransactionByEventId(eventId, requestingParties, accessToken?.Reduce((string) null), traceContext).Transaction));
        }

        public Single<TransactionTree> GetTransactionById(string transactionId, IEnumerable<string> requestingParties, Optional<string> accessToken = null, TraceContext traceContext = null)
        {
            return Single.Just(TransactionTree.FromProto(_transactionClient.GetTransactionById(transactionId, requestingParties, accessToken?.Reduce((string) null), traceContext).Transaction));
        }

        public Single<Transaction> GetFlatTransactionByEventId(string eventId, HashSet<string> requestingParties, Optional<string> accessToken = null, TraceContext traceContext = null)
        {
            return Single.Just(Transaction.FromProto(_transactionClient.GetFlatTransactionByEventId(eventId, requestingParties, accessToken?.Reduce((string) null), traceContext).Transaction));
        }

        public Single<Transaction> GetFlatTransactionById(string transactionId, HashSet<string> requestingParties, Optional<string> accessToken = null, TraceContext traceContext = null)
        {
            return Single.Just(Transaction.FromProto(_transactionClient.GetFlatTransactionById(transactionId, requestingParties, accessToken?.Reduce((string) null), traceContext).Transaction));
        }

        public Single<LedgerOffset> GetLedgerEnd(Optional<string> accessToken = null, TraceContext traceContext = null)
        {
            return Single.Just(LedgerOffset.FromProto(_transactionClient.GetLedgerEnd(accessToken?.Reduce((string) null), traceContext)));
        }
    }
}
