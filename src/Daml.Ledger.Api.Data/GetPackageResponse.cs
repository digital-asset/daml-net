// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf;

namespace Com.Daml.Ledger.Api.Data
{
    public class GetPackageResponse
    {
        public class HashFunction
        {
            private readonly int _value;

            private static readonly Dictionary<int, HashFunction> _valueToEnumMap = new Dictionary<int, HashFunction>();

            private static readonly HashFunction _unrecognized = new HashFunction(-1);

            static HashFunction()
            {
                foreach (var e in Enum.GetValues(typeof(DigitalAsset.Ledger.Api.V1.HashFunction)))
                    _valueToEnumMap.Add((int)e, new HashFunction((int)e));
            }

            private HashFunction(int value)
            {
                _value = value;
            }

            public static HashFunction ValueOf(int value) => _valueToEnumMap.TryGetValue(value, out HashFunction func) ? func : _unrecognized;
        }

        private readonly ByteString _archivePayload;

        public GetPackageResponse(HashFunction hashFunction, string hash, ByteString archivePayload)
        {
            Function = hashFunction;
            Hash = hash;
            _archivePayload = archivePayload;
        }

        public HashFunction Function { get; }
        public string Hash { get; }

        public byte[] ArchivePayload => _archivePayload.ToByteArray();

        public static GetPackageResponse FromProto(DigitalAsset.Ledger.Api.V1.GetPackageResponse p) => new GetPackageResponse(HashFunction.ValueOf((int) p.HashFunction), p.Hash, p.ArchivePayload);
    }
}
