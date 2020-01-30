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
        private readonly PackageManagementService.PackageManagementServiceClient _packageManagementClient;

        public PackageManagementClient(Channel channel)
        {
            _packageManagementClient = new PackageManagementService.PackageManagementServiceClient(channel);
        }

        public IEnumerable<PackageDetails> ListKnownPackages()
        {
            var response = _packageManagementClient.ListKnownPackages(new ListKnownPackagesRequest());
            return response.PackageDetails;
        }

        public async Task<IEnumerable<PackageDetails>> ListKnownPackagesAsync()
        {
            var response = await _packageManagementClient.ListKnownPackagesAsync(new ListKnownPackagesRequest());
            return response.PackageDetails;
        }

        public void UploadDarFile(Stream stream, string submissionId = null)
        {
            var request = new UploadDarFileRequest { DarFile = ByteString.FromStream(stream) };
            if (!string.IsNullOrEmpty(submissionId))
                request.SubmissionId = submissionId;
            
            _packageManagementClient.UploadDarFile(request);
        }

        public async Task UploadDarFileAsync(Stream stream, string submissionId = null)
        {
            var byteString = await ByteString.FromStreamAsync(stream);
            var request = new UploadDarFileRequest { DarFile = byteString };
            if (!string.IsNullOrEmpty(submissionId))
                request.SubmissionId = submissionId;
                
            await _packageManagementClient.UploadDarFileAsync(request);
        }
    }
}
