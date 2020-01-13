﻿// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Daml.Ledger.Client.Auth.Client;

namespace Daml.Ledger.Client
{
    using System.Collections.Generic;
    using Com.DigitalAsset.Ledger.Api.V1;
    using Grpc.Core;

    public class LedgerConfigurationClient : ILedgerConfigurationClient
    {
        private readonly string _ledgerId;
        private readonly ClientStub<LedgerConfigurationService.LedgerConfigurationServiceClient> _ledgerConfigurationClient;

        public LedgerConfigurationClient(string ledgerId, Channel channel, string accessToken)
        {
            _ledgerId = ledgerId;
            _ledgerConfigurationClient = new ClientStub<LedgerConfigurationService.LedgerConfigurationServiceClient>(new LedgerConfigurationService.LedgerConfigurationServiceClient(channel), accessToken);
        }

        public IAsyncEnumerator<GetLedgerConfigurationResponse> GetLedgerConfiguration(string accessToken = null, TraceContext traceContext = null)
        {
            var request = new GetLedgerConfigurationRequest { LedgerId = _ledgerId, TraceContext = traceContext };

            var response = _ledgerConfigurationClient.WithAccess(accessToken).DispatchRequest(request, (c, r, co) => c.GetLedgerConfiguration(r, co), (c, r) => c.GetLedgerConfiguration(r));

            return response.ResponseStream;
        }

        public IEnumerable<GetLedgerConfigurationResponse> GetLedgerConfigurationSync(string accessToken = null, TraceContext traceContext = null)
        {
            var r = GetLedgerConfiguration(accessToken, traceContext);
            while (r.MoveNext().Result)
            {
                yield return r.Current;
            }
        }
    }
}
