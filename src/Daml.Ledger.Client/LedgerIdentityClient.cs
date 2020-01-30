// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1;
    using Daml.Ledger.Client.Auth.Client;
    using Grpc.Core;

    public class LedgerIdentityClient : ILedgerIdentityClient
    {
        private readonly ClientStub<LedgerIdentityService.LedgerIdentityServiceClient> _ledgerIdentityClient;

        public LedgerIdentityClient(Channel channel, string accessToken)
        {
            _ledgerIdentityClient = new ClientStub<LedgerIdentityService.LedgerIdentityServiceClient>(new LedgerIdentityService.LedgerIdentityServiceClient(channel), accessToken);
        }

        public string GetLedgerIdentity(string accessToken = null)
        {
            var response = _ledgerIdentityClient.WithAccess(accessToken).Dispatch(new GetLedgerIdentityRequest(), (c, r, co) => c.GetLedgerIdentity(r, co));
            return response.LedgerId;
        }

        public async Task<string> GetLedgerIdentityAsync(string accessToken = null)
        {
            var response = await _ledgerIdentityClient.WithAccess(accessToken).Dispatch(new GetLedgerIdentityRequest(), (c, r, co) => c.GetLedgerIdentityAsync(r, co));
            return response.LedgerId;
        }
    }
}
