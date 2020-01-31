// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Threading.Tasks;
using Grpc.Core;

namespace Daml.Ledger.Client.Testing
{
    using Com.DigitalAsset.Ledger.Api.V1.Testing;
    using Daml.Ledger.Client.Auth.Client;

    public class ResetClient : IResetClient
    {
        private readonly string _ledgerId;
        private readonly ClientStub<ResetService.ResetServiceClient> _resetClient;

        public ResetClient(string ledgerId, Channel channel, string accessToken)
        {
            _ledgerId = ledgerId;
            _resetClient = new ClientStub<ResetService.ResetServiceClient>(new ResetService.ResetServiceClient(channel), accessToken);
        }

        public void Reset(string accessToken = null)
        {
            _resetClient.WithAccess(accessToken).Dispatch(new ResetRequest { LedgerId = _ledgerId }, (c, r, co) => c.Reset(r, co));
        }

        public async Task ResetAsync(string accessToken = null)
        {
            await _resetClient.WithAccess(accessToken).Dispatch(new ResetRequest { LedgerId = _ledgerId }, (c, r, co) => c.ResetAsync(r, co));
        }
    }
}
