// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1;
    using Grpc.Core;

    public class LedgerIdentityClient : ILedgerIdentityClient
    {
        private readonly LedgerIdentityService.LedgerIdentityServiceClient ledgerIdentityClient;

        public LedgerIdentityClient(Channel channel)
        {
            this.ledgerIdentityClient = new LedgerIdentityService.LedgerIdentityServiceClient(channel);
        }

        public string GetLedgerIdentity()
        {
            var request = new GetLedgerIdentityRequest();
            var response = this.ledgerIdentityClient.GetLedgerIdentity(request);
            return response.LedgerId;
        }

        public async Task<string> GetLedgerIdentityAsync()
        {
            var request = new GetLedgerIdentityRequest();
            var response = await this.ledgerIdentityClient.GetLedgerIdentityAsync(request);
            return response.LedgerId;
        }
    }
}
