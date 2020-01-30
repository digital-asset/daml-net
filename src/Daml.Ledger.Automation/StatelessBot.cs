// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace DigitalAsset.Ledger.Automation
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1;
    using Daml.Ledger.Client;
    using Grpc.Core;

    public class StatelessBot
    {
        private readonly ITransactionsClient _transactionClient;
        private readonly ICommandClient _commandClient;
        private readonly Func<Transaction, Commands> _handler;

        public StatelessBot(string ledgerId, Channel channel, string accessToken, Func<Transaction, Commands> handler)
        {
            _transactionClient = new TransactionsClient(ledgerId, channel, accessToken);
            _commandClient = new CommandClient(ledgerId, channel, accessToken);
            _handler = handler;
        }

        public async Task Run(
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            string accessToken = null,
            TraceContext traceContext = null)
        {
            using (var stream = _transactionClient.GetTransactions(transactionFilter, beginOffset, endOffset, verbose, accessToken, traceContext))
            {
                while (stream.MoveNext().Result)
                {
                    foreach (var tx in stream.Current.Transactions)
                    {
                        var commands = _handler(tx);
                        await _commandClient.SubmitAndWaitAsync(commands);
                    }
                }
            }
        }
    }
}
