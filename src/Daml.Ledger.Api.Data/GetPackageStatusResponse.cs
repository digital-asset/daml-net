// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;

namespace Daml.Ledger.Api.Data
{
    public class GetPackageStatusResponse
    {
        public class PackageStatus
        {
            private readonly int _value;

            private static readonly Dictionary<int, PackageStatus> _valueToEnumMap = new Dictionary<int,PackageStatus>();

            private static readonly PackageStatus _unrecognized = new PackageStatus(-1);

            static PackageStatus()
            {
                foreach (var s in Enum.GetValues(typeof(Com.DigitalAsset.Ledger.Api.V1.PackageStatus)))
                    _valueToEnumMap.Add((int) s, new PackageStatus((int) s));
            }

            private PackageStatus(int value)
            {
                _value = value;
            }

            public static PackageStatus ValueOf(int value) => _valueToEnumMap.TryGetValue(value, out PackageStatus ps) ? ps : _unrecognized;
        }

        public GetPackageStatusResponse(PackageStatus packageStatus)
        {
            PackageStatusValue = packageStatus;
        }
        
        public PackageStatus PackageStatusValue { get; }

        public static GetPackageStatusResponse FromProto(Com.DigitalAsset.Ledger.Api.V1.PackageStatus packageStatus) => new GetPackageStatusResponse(PackageStatus.ValueOf((int) packageStatus));
    }
} 
