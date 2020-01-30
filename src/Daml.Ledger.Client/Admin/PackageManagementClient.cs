// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Admin
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1.Admin;
    using Daml.Ledger.Client.Auth.Client;
    using Google.Protobuf;
    using Grpc.Core;

    public class PackageManagementClient : IPackageManagementClient
    {
        private readonly ClientStub<PackageManagementService.PackageManagementServiceClient> _packageManagementClient;

        public PackageManagementClient(Channel channel)
        {
            _packageManagementClient = new ClientStub<PackageManagementService.PackageManagementServiceClient>(new PackageManagementService.PackageManagementServiceClient(channel));
        }

        public IEnumerable<PackageDetails> ListKnownPackages()
        {
            var response = _packageManagementClient.Dispatch(new ListKnownPackagesRequest(), (c, r, co) => c.ListKnownPackages(r, co));
            return response.PackageDetails;
        }

        public async Task<IEnumerable<PackageDetails>> ListKnownPackagesAsync()
        {
            var response = await _packageManagementClient.Dispatch(new ListKnownPackagesRequest(), (c, r, co) => c.ListKnownPackagesAsync(r, co));
            return response.PackageDetails;
        }

        public void UploadDarFile(Stream stream, string submissionId = null)
        {
            var request = new UploadDarFileRequest { DarFile = ByteString.FromStream(stream) };
            if (!string.IsNullOrEmpty(submissionId))
                request.SubmissionId = submissionId;
            
            _packageManagementClient.Dispatch(request, (c, r, co) => c.UploadDarFile(r, co));
        }

        public async Task UploadDarFileAsync(Stream stream, string submissionId = null)
        {
            var byteString = await ByteString.FromStreamAsync(stream);
            var request = new UploadDarFileRequest { DarFile = byteString };
            if (!string.IsNullOrEmpty(submissionId))
                request.SubmissionId = submissionId;
                
            await _packageManagementClient.Dispatch(request, (c, r, co) => c.UploadDarFileAsync(r, co));
        }
    }
}
