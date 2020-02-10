// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using Grpc.Core;

namespace Daml.Ledger.Client
{
    using Com.DigitalAsset.Ledger.Api.V1;
    using Daml.Ledger.Client.Auth.Client;

    public class ActiveContractsClient : IActiveContractsClient
    {
        private readonly ClientStub<ActiveContractsService.ActiveContractsServiceClient> _activeContractsClient;

        public ActiveContractsClient(string ledgerId, Channel channel, string accessToken)
        {
            LedgerId = ledgerId;
            _activeContractsClient = new ClientStub<ActiveContractsService.ActiveContractsServiceClient>(new ActiveContractsService.ActiveContractsServiceClient(channel), accessToken);
        }

        public string LedgerId { get; }

        public IAsyncEnumerator<GetActiveContractsResponse> GetActiveContracts(
            TransactionFilter transactionFilter,
            bool verbose = true,
            string accessToken = null,
            TraceContext traceContext = null)
        {
            var request = new GetActiveContractsRequest { LedgerId = LedgerId, Filter = transactionFilter, Verbose = verbose, TraceContext = traceContext };
            var response = _activeContractsClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.GetActiveContracts(r, co));

            return response.ResponseStream;
        }

        public IEnumerable<GetActiveContractsResponse> GetActiveContractsSync(
            TransactionFilter transactionFilter,
            bool verbose = true,
            string accessToken = null,
            TraceContext traceContext = null)
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
