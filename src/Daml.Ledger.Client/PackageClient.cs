// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1;
    using Daml.Ledger.Client.Auth.Client;
    using Grpc.Core;

    public class PackageClient : IPackageClient
    {
        private readonly string _ledgerId;
        private readonly ClientStub<PackageService.PackageServiceClient> _packageClient;

        public PackageClient(string ledgerId, Channel channel)
        {
            _ledgerId = ledgerId;
            _packageClient = new ClientStub<PackageService.PackageServiceClient>(new PackageService.PackageServiceClient(channel));
        }

        public GetPackageResponse GetPackage(string packageId, TraceContext traceContext = null)
        {
            var request = new GetPackageRequest { LedgerId = _ledgerId, PackageId = packageId, TraceContext = traceContext };
            return _packageClient.Dispatch(request, (c, r, co) => c.GetPackage(r, co));
        }

        public async Task<GetPackageResponse> GetPackageAsync(string packageId, TraceContext traceContext = null)
        {
            var request = new GetPackageRequest { LedgerId = _ledgerId, PackageId = packageId, TraceContext = traceContext };
            return await _packageClient.Dispatch(request, (c, r, co) => c.GetPackageAsync(r, co));
        }

        public PackageStatus GetPackageStatus(string packageId, TraceContext traceContext = null)
        {
            var request = new GetPackageStatusRequest { LedgerId = _ledgerId, PackageId = packageId, TraceContext = traceContext };
            var response = _packageClient.Dispatch(request, (c, r, co) => c.GetPackageStatus(r, co));
            return response.PackageStatus;
        }

        public async Task<PackageStatus> GetPackageStatusAsync(string packageId, TraceContext traceContext = null)
        {
            var request = new GetPackageStatusRequest { LedgerId = _ledgerId, PackageId = packageId, TraceContext = traceContext };
            var response = await _packageClient.Dispatch(request, (c, r, co) => c.GetPackageStatusAsync(r, co));
            return response.PackageStatus;
        }

        public IEnumerable<string> ListPackages(TraceContext traceContext = null)
        {
            var request = new ListPackagesRequest { LedgerId = _ledgerId, TraceContext = traceContext };
            var response = _packageClient.Dispatch(request, (c, r, co) => c.ListPackages(r, co));
            return response.PackageIds;
        }

        public async Task<IEnumerable<string>> ListPackagesAsync(TraceContext traceContext = null)
        {
            var request = new ListPackagesRequest { LedgerId = _ledgerId, TraceContext = traceContext };
            var response = await _packageClient.Dispatch(request, (c, r, co) => c.ListPackagesAsync(r, co));
            return response.PackageIds;
        }
    }
}
