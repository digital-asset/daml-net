// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using Com.Daml.Ledger.Api.Util;

namespace Com.Daml.Ledger.Api.Data 
{
    public abstract class Filter : IComparable<Filter>, IEquatable<Filter>
    {
        public static Filter FromProto(DigitalAsset.Ledger.Api.V1.Filters filters)
        {
            if (filters.Inclusive != null)
                return InclusiveFilter.FromProto(filters.Inclusive);

            return NoFilter.Instance;
        }

        public abstract DigitalAsset.Ledger.Api.V1.Filters ToProto();

        public int CompareTo(Filter rhs) => GetHashCode().CompareTo(rhs.GetHashCode());

        public static bool operator ==(Filter lhs, Filter rhs) => lhs.Compare(rhs);
        public static bool operator !=(Filter lhs, Filter rhs) => !(lhs == rhs);

        public override bool Equals(object obj) => Equals((Filter)obj);
        public abstract bool Equals(Filter other);

        public abstract override int GetHashCode();
    }
}
