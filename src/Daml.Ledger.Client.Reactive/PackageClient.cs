// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Grpc.Core;

namespace Daml.Ledger.Client.Reactive
{
    using System;
    using System.Reactive.Linq;
    using Com.Daml.Ledger.Api.Util;

    using Single = Com.Daml.Ledger.Api.Util.Single;
    using GetPackageResponse = Com.Daml.Ledger.Api.Data.GetPackageResponse;
    using TraceContext = Com.DigitalAsset.Ledger.Api.V1.TraceContext;
    using PackageStatus = Com.DigitalAsset.Ledger.Api.V1.PackageStatus;

    public class PackageClient
    {
        private readonly IPackageClient _packageClient;

        public PackageClient(string ledgerId, Channel channel, Optional<string> accessToken)
        {
            _packageClient = new Client.PackageClient(ledgerId, channel, accessToken.Reduce((string) null));
        }

        public IObservable<string> ListPackages(Optional<string> accessToken = null, TraceContext traceContext = null)
        {
            return _packageClient.ListPackages(accessToken?.Reduce((string) null), traceContext).ToObservable();
        }

        public Single<GetPackageResponse> GetPackage(string packageId, Optional<string> accessToken = null, TraceContext traceContext = null)
        {
            return Single.Just(GetPackageResponse.FromProto(_packageClient.GetPackage(packageId, accessToken?.Reduce((string) null), traceContext)));
        }

        public Single<PackageStatus> GetPackageStatus(string packageId, Optional<string> accessToken = null, TraceContext traceContext = null)
        {
            return Single.Just(_packageClient.GetPackageStatus(packageId, accessToken?.Reduce((string) null), traceContext));
        }
    }
}
