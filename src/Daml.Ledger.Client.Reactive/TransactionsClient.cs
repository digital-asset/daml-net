// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Reactive
{
    using System;
    using System.Reactive.Concurrency;
    using Com.DigitalAsset.Ledger.Api.V1;
    using Client;
    using Daml.Ledger.Client.Reactive.Util;

    public class TransactionsClient
    {
        private readonly ITransactionsClient _transactionsClient;
        private readonly IScheduler _scheduler;

        public TransactionsClient(ITransactionsClient transactionsClient, IScheduler scheduler)
        {
            _transactionsClient = transactionsClient;
            _scheduler = scheduler;
        }

        public IObservable<GetTransactionsResponse> GetTransactions(
            string ledgerId,
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            TraceContext traceContext = null)
        {
            return _transactionsClient.GetTransactions(ledgerId, transactionFilter, beginOffset, endOffset, verbose, traceContext).CreateAsyncObservable(_scheduler);
        }

        public IObservable<GetTransactionTreesResponse> GetTransactionTrees(
            string ledgerId,
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            TraceContext traceContext = null)
        {
            return _transactionsClient.GetTransactionTrees(ledgerId, transactionFilter, beginOffset, endOffset, verbose, traceContext).CreateAsyncObservable(_scheduler);
        }
    }
}
