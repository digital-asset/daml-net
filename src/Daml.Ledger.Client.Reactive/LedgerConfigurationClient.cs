// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Reactive.Concurrency;

namespace Daml.Ledger.Client.Reactive
{
    using Daml.Ledger.Client.Reactive.Util;
    using Daml.Ledger.Api.Data.Util;

    using GetLedgerConfigurationResponse = Com.DigitalAsset.Ledger.Api.V1.GetLedgerConfigurationResponse;
    using TraceContext = Com.DigitalAsset.Ledger.Api.V1.TraceContext;

    public class LedgerConfigurationClient
    {
        private readonly ILedgerConfigurationClient _ledgerConfigurationClient;
        private readonly IScheduler _scheduler;

        public LedgerConfigurationClient(ILedgerConfigurationClient ledgerConfigurationClient, IScheduler scheduler = null)
        {
            _ledgerConfigurationClient = ledgerConfigurationClient;
            _scheduler = scheduler;
        }

        public IObservable<GetLedgerConfigurationResponse> GetLedgerConfiguration(Optional<string> accessToken = null, TraceContext traceContext = null)
        {
            return _ledgerConfigurationClient.GetLedgerConfiguration(accessToken?.Reduce((string) null), traceContext).CreateAsyncObservable(_scheduler);
        }
    }
}
