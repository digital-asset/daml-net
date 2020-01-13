// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using System.Collections.Generic;
    using Com.DigitalAsset.Ledger.Api.V1;
    using Daml.Ledger.Client.Auth.Client;
    using Grpc.Core;

    public class ActiveContractsClient : IActiveContractsClient
    {
        private readonly string _ledgerId;
        private readonly ClientStub<ActiveContractsService.ActiveContractsServiceClient> _activeContractsClient;

        public ActiveContractsClient(string ledgerId, Channel channel, string accessToken)
        {
            _ledgerId = ledgerId;
            _activeContractsClient = new ClientStub<ActiveContractsService.ActiveContractsServiceClient>(new ActiveContractsService.ActiveContractsServiceClient(channel), accessToken);
        }

        public IAsyncEnumerator<GetActiveContractsResponse> GetActiveContracts(
            TransactionFilter transactionFilter,
            bool verbose,
            string accessToken,
            TraceContext traceContext)
        {
            var request = new GetActiveContractsRequest { LedgerId = _ledgerId, Filter = transactionFilter, Verbose = verbose, TraceContext = traceContext };

            var response = _activeContractsClient.WithAccess(accessToken).DispatchRequest(request, (c, r, co) => c.GetActiveContracts(r, co), (c, r) => c.GetActiveContracts(r));

            return response.ResponseStream;
        }

        public IEnumerable<GetActiveContractsResponse> GetActiveContractsSync(
            TransactionFilter transactionFilter,
            bool verbose,
            string accessToken,
            TraceContext traceContext)
        {
            using (var stream = GetActiveContracts(transactionFilter, verbose, accessToken, traceContext))
            {
                while (stream.MoveNext().Result)
                {
                    yield return stream.Current;
                }
            }
        }
    }
}
