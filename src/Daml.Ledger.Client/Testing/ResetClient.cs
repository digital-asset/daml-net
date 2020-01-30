// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Testing
{
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1.Testing;
    using Grpc.Core;

    public class ResetClient : IResetClient
    {
        private readonly string _ledgerId;
        private readonly ResetService.ResetServiceClient _resetClient;

        public ResetClient(string ledgerId, Channel channel)
        {
            _ledgerId = ledgerId;
            _resetClient = new ResetService.ResetServiceClient(channel);
        }

        public void Reset()
        {
            _resetClient.Reset(new ResetRequest { LedgerId = _ledgerId });
        }

        public async Task ResetAsync()
        {
            await _resetClient.ResetAsync(new ResetRequest { LedgerId = _ledgerId });
        }
    }
}
