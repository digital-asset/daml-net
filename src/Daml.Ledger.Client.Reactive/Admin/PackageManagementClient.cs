// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Reactive.Linq;

namespace Daml.Ledger.Client.Reactive.Admin
{
    using Daml.Ledger.Client.Admin;
    using Daml.Ledger.Api.Data.Util;

    using PackageDetails = Com.DigitalAsset.Ledger.Api.V1.Admin.PackageDetails;

    public class PackageManagementClient
    {
        private readonly IPackageManagementClient _packageManagementClient;

        public PackageManagementClient(IPackageManagementClient packageManagementClient)
        {
            _packageManagementClient = packageManagementClient;
        }

        public IObservable<PackageDetails> ListKnownPackages(Optional<string> accessToken = null)
        {
            return _packageManagementClient.ListKnownPackages(accessToken?.Reduce((string) null)).ToObservable();
        }

        void UploadDarFile(Stream stream, Optional<string> accessToken = null)
        {
            _packageManagementClient.UploadDarFile(stream, accessToken?.Reduce((string) null));
        }
    }
}
