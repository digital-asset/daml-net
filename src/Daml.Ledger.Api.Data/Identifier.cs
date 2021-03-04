// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Daml.Ledger.Api.Data 
{
    using Util;

    public sealed class Identifier : IComparable<Identifier>
    {
        // This constructor is deprecated in favor of {@link Identifier#Identifier(string, string, string)}
        public Identifier(string packageId, string name)
        {
            PackageId = packageId;
            int lastDot = name.LastIndexOf('.');
            if (lastDot <= 0)
            {
                // The module component of the name must be at least 1 character long.
                // if no '.' is found or it is on the first position, then the name is not a valid identifier.
                throw new ArgumentException($"Identifier name {name} has wrong format. Dot-separated module and entity name expected (e.g.: Foo.Bar)");
            }
            ModuleName = name.Substring(0, lastDot);
            EntityName = name.Substring(lastDot + 1);
        }

        public Identifier(string packageId, string moduleName, string entityName)
        {
            PackageId = packageId;
            ModuleName = moduleName;
            EntityName = entityName;
        }

#pragma warning disable CS0612
        public static Identifier FromProto(Com.Daml.Ledger.Api.V1.Identifier identifier)
        {
            if (!string.IsNullOrEmpty(identifier.ModuleName) && !string.IsNullOrEmpty(identifier.EntityName))
                return new Identifier(identifier.PackageId, identifier.ModuleName, identifier.EntityName);

            throw new ArgumentException($"Invalid identifier {identifier}: both module_name and entity_name must be set.");
        }
#pragma warning restore CS0612

        public Com.Daml.Ledger.Api.V1.Identifier ToProto() => new Com.Daml.Ledger.Api.V1.Identifier { PackageId = PackageId, ModuleName = ModuleName, EntityName = EntityName };

        public string PackageId { get; }

        public string ModuleName {  get; }

        public string EntityName {  get; }

        public override bool Equals(object obj) => this.Compare(obj, rhs => PackageId == rhs.PackageId && ModuleName == rhs.ModuleName && EntityName == rhs.EntityName);

        public override int GetHashCode() => (PackageId, ModuleName, EntityName).GetHashCode();

        public static bool operator ==(Identifier lhs, Identifier rhs) => lhs.Compare(rhs);
        public static bool operator !=(Identifier lhs, Identifier rhs) => !(lhs == rhs);

        public int CompareTo(Identifier rhs) => ToString().CompareTo(rhs.ToString());
        
        public override string ToString() => $"Identifier{{packageId='{PackageId}', moduleName='{ModuleName}', entityName='{EntityName}'}}";
    }
}
