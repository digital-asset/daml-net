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
        private readonly ITransactionsClient transactionClient;
        private readonly ICommandClient commandClient;
        private readonly Func<Transaction, Commands> handler;

        public StatelessBot(Channel channel, Func<Transaction, Commands> handler)
        {
            this.transactionClient = new TransactionsClient(channel);
            this.commandClient = new CommandClient(channel);
            this.handler = handler;
        }

        public async Task Run(
            string ledgerId,
            TransactionFilter transactionFilter,
            LedgerOffset beginOffset,
            LedgerOffset endOffset = null,
            bool verbose = true,
            TraceContext traceContext = null)
        {
            using (var stream = this.transactionClient.GetTransactions(ledgerId, transactionFilter, beginOffset, endOffset, verbose, traceContext))
            {
                while (stream.MoveNext().Result)
                {
                    foreach (var tx in stream.Current.Transactions)
                    {
                        var commands = handler(tx);
                        await this.commandClient.SubmitAndWaitAsync(commands);
                    }
                }
            }
        }
    }
}
