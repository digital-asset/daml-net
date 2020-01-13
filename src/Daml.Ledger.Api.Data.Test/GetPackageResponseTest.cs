// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Text;
using Com.DigitalAsset.Ledger.Api.V1;
using Google.Protobuf;
using NUnit.Framework;

namespace Com.Daml.Ledger.Api.Data.Test
{
    [TestFixture]
    public class GetPackageResponseTest
    {
        [Test]
        public void CanConvertFromProto()
        {
            DigitalAsset.Ledger.Api.V1.GetPackageResponse protoValue = new DigitalAsset.Ledger.Api.V1.GetPackageResponse() { HashFunction = HashFunction.Sha256, Hash = "hash", ArchivePayload = ByteString.CopyFromUtf8("ArchivePayload") };

            GetPackageResponse response = GetPackageResponse.FromProto(protoValue);

            Assert.AreEqual("hash", response.Hash);
            Assert.AreEqual("ArchivePayload", Encoding.UTF8.GetString(response.ArchivePayload, 0, response.ArchivePayload.Length));
            Assert.AreEqual(GetPackageResponse.HashFunction.ValueOf(0), response.Function);
        }
    }
}





