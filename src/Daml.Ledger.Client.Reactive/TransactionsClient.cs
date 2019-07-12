// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Reactive
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Linq;
    using Com.DigitalAsset.Ledger.Api.V1;
    using Daml.Ledger.Client;

    public class TransactionsClient
    {
        private readonly ITransactionsClient transactionsClient;

        public TransactionsClient(ITransactionsClient transactionsClient)
        {
            this.transactionsClient = transactionsClient;
        }

        public IObservable<GetTransactionsResponse> GetTransactions(
            string ledgerId,
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            TraceContext traceContext = null)
        {
            var observable = Observable.Create<GetTransactionsResponse>(async observer =>
            {
                using (var stream = this.transactionsClient.GetTransactions(ledgerId, transactionFilter, beginOffset, endOffset, verbose, traceContext))
                {
                    var hasNext = await stream.MoveNext();
                    while (hasNext)
                    {
                        observer.OnNext(stream.Current);
                        hasNext = await stream.MoveNext();
                    }
                }
            });

            return observable;
        }

        public IObservable<GetTransactionTreesResponse> GetTransactionTrees(
            string ledgerId,
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            TraceContext traceContext = null)
        {
            var observable = Observable.Create<GetTransactionTreesResponse>(async observer =>
            {
                using (var stream = this.transactionsClient.GetTransactionTrees(ledgerId, transactionFilter, beginOffset, endOffset, verbose, traceContext))
                {
                    var hasNext = await stream.MoveNext();
                    while (hasNext)
                    {
                        observer.OnNext(stream.Current);
                        hasNext = await stream.MoveNext();
                    }
                }
            });

            return observable;
        }
    }
}
