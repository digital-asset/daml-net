// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Admin
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1.Admin;
    using Google.Protobuf;
    using Grpc.Core;

    public class PackageManagementClient : IPackageManagementClient
    {
        private readonly PackageManagementService.PackageManagementServiceClient packageManagementClient;

        public PackageManagementClient(Channel channel)
        {
            this.packageManagementClient = new PackageManagementService.PackageManagementServiceClient(channel);
        }

        public IEnumerable<PackageDetails> ListKnownPackages()
        {
            var request = new ListKnownPackagesRequest();
            var response = this.packageManagementClient.ListKnownPackages(request);
            return response.PackageDetails;
        }

        public async Task<IEnumerable<PackageDetails>> ListKnownPackagesAsync()
        {
            var request = new ListKnownPackagesRequest();
            var response = await this.packageManagementClient.ListKnownPackagesAsync (request);
            return response.PackageDetails;
        }

        public void UploadDarFile(Stream stream)
        {
            var byteString = ByteString.FromStream(stream);
            var request = new UploadDarFileRequest { DarFile = byteString };
            this.packageManagementClient.UploadDarFile(request);
        }

        public async Task UploadDarFileAsync(Stream stream)
        {
            var byteString = await ByteString.FromStreamAsync(stream);
            var request = new UploadDarFileRequest { DarFile = byteString };
            await this.packageManagementClient.UploadDarFileAsync(request);
        }
    }
}
