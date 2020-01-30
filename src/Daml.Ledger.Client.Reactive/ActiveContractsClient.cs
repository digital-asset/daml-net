﻿// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Reactive
{
    using System;
    using System.Reactive.Concurrency;
    using Com.DigitalAsset.Ledger.Api.V1;
    using Daml.Ledger.Client.Reactive.Util;

    public class ActiveContractsClient
    {
        private readonly IActiveContractsClient _activeContractsClient;
        private readonly IScheduler _scheduler;

        public ActiveContractsClient(IActiveContractsClient activeContractsClient, IScheduler scheduler = null)
        {
            _activeContractsClient = activeContractsClient;
            _scheduler = scheduler;
        }

        public IObservable<GetActiveContractsResponse> GetActiveContracts(string ledgerId, TransactionFilter transactionFilter, bool verbose = true, TraceContext traceContext = null)
        {
            return _activeContractsClient.GetActiveContracts(ledgerId, transactionFilter, verbose, traceContext).CreateAsyncObservable(_scheduler);
        }
    }
}
