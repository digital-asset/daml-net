// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Automation
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1;
    using Daml.Ledger.Client;

    public class StatefulBot<TState>
    {
        private readonly ITransactionsClient transactionsClient;
        private readonly ICommandClient commandClient;
        private readonly Func<TState, Transaction, TState> update;
        private readonly Func<TState, Commands> handler;
        private TState state;

        public StatefulBot(
            ITransactionsClient transactionsClient,
            ICommandClient commandClient,
            TState initial,
            Func<TState, Transaction, TState> update,
            Func<TState, Commands> handler)
        {
            this.transactionsClient = transactionsClient;
            this.commandClient = commandClient;
            this.state = initial;
            this.update = update;
            this.handler = handler;
        }

        public async Task Run(string ledgerId, TransactionFilter transactionFilter, LedgerOffset beginOffset, LedgerOffset endOffset, bool verbose, TraceContext traceContext = null)
        {
            using (var stream = this.transactionsClient.GetTransactions(ledgerId, transactionFilter, beginOffset, endOffset, verbose, traceContext))
            {
                while (stream.MoveNext().Result)
                {
                    foreach (var tx in stream.Current.Transactions)
                    {
                        this.state = update(this.state, tx);
                        var commands = handler(this.state);
                        await this.commandClient.SubmitAndWaitAsync(commands);
                    }
                }
            }
        }
    }
}
