// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Reactive
{
    using System;
    using System.Reactive.Concurrency;
    using Com.DigitalAsset.Ledger.Api.V1;
    using Daml.Ledger.Client.Reactive.Util;

    public class LedgerConfigurationClient
    {
        private readonly ILedgerConfigurationClient _ledgerConfigurationClient;
        private readonly IScheduler _scheduler;

        public LedgerConfigurationClient(ILedgerConfigurationClient ledgerConfigurationClient, IScheduler scheduler)
        {
            _ledgerConfigurationClient = ledgerConfigurationClient;
            _scheduler = scheduler;
        }

        public IObservable<GetLedgerConfigurationResponse> GetLedgerConfiguration(string ledgerId, TraceContext traceContext = null)
        {
            return _ledgerConfigurationClient.GetLedgerConfiguration(ledgerId, traceContext).CreateAsyncObservable(_scheduler);
        }
    }
}
