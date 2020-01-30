// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1;
    using Grpc.Core;

    public class PackageClient : IPackageClient
    {
        private readonly string _ledgerId;
        private readonly PackageService.PackageServiceClient _packageClient;

        public PackageClient(string ledgerId, Channel channel)
        {
            _ledgerId = ledgerId;
            _packageClient = new PackageService.PackageServiceClient(channel);
        }

        public GetPackageResponse GetPackage(string packageId, TraceContext traceContext = null)
        {
            var request = new GetPackageRequest { LedgerId = _ledgerId, PackageId = packageId, TraceContext = traceContext };
            return _packageClient.GetPackage(request);
        }

        public async Task<GetPackageResponse> GetPackageAsync(string packageId, TraceContext traceContext = null)
        {
            var request = new GetPackageRequest { LedgerId = _ledgerId, PackageId = packageId, TraceContext = traceContext };
            return await _packageClient.GetPackageAsync(request);
        }

        public PackageStatus GetPackageStatus(string packageId, TraceContext traceContext = null)
        {
            var request = new GetPackageStatusRequest { LedgerId = _ledgerId, PackageId = packageId, TraceContext = traceContext };
            var response = _packageClient.GetPackageStatus(request);
            return response.PackageStatus;
        }

        public async Task<PackageStatus> GetPackageStatusAsync(string packageId, TraceContext traceContext = null)
        {
            var request = new GetPackageStatusRequest { LedgerId = _ledgerId, PackageId = packageId, TraceContext = traceContext };
            var response = await _packageClient.GetPackageStatusAsync(request);
            return response.PackageStatus;
        }

        public IEnumerable<string> ListPackages(TraceContext traceContext = null)
        {
            var request = new ListPackagesRequest { LedgerId = _ledgerId, TraceContext = traceContext };
            var response = _packageClient.ListPackages(request);
            return response.PackageIds;
        }

        public async Task<IEnumerable<string>> ListPackagesAsync(TraceContext traceContext = null)
        {
            var request = new ListPackagesRequest { LedgerId = _ledgerId, TraceContext = traceContext };
            var response = await _packageClient.ListPackagesAsync(request);
            return response.PackageIds;
        }
    }
}
