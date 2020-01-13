// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Grpc.Core;

namespace Daml.Ledger.Client.Reactive.Admin
{
    using System;
    using System.IO;
    using System.Reactive.Linq;
    using Com.DigitalAsset.Ledger.Api.V1.Admin;
    using Com.Daml.Ledger.Api.Util;

    public class PackageManagementClient
    {
        private readonly Client.Admin.IPackageManagementClient _packageManagementClient;

        public PackageManagementClient(Channel channel, Optional<string> accessToken)
        {
            _packageManagementClient = new Client.Admin.PackageManagementClient(channel, accessToken.Reduce((string) null));
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
