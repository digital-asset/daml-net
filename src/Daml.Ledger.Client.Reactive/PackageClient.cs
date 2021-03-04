// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Reactive.Linq;

namespace Daml.Ledger.Client.Reactive
{
    using Daml.Ledger.Api.Data.Util;
    
    using GetPackageResponse = Com.DigitalAsset.Ledger.Api.V1.GetPackageResponse;
    using TraceContext = Com.DigitalAsset.Ledger.Api.V1.TraceContext;
    using PackageStatus = Com.DigitalAsset.Ledger.Api.V1.PackageStatus;

    public class PackageClient
    {
        private readonly IPackageClient _packageClient;

        public PackageClient(IPackageClient packageClient)
        {
            _packageClient = packageClient;
        }

        public IObservable<string> ListPackages(Optional<string> accessToken = null, TraceContext traceContext = null)
        {
            return _packageClient.ListPackages(accessToken?.Reduce((string) null), traceContext).ToObservable();
        }

        public GetPackageResponse GetPackage(string packageId, Optional<string> accessToken = null, TraceContext traceContext = null)
        {
            return _packageClient.GetPackage(packageId, accessToken?.Reduce((string) null), traceContext);
        }

        public PackageStatus GetPackageStatus(string packageId, Optional<string> accessToken = null, TraceContext traceContext = null)
        {
            return _packageClient.GetPackageStatus(packageId, accessToken?.Reduce((string) null), traceContext);
        }
    }
}
