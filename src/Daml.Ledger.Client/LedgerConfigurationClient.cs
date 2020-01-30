// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using System.Collections.Generic;
    using Com.DigitalAsset.Ledger.Api.V1;
    using Daml.Ledger.Client.Auth.Client;
    using Grpc.Core;

    public class LedgerConfigurationClient : ILedgerConfigurationClient
    {
        private readonly string _ledgerId;
        private readonly ClientStub<LedgerConfigurationService.LedgerConfigurationServiceClient> _ledgerConfigurationClient;

        public LedgerConfigurationClient(string ledgerId, Channel channel)
        {
            _ledgerId = ledgerId;
            _ledgerConfigurationClient = new ClientStub<LedgerConfigurationService.LedgerConfigurationServiceClient>(new LedgerConfigurationService.LedgerConfigurationServiceClient(channel));
        }

        public IAsyncEnumerator<GetLedgerConfigurationResponse> GetLedgerConfiguration(TraceContext traceContext = null)
        {
            var request = new GetLedgerConfigurationRequest { LedgerId = _ledgerId, TraceContext = traceContext };
            var response = _ledgerConfigurationClient.Dispatch(request, (c, r, co) => c.GetLedgerConfiguration(r, co));
            return response.ResponseStream;
        }

        public IEnumerable<GetLedgerConfigurationResponse> GetLedgerConfigurationSync(TraceContext traceContext = null)
        {
            var r = GetLedgerConfiguration(traceContext);
            while (r.MoveNext().Result)
            {
                yield return r.Current;
            }
        }
    }
}
