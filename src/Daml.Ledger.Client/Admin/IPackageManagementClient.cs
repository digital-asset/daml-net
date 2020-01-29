// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Admin
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1.Admin;

    public interface IPackageManagementClient
    {
        IEnumerable<PackageDetails> ListKnownPackages();

        Task<IEnumerable<PackageDetails>> ListKnownPackagesAsync();

        void UploadDarFile(Stream stream);

        Task UploadDarFileAsync(Stream stream);
    }
}
