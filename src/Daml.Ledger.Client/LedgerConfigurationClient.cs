// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using Grpc.Core;
using Grpc.Core.Utils;

namespace Daml.Ledger.Client
{
    using Com.Daml.Ledger.Api.V1;
    using Daml.Ledger.Client.Auth.Client;

    public class LedgerConfigurationClient : ILedgerConfigurationClient
    {
        private readonly ClientStub<LedgerConfigurationService.LedgerConfigurationServiceClient> _ledgerConfigurationClient;

        public LedgerConfigurationClient(string ledgerId, Channel channel, string accessToken)
        {
            LedgerId = ledgerId;
            _ledgerConfigurationClient = new ClientStub<LedgerConfigurationService.LedgerConfigurationServiceClient>(new LedgerConfigurationService.LedgerConfigurationServiceClient(channel), accessToken);
        }

        public string LedgerId { get; }

        public IAsyncEnumerator<GetLedgerConfigurationResponse> GetLedgerConfiguration(string accessToken = null, TraceContext traceContext = null)
        {
            return GetLedgerConfigurationImpl(accessToken, traceContext).ReadAllAsync().GetAsyncEnumerator();
        }

        public IEnumerable<GetLedgerConfigurationResponse> GetLedgerConfigurationSync(string accessToken = null, TraceContext traceContext = null)
        {
            return GetLedgerConfigurationImpl(accessToken, traceContext).ToListAsync().Result;
            }

        private IAsyncStreamReader<GetLedgerConfigurationResponse> GetLedgerConfigurationImpl(string accessToken, TraceContext traceContext)
        {
            var request = new GetLedgerConfigurationRequest { LedgerId = LedgerId, TraceContext = traceContext };
            var response = _ledgerConfigurationClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.GetLedgerConfiguration(r, co));
            return response.ResponseStream;
        }
    }
}
