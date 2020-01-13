// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Com.Daml.Ledger.Api.Util;
using Grpc.Core;

namespace Daml.Ledger.Client.Reactive
{
    using System;
    using System.Reactive.Concurrency;
    using System.Reactive.Linq;
    using Util;

    using LedgerConfiguration = Com.DigitalAsset.Ledger.Api.V1.LedgerConfiguration;
    using TraceContext = Com.DigitalAsset.Ledger.Api.V1.TraceContext;

    public class LedgerConfigurationClient
    {
        private readonly ILedgerConfigurationClient _ledgerConfigurationClient;
        private readonly IScheduler _scheduler;

        public LedgerConfigurationClient(string ledgerId, Channel channel, Optional<string> accessToken, IScheduler scheduler = null)
        {
            _ledgerConfigurationClient = new Client.LedgerConfigurationClient(ledgerId, channel, accessToken.Reduce((string) null));
            _scheduler = scheduler;
        }

        public IObservable<LedgerConfiguration> GetLedgerConfiguration(Optional<string> accessToken = null, TraceContext traceContext = null)
        {
            return _ledgerConfigurationClient.GetLedgerConfiguration(accessToken?.Reduce((string) null), traceContext).CreateAsyncObservable(_scheduler).Select(r => r.LedgerConfiguration);
        }
    }
}
