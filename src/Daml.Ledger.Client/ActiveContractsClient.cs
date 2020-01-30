// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using System.Collections.Generic;
    using Com.DigitalAsset.Ledger.Api.V1;
    using Grpc.Core;

    public class ActiveContractsClient : IActiveContractsClient
    {
        private readonly string _ledgerId;
        private readonly ActiveContractsService.ActiveContractsServiceClient _activeContractsClient;

        public ActiveContractsClient(string ledgerId, Channel channel)
        {
            _ledgerId = ledgerId;
            _activeContractsClient = new ActiveContractsService.ActiveContractsServiceClient(channel);
        }

        public IAsyncEnumerator<GetActiveContractsResponse> GetActiveContracts(
            TransactionFilter transactionFilter,
            bool verbose = true,
            TraceContext traceContext = null)
        {
            var request = new GetActiveContractsRequest { LedgerId = _ledgerId, Filter = transactionFilter, Verbose = verbose, TraceContext = traceContext };
            var response = _activeContractsClient.GetActiveContracts(request);
            return response.ResponseStream;
        }

        public IEnumerable<GetActiveContractsResponse> GetActiveContractsSync(
            TransactionFilter transactionFilter,
            bool verbose = true,
            TraceContext traceContext = null)
        {
            using (var stream = GetActiveContracts(transactionFilter, verbose, traceContext))
            {
                while (stream.MoveNext().Result)
                {
                    yield return stream.Current;
                }
            }
        }
    }
}
