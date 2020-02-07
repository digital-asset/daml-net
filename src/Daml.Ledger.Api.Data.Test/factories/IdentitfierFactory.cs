// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Api.Data.Test.Factories
{
    public static class IdentifierFactory
    {
        public static Identifier Id1 { get; } = new Identifier("package1", "module1", "entity1");
        public static Identifier Id2 { get; } = new Identifier("package2", "module2", "entity2");
        public static Identifier Id3 { get; } = new Identifier("package3", "module3", "entity3");
        public static Identifier Id4 { get; } = new Identifier("package4", "module4", "entity4");
        public static Identifier Id5 { get; } = new Identifier("package5", "module5", "entity5");
        public static Identifier Id6 { get; } = new Identifier("package6", "module6", "entity6");
    }
}
