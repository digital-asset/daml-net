// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Com.Daml.Ledger.Api.Util;

using Identifier = Com.Daml.Ledger.Api.Data.Identifier;
using Record = Com.Daml.Ledger.Api.Data.Record;

namespace Daml.Ledger.Automation.Components.Helpers
{
    public class CreatedContract
    {
        private readonly int _hashCode;

        public CreatedContract(Identifier templateId, Record createArguments, ICreatedContractContext context)
        {
            TemplateId = templateId;
            CreateArguments = createArguments;
            Context = context;

            _hashCode = new HashCodeHelper().Add(TemplateId).Add(CreateArguments).Add(Context).ToHashCode();
        }

        public Identifier TemplateId { get; }
        public Record CreateArguments { get; }
        public ICreatedContractContext Context { get; }

        public override string ToString() => $"CreatedContract{{templateId={TemplateId}, createArguments={CreateArguments}, context={Context}}}";

        public override bool Equals(object obj) => this.Compare(obj, rhs => TemplateId == rhs.TemplateId && CreateArguments == rhs.CreateArguments && Context == rhs.Context);

        public static bool operator ==(CreatedContract lhs, CreatedContract rhs) => lhs.Compare(rhs);
        public static bool operator !=(CreatedContract lhs, CreatedContract rhs) => !(lhs == rhs);

        public override int GetHashCode() => _hashCode;
    }
}
