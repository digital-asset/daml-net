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
        private readonly PackageService.PackageServiceClient packageClient;

        public PackageClient(Channel channel)
        {
            this.packageClient = new PackageService.PackageServiceClient(channel);
        }

        public GetPackageResponse GetPackage(string ledgerId, string packageId, TraceContext traceContext = null)
        {
            var request = new GetPackageRequest { LedgerId = ledgerId, PackageId = packageId, TraceContext = traceContext };
            var response = this.packageClient.GetPackage(request);
            return response;
        }

        public async Task<GetPackageResponse> GetPackageAsync(string ledgerId, string packageId, TraceContext traceContext = null)
        {
            var request = new GetPackageRequest { LedgerId = ledgerId, PackageId = packageId, TraceContext = traceContext };
            var response = await this.packageClient.GetPackageAsync(request);
            return response;
        }

        public PackageStatus GetPackageStatus(string ledgerId, string packageId, TraceContext traceContext = null)
        {
            var request = new GetPackageStatusRequest { LedgerId = ledgerId, PackageId = packageId, TraceContext = traceContext };
            var response = this.packageClient.GetPackageStatus(request);
            return response.PackageStatus;
        }

        public async Task<PackageStatus> GetPackageStatusAsync(string ledgerId, string packageId, TraceContext traceContext = null)
        {
            var request = new GetPackageStatusRequest { LedgerId = ledgerId, PackageId = packageId, TraceContext = traceContext };
            var response = await this.packageClient.GetPackageStatusAsync(request);
            return response.PackageStatus;
        }

        public IEnumerable<string> ListPackages(string ledgerId, TraceContext traceContext = null)
        {
            var request = new ListPackagesRequest { LedgerId = ledgerId, TraceContext = traceContext };
            var response = this.packageClient.ListPackages(request);
            return response.PackageIds;
        }

        public async Task<IEnumerable<string>> ListPackagesAsync(string ledgerId, TraceContext traceContext = null)
        {
            var request = new ListPackagesRequest { LedgerId = ledgerId, TraceContext = traceContext };
            var response = await this.packageClient.ListPackagesAsync(request);
            return response.PackageIds;
        }
    }
}
