// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Text;
using Google.Protobuf;
using Xunit;
using FluentAssertions;

using Com.Daml.Ledger.Api.V1;

namespace Daml.Ledger.Api.Data.Test
{
    public class GetPackageResponseTest
    {
        [Fact]
        public void CanConvertFromProto()
        {
            Com.Daml.Ledger.Api.V1.GetPackageResponse protoValue = new Com.Daml.Ledger.Api.V1.GetPackageResponse() { HashFunction = HashFunction.Sha256, Hash = "hash", ArchivePayload = ByteString.CopyFromUtf8("ArchivePayload") };

            GetPackageResponse response = GetPackageResponse.FromProto(protoValue);

            response.Hash.Should().Be("hash");
            Encoding.UTF8.GetString(response.ArchivePayload, 0, response.ArchivePayload.Length).Should().Be("ArchivePayload");
            response.Function.Should().Be(GetPackageResponse.HashFunction.ValueOf(0));
        }
    }
}





