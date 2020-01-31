// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Reactive.Concurrency;

namespace Daml.Ledger.Client.Reactive
{
    using Com.DigitalAsset.Ledger.Api.V1;
    using Daml.Ledger.Client.Reactive.Util;
    using Daml.Ledger.Api.Data.Util;
 
    public class ActiveContractsClient
    {
        private readonly IActiveContractsClient _activeContractsClient;
        private readonly IScheduler _scheduler;

        public ActiveContractsClient(IActiveContractsClient activeContractsClient, IScheduler scheduler = null)
        {
            _activeContractsClient = activeContractsClient;
            _scheduler = scheduler;
        }

        public IObservable<GetActiveContractsResponse> GetActiveContracts(TransactionFilter transactionFilter, bool verbose = true, Optional<string> accessToken = null, TraceContext traceContext = null)
        {
            return _activeContractsClient.GetActiveContracts(transactionFilter, verbose, accessToken?.Reduce((string) null), traceContext).CreateAsyncObservable(_scheduler);
        }
    }
}
