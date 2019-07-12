// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using System.Collections.Generic;
    using Com.DigitalAsset.Ledger.Api.V1;
    using Grpc.Core;

    public class LedgerConfigurationClient : ILedgerConfigurationClient
    {
        private readonly LedgerConfigurationService.LedgerConfigurationServiceClient ledgerConfigurationClient;

        public LedgerConfigurationClient(Channel channel)
        {
            this.ledgerConfigurationClient = new LedgerConfigurationService.LedgerConfigurationServiceClient(channel);
        }

        public IAsyncEnumerator<GetLedgerConfigurationResponse> GetLedgerConfiguration(string ledgerId, TraceContext traceContext = null)
        {
            var request = new GetLedgerConfigurationRequest { LedgerId = ledgerId, TraceContext = traceContext };
            var call = this.ledgerConfigurationClient.GetLedgerConfiguration(request);
            return call.ResponseStream;
        }


        public IEnumerable<GetLedgerConfigurationResponse> GetLedgerConfigurationSync(string ledgerId, TraceContext traceContext = null)
        {
            var r = this.GetLedgerConfiguration(ledgerId, traceContext);
            while (r.MoveNext().Result)
            {
                yield return r.Current;
            }
        }
    }
}
