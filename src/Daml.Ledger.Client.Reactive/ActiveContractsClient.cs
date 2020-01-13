// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Grpc.Core;

namespace Daml.Ledger.Client.Reactive
{
    using System;
    using System.Reactive.Concurrency;
    using System.Reactive.Linq;
    using Util;
    using Com.Daml.Ledger.Api.Util;

    using GetActiveContractsResponse = Com.Daml.Ledger.Api.Data.GetActiveContractsResponse;
    using TransactionFilter = Com.Daml.Ledger.Api.Data.TransactionFilter;
    using TraceContext = Com.DigitalAsset.Ledger.Api.V1.TraceContext;

    public class ActiveContractsClient
    {
        private readonly IActiveContractsClient _activeContractsClient;
        private readonly IScheduler _scheduler;

        public ActiveContractsClient(string ledgerId, Channel channel, Optional<string> accessToken, IScheduler scheduler = null)
        {
            _activeContractsClient = new Client.ActiveContractsClient(ledgerId, channel, accessToken.Reduce((string) null));
            _scheduler = scheduler;
        }

        public IObservable<GetActiveContractsResponse> GetActiveContracts(TransactionFilter transactionFilter, bool verbose = true, Optional<string> accessToken = null, TraceContext traceContext = null)
        {
            var response = _activeContractsClient.GetActiveContracts(transactionFilter.ToProto(), verbose, accessToken?.Reduce((string) null), traceContext);

            return response.CreateAsyncObservable(_scheduler).Select(GetActiveContractsResponse.FromProto);
        }
    }
}
