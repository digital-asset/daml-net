// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1;

    public interface IPackageClient
    {
        GetPackageResponse GetPackage(string packageId, TraceContext traceContext = null);

        Task<GetPackageResponse> GetPackageAsync(string packageId, TraceContext traceContext = null);

        PackageStatus GetPackageStatus(string packageId, TraceContext traceContext = null);

        Task<PackageStatus> GetPackageStatusAsync(string packageId, TraceContext traceContext = null);

        IEnumerable<string> ListPackages(TraceContext traceContext = null);

        Task<IEnumerable<string>> ListPackagesAsync(TraceContext traceContext = null);
    }
}
