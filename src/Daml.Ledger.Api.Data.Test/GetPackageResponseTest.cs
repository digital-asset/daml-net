// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Text;
using Google.Protobuf;
using NUnit.Framework;

using Com.DigitalAsset.Ledger.Api.V1;

namespace Daml.Ledger.Api.Data.Test
{
    [TestFixture]
    public class GetPackageResponseTest
    {
        [Test]
        public void CanConvertFromProto()
        {
            Com.DigitalAsset.Ledger.Api.V1.GetPackageResponse protoValue = new Com.DigitalAsset.Ledger.Api.V1.GetPackageResponse() { HashFunction = HashFunction.Sha256, Hash = "hash", ArchivePayload = ByteString.CopyFromUtf8("ArchivePayload") };

            GetPackageResponse response = GetPackageResponse.FromProto(protoValue);

            Assert.AreEqual("hash", response.Hash);
            Assert.AreEqual("ArchivePayload", Encoding.UTF8.GetString(response.ArchivePayload, 0, response.ArchivePayload.Length));
            Assert.AreEqual(GetPackageResponse.HashFunction.ValueOf(0), response.Function);
        }
    }
}





