// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daml.Ledger.Client
{
    using Com.DigitalAsset.Ledger.Api.V1;

    public interface IPackageClient
    {
        string LedgerId { get; }

        GetPackageResponse GetPackage(string packageId, string accessToken = null, TraceContext traceContext = null);

        Task<GetPackageResponse> GetPackageAsync(string packageId, string accessToken = null, TraceContext traceContext = null);

        PackageStatus GetPackageStatus(string packageId, string accessToken = null, TraceContext traceContext = null);

        Task<PackageStatus> GetPackageStatusAsync(string packageId, string accessToken = null, TraceContext traceContext = null);

        IEnumerable<string> ListPackages(string accessToken = null, TraceContext traceContext = null);

        Task<IEnumerable<string>> ListPackagesAsync(string accessToken = null, TraceContext traceContext = null);
    }
}
