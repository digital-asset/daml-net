// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Daml.Ledger.Api.Data 
{
    using Util;

    public class InclusiveFilter : Filter
    {
        private readonly int _hashCode;

        public InclusiveFilter(IEnumerable<Identifier> templateIds)
        {
            TemplateIds = ImmutableHashSet.CreateRange(templateIds);

            _hashCode = new HashCodeHelper().AddRange(TemplateIds).ToHashCode();
        }

        public IImmutableSet<Identifier> TemplateIds { get; }

        public override Com.DigitalAsset.Ledger.Api.V1.Filters ToProto()
        {
            var inclusiveFilter = new Com.DigitalAsset.Ledger.Api.V1.InclusiveFilters();
            inclusiveFilter.TemplateIds.AddRange(from id in TemplateIds select id.ToProto());

            return new Com.DigitalAsset.Ledger.Api.V1.Filters { Inclusive = inclusiveFilter };
        }

        public static InclusiveFilter FromProto(Com.DigitalAsset.Ledger.Api.V1.InclusiveFilters inclusiveFilters) => new InclusiveFilter(from templateId in inclusiveFilters.TemplateIds select Identifier.FromProto(templateId));

        public override string ToString() => $"InclusiveFilter{{templateIds={TemplateIds}}}";

        public override bool Equals(object obj) => Equals((Filter) obj);
        public override bool Equals(Filter obj) => this.Compare(obj, rhs => _hashCode == rhs._hashCode && !TemplateIds.Except(rhs.TemplateIds).Any());

        public override int GetHashCode() => _hashCode;

        public static bool operator ==(InclusiveFilter lhs, InclusiveFilter rhs) => lhs.Compare(rhs);
        public static bool operator !=(InclusiveFilter lhs, InclusiveFilter rhs) => !(lhs == rhs);
    }
} 
