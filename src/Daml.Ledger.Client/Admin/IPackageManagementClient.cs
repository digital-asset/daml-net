// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Daml.Ledger.Client.Admin
{
    using Com.DigitalAsset.Ledger.Api.V1.Admin;

    public interface IPackageManagementClient
    {
        IEnumerable<PackageDetails> ListKnownPackages(string accessToken = null);

        Task<IEnumerable<PackageDetails>> ListKnownPackagesAsync(string accessToken = null);

        void UploadDarFile(Stream stream, string submissionId = null, string accessToken = null);

        Task UploadDarFileAsync(Stream stream, string submissionId = null, string accessToken = null);
    }
}
