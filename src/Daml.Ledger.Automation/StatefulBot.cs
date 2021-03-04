// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Threading.Tasks;

namespace Daml.Ledger.Automation
{
    using Com.Daml.Ledger.Api.V1;
    using Daml.Ledger.Client;

    public class StatefulBot<TState>
    {
        private readonly ITransactionsClient _transactionsClient;
        private readonly ICommandClient _commandClient;
        private readonly Func<TState, Transaction, TState> _update;
        private readonly Func<TState, Commands> _handler;
        private TState _state;

        public StatefulBot(
            ITransactionsClient transactionsClient,
            ICommandClient commandClient,
            TState initial,
            Func<TState, Transaction, TState> update,
            Func<TState, Commands> handler)
        {
            _transactionsClient = transactionsClient;
            _commandClient = commandClient;
            _state = initial;
            _update = update;
            _handler = handler;
        }

        public async Task Run(TransactionFilter transactionFilter, LedgerOffset beginOffset, LedgerOffset endOffset, bool verbose, string accessToken = null, TraceContext traceContext = null)
        {
            await using (var stream = _transactionsClient.GetTransactions(transactionFilter, beginOffset, endOffset, verbose, accessToken, traceContext))
            {
                while (await stream.MoveNextAsync())
                {
                    foreach (var tx in stream.Current.Transactions)
                    {
                        _state = _update(_state, tx);
                        var commands = _handler(_state);
                        await _commandClient.SubmitAndWaitAsync(commands);
                    }
                }
            }
        }
    }
}
