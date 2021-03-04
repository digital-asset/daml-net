// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Api.Data.Test
{
    public class GetPackageStatusResponseTest
    {
        [Fact]
        public void CanConvertFromProto()
        {
            Com.Daml.Ledger.Api.V1.GetPackageStatusResponse protoValue = new Com.Daml.Ledger.Api.V1.GetPackageStatusResponse() { PackageStatus = Com.Daml.Ledger.Api.V1.PackageStatus.Registered };

            GetPackageStatusResponse response = GetPackageStatusResponse.FromProto(protoValue.PackageStatus);
            response.PackageStatusValue.Should().Be(GetPackageStatusResponse.PackageStatus.ValueOf(1));
        }
    }
}





