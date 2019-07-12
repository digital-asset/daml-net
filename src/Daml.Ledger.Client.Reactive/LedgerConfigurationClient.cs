// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Reactive
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Linq;
    using Com.DigitalAsset.Ledger.Api.V1;

    public class LedgerConfigurationClient
    {
        private readonly ILedgerConfigurationClient ledgerConfigurationClient;

        public LedgerConfigurationClient(ILedgerConfigurationClient ledgerConfigurationClient)
        {
            this.ledgerConfigurationClient = ledgerConfigurationClient;
        }

        public IObservable<GetLedgerConfigurationResponse> GetLedgerConfiguration(string ledgerId, TraceContext traceContext = null)
        {
            var observable = Observable.Create<GetLedgerConfigurationResponse>(async observer =>
            {
                using (var stream = this.ledgerConfigurationClient.GetLedgerConfiguration(ledgerId, traceContext))
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
