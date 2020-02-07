// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Api.Data 
{
    using Util;

    public class CreateCommand : Command
    {
        public CreateCommand(Identifier templateId, Record createArguments)
        {
            TemplateId = templateId;
            CreateArguments = createArguments;
        }

        public override string ToString() => $"CreateCommand{{templateId={TemplateId}, createArguments={CreateArguments}";

        public override bool Equals(Command obj)  => this.Compare(obj, rhs => TemplateId == rhs.TemplateId && CreateArguments == rhs.CreateArguments);

        public override bool Equals(object obj) => Equals((Command)obj);

        public override int GetHashCode() => (TemplateId, CreateArguments).GetHashCode();

        public static bool operator ==(CreateCommand lhs, CreateCommand rhs) => lhs.Compare(rhs);
        public static bool operator !=(CreateCommand lhs, CreateCommand rhs) => !(lhs == rhs);

        public override Identifier TemplateId {  get; }

        public Record CreateArguments { get; }

        public static CreateCommand FromProto(Com.DigitalAsset.Ledger.Api.V1.CreateCommand create) => new CreateCommand(Identifier.FromProto(create.TemplateId), Record.FromProto(create.CreateArguments));

        public Com.DigitalAsset.Ledger.Api.V1.CreateCommand ToProto() => new Com.DigitalAsset.Ledger.Api.V1.CreateCommand { TemplateId = TemplateId.ToProto(), CreateArguments = CreateArguments.ToProtoRecord() };
    }
} 
