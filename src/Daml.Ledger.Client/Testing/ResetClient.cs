// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Testing
{
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1.Testing;
    using Daml.Ledger.Client.Auth.Client;
    using Grpc.Core;

    public class ResetClient : IResetClient
    {
        private readonly string _ledgerId;
        private readonly ClientStub<ResetService.ResetServiceClient> _resetClient;

        public ResetClient(string ledgerId, Channel channel)
        {
            _ledgerId = ledgerId;
            _resetClient = new ClientStub<ResetService.ResetServiceClient>(new ResetService.ResetServiceClient(channel));
        }

        public void Reset()
        {
            _resetClient.Dispatch(new ResetRequest { LedgerId = _ledgerId }, (c, r, co) => c.Reset(r, co));
        }

        public async Task ResetAsync()
        {
            await _resetClient.Dispatch(new ResetRequest { LedgerId = _ledgerId }, (c, r, co) => c.ResetAsync(r, co));
        }
    }
}
