// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0
using Grpc.Core;
using System.Threading.Tasks;

namespace Daml.Ledger.Client
{
    using Com.Daml.Ledger.Api.V1;
    using Daml.Ledger.Client.Auth.Client;

    public class VersionClient : IVersionClient
    {
        private readonly ClientStub<VersionService.VersionServiceClient> _versionClient;

        public VersionClient(string ledgerId, Channel channel, string accessToken)
        {
            LedgerId = ledgerId;
            _versionClient = new ClientStub<VersionService.VersionServiceClient>(new VersionService.VersionServiceClient(channel), accessToken);
        }

        public string LedgerId { get; }

        public string GetLedgerApiVersion(string accessToken = null)
        {
            var request = new GetLedgerApiVersionRequest { LedgerId = LedgerId };
            return _versionClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.GetLedgerApiVersion(r, co)).Version;
        }

        public async Task<string> GetLedgerApiVersionAsync(string accessToken = null)
        {
            var request = new GetLedgerApiVersionRequest { LedgerId = LedgerId };
            return (await _versionClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.GetLedgerApiVersionAsync(r, co))).Version;
        }
    }
}
