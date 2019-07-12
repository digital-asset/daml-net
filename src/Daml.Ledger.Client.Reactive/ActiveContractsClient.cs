// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Reactive
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Linq;
    using Com.DigitalAsset.Ledger.Api.V1;
    using Daml.Ledger.Client;

    public class ActiveContractsClient
    {
        private readonly IActiveContractsClient activeContractsClient;

        public ActiveContractsClient(IActiveContractsClient activeContractsClient)
        {
            this.activeContractsClient = activeContractsClient;
        }

        public IObservable<GetActiveContractsResponse> GetActiveContracts(string ledgerId, TransactionFilter transactionFilter, bool verbose = true, TraceContext traceContext = null)
        {
            var observable = Observable.Create<GetActiveContractsResponse>(async observer =>
            {
                using (var stream = this.activeContractsClient.GetActiveContracts(ledgerId, transactionFilter, verbose, traceContext))
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
