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

        public PackageManagementClient(Channel channel, string accessToken)
        {
            _packageManagementClient = new ClientStub<PackageManagementService.PackageManagementServiceClient>(new PackageManagementService.PackageManagementServiceClient(channel), accessToken);
        }

        public IEnumerable<PackageDetails> ListKnownPackages(string accessToken = null)
        {
            var response = _packageManagementClient.WithAccess(accessToken).DispatchRequest(new ListKnownPackagesRequest(), (c, r, co) => c.ListKnownPackages(r, co), (c, r) => c.ListKnownPackages(r));
            return response.PackageDetails;
        }

        public async Task<IEnumerable<PackageDetails>> ListKnownPackagesAsync(string accessToken = null)
        {
            var response = await _packageManagementClient.WithAccess(accessToken).DispatchRequest(new ListKnownPackagesRequest(), (c, r, co) => c.ListKnownPackagesAsync(r, co), (c, r) => c.ListKnownPackagesAsync(r));

            return response.PackageDetails;
        }

        public void UploadDarFile(Stream stream, string submissionId = null, string accessToken = null)
        {
            var request = new UploadDarFileRequest { DarFile = ByteString.FromStream(stream) };
            if (!string.IsNullOrEmpty(submissionId))
                request.SubmissionId = submissionId;
            
            _packageManagementClient.WithAccess(accessToken).DispatchRequest(request, (c, r, co) => c.UploadDarFile(r, co), (c, r) => c.UploadDarFile(r));
        }

        public async Task UploadDarFileAsync(Stream stream, string submissionId = null, string accessToken = null)
        {
            var byteString = await ByteString.FromStreamAsync(stream);
            var request = new UploadDarFileRequest { DarFile = byteString };
            if (!string.IsNullOrEmpty(submissionId))
                request.SubmissionId = submissionId;

            await _packageManagementClient.WithAccess(accessToken).DispatchRequest(request, (c, r, co) => c.UploadDarFileAsync(r, co), (c, r) => c.UploadDarFileAsync(r));
        }
    }
}
