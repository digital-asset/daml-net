// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    [TestFixture]
    public class GetPackageStatusResponseTest
    {
        [Test]
        public void CanConvertFromProto()
        {
            Com.DigitalAsset.Ledger.Api.V1.GetPackageStatusResponse protoValue = new Com.DigitalAsset.Ledger.Api.V1.GetPackageStatusResponse() { PackageStatus = Com.DigitalAsset.Ledger.Api.V1.PackageStatus.Registered };

            GetPackageStatusResponse response = GetPackageStatusResponse.FromProto(protoValue.PackageStatus);

            Assert.AreEqual(GetPackageStatusResponse.PackageStatus.ValueOf(1), response.PackageStatusValue);
        }
    }
}





