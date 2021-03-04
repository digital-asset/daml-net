// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;

namespace Daml.Ledger.Client.Admin
{
    using Com.Daml.Ledger.Api.V1.Admin;
    using Daml.Ledger.Client.Auth.Client;

    public class PackageManagementClient : IPackageManagementClient
    {
        private readonly ClientStub<PackageManagementService.PackageManagementServiceClient> _packageManagementClient;

        public PackageManagementClient(Channel channel, string accessToken)
        {
            _packageManagementClient = new ClientStub<PackageManagementService.PackageManagementServiceClient>(new PackageManagementService.PackageManagementServiceClient(channel), accessToken);
        }

        public IEnumerable<PackageDetails> ListKnownPackages(string accessToken = null)
        {
            var response = _packageManagementClient.WithAccess(accessToken).Dispatch(new ListKnownPackagesRequest(), (c, r, co) => c.ListKnownPackages(r, co));
            return response.PackageDetails;
        }

        public async Task<IEnumerable<PackageDetails>> ListKnownPackagesAsync(string accessToken = null)
        {
            var response = await _packageManagementClient.WithAccess(accessToken).Dispatch(new ListKnownPackagesRequest(), (c, r, co) => c.ListKnownPackagesAsync(r, co));

            return response.PackageDetails;
        }

        public void UploadDarFile(Stream stream, string submissionId = null, string accessToken = null)
        {
            var request = new UploadDarFileRequest { DarFile = ByteString.FromStream(stream) };
            if (!string.IsNullOrEmpty(submissionId))
                request.SubmissionId = submissionId;
            
            _packageManagementClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.UploadDarFile(r, co));
        }

        public async Task UploadDarFileAsync(Stream stream, string submissionId = null, string accessToken = null)
        {
            var byteString = await ByteString.FromStreamAsync(stream);
            var request = new UploadDarFileRequest { DarFile = byteString };
            if (!string.IsNullOrEmpty(submissionId))
                request.SubmissionId = submissionId;

            await _packageManagementClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.UploadDarFileAsync(r, co));
        }
    }
}
