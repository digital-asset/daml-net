// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Api.Data.Test.Factories
{
    public static class CreateCommandsFactory
    {
        public static Command[] Commands1 { get; } = { new CreateCommand(IdentifierFactory.Id1, RecordFactory.Record1), new CreateCommand(IdentifierFactory.Id2, RecordFactory.Record2) };
        public static Command[] Commands2 { get; } = { new CreateCommand(IdentifierFactory.Id3, RecordFactory.Record3), new CreateCommand(IdentifierFactory.Id4, RecordFactory.Record4) };
    }
}
