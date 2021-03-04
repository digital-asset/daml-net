﻿// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;

namespace Daml.Ledger.Client
{
    using Com.Daml.Ledger.Api.V1;
    using Daml.Ledger.Client.Auth.Client;

    public class PackageClient : IPackageClient
    {
        private readonly ClientStub<PackageService.PackageServiceClient> _packageClient;

        public PackageClient(string ledgerId, Channel channel, string accessToken)
        {
            LedgerId = ledgerId;
            _packageClient = new ClientStub<PackageService.PackageServiceClient>(new PackageService.PackageServiceClient(channel), accessToken);
        }

        public string LedgerId { get; }

        public GetPackageResponse GetPackage(string packageId, string accessToken = null, TraceContext traceContext = null)
        {
            var request = new GetPackageRequest { LedgerId = LedgerId, PackageId = packageId, TraceContext = traceContext };
            return _packageClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.GetPackage(r, co));
        }

        public async Task<GetPackageResponse> GetPackageAsync(string packageId, string accessToken = null, TraceContext traceContext = null)
        {
            var request = new GetPackageRequest { LedgerId = LedgerId, PackageId = packageId, TraceContext = traceContext };
            return await _packageClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.GetPackageAsync(r, co));
        }

        public PackageStatus GetPackageStatus(string packageId, string accessToken = null, TraceContext traceContext = null)
        {
            var request = new GetPackageStatusRequest { LedgerId = LedgerId, PackageId = packageId, TraceContext = traceContext };
            var response = _packageClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.GetPackageStatus(r, co));

            return response.PackageStatus;
        }

        public async Task<PackageStatus> GetPackageStatusAsync(string packageId, string accessToken = null, TraceContext traceContext = null)
        {
            var request = new GetPackageStatusRequest { LedgerId = LedgerId, PackageId = packageId, TraceContext = traceContext };
            var response = await _packageClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.GetPackageStatusAsync(r, co));
            return response.PackageStatus;
        }

        public IEnumerable<string> ListPackages(string accessToken = null, TraceContext traceContext = null)
        {
            var request = new ListPackagesRequest { LedgerId = LedgerId, TraceContext = traceContext };
            var response = _packageClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.ListPackages(r, co));
            return response.PackageIds;
        }

        public async Task<IEnumerable<string>> ListPackagesAsync(string accessToken = null, TraceContext traceContext = null)
        {
            var request = new ListPackagesRequest { LedgerId = LedgerId, TraceContext = traceContext };
            var response = await _packageClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.ListPackagesAsync(r, co));
            return response.PackageIds;
        }
    }
}
