// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using System.Collections.Generic;
    using Com.DigitalAsset.Ledger.Api.V1;
    using Grpc.Core;

    public class ActiveContractsClient : IActiveContractsClient
    {
        private readonly ActiveContractsService.ActiveContractsServiceClient activeContractsClient;

        public ActiveContractsClient(Channel channel)
        {
            this.activeContractsClient = new ActiveContractsService.ActiveContractsServiceClient(channel);
        }

        public IAsyncEnumerator<GetActiveContractsResponse> GetActiveContracts(
            string ledgerId,
            TransactionFilter transactionFilter,
            bool verbose = true,
            TraceContext traceContext = null)
        {
            var request = new GetActiveContractsRequest { LedgerId = ledgerId, Filter = transactionFilter, Verbose = verbose, TraceContext = traceContext };
            var call = this.activeContractsClient.GetActiveContracts(request);
            return call.ResponseStream;
        }

        public IEnumerable<GetActiveContractsResponse> GetActiveContractsSync(
            string ledgerId,
            TransactionFilter transactionFilter,
            bool verbose = true,
            TraceContext traceContext = null)
        {
            using (var stream = this.GetActiveContracts(ledgerId, transactionFilter, verbose, traceContext))
            {
                while (stream.MoveNext().Result)
                {
                    yield return stream.Current;
                }
            }
        }
    }
}
