// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Com.Daml.Ledger.Api.Data.Test
{
    [TestFixture]
    public class GetPackageStatusResponseTest
    {
        [Test]
        public void CanConvertFromProto()
        {
            DigitalAsset.Ledger.Api.V1.GetPackageStatusResponse protoValue = new DigitalAsset.Ledger.Api.V1.GetPackageStatusResponse() { PackageStatus = Com.DigitalAsset.Ledger.Api.V1.PackageStatus.Registered };

            GetPackageStatusResponse response = GetPackageStatusResponse.FromProto(protoValue);

            Assert.AreEqual(GetPackageStatusResponse.PackageStatus.ValueOf(1), response.PackageStatusValue);
        }
    }
}





