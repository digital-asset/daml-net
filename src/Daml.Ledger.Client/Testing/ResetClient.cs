// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1.Testing;
    using Grpc.Core;

    public class ResetClient : IResetClient
    {
        private readonly ResetService.ResetServiceClient resetClient;

        public ResetClient(Channel channel)
        {
            this.resetClient = new ResetService.ResetServiceClient(channel);
        }

        public void Reset(string ledgerId)
        {
            var request = new ResetRequest { LedgerId = ledgerId };
            this.resetClient.Reset(request);
        }

        public async Task ResetAsync(string ledgerId)
        {
            var request = new ResetRequest { LedgerId = ledgerId };
            await this.resetClient.ResetAsync(request);
        }
    }
}
