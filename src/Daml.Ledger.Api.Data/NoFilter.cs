// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Api.Data 
{
    using Util;

    public class NoFilter : Filter
    {
        public static readonly NoFilter Instance = new NoFilter();

        private NoFilter() { }

        public override Com.Daml.Ledger.Api.V1.Filters ToProto() => new Com.Daml.Ledger.Api.V1.Filters();

        public override bool Equals(object obj) => Equals((Filter)obj);
        public override bool Equals(Filter obj) => this.Compare(obj, rhs => true);

        public override int GetHashCode() => Instance.GetHashCode();
    }
} 
