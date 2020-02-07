// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Reactive.Concurrency;

namespace Daml.Ledger.Client.Reactive
{
    using Client;
    using Daml.Ledger.Client.Reactive.Util;
    using Daml.Ledger.Api.Data.Util;

    using GetTransactionsResponse = Com.DigitalAsset.Ledger.Api.V1.GetTransactionsResponse;
    using TransactionFilter = Com.DigitalAsset.Ledger.Api.V1.TransactionFilter;
    using LedgerOffset = Com.DigitalAsset.Ledger.Api.V1.LedgerOffset;
    using TraceContext = Com.DigitalAsset.Ledger.Api.V1.TraceContext;
    using GetTransactionTreesResponse = Com.DigitalAsset.Ledger.Api.V1.GetTransactionTreesResponse;

    public class TransactionsClient
    {
        private readonly ITransactionsClient _transactionsClient;
        private readonly IScheduler _scheduler;

        public TransactionsClient(ITransactionsClient transactionsClient, IScheduler scheduler = null)
        {
            _transactionsClient = transactionsClient;
            _scheduler = scheduler;
        }

        public IObservable<GetTransactionsResponse> GetTransactions(
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            Optional<string> accessToken = null, 
            TraceContext traceContext = null)
        {
            return _transactionsClient.GetTransactions(transactionFilter, beginOffset, endOffset, verbose, accessToken?.Reduce((string) null), traceContext).CreateAsyncObservable(_scheduler);
        }

        public IObservable<GetTransactionTreesResponse> GetTransactionTrees(
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            Optional<string> accessToken = null, 
            TraceContext traceContext = null)
        {
            return _transactionsClient.GetTransactionTrees(transactionFilter, beginOffset, endOffset, verbose, accessToken?.Reduce((string) null), traceContext).CreateAsyncObservable(_scheduler);
        }
    }
}
