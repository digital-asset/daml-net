// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Com.Daml.Ledger.Api.Data.Test.Factories
{
    public static class TransactionFactory
    {
        private static readonly DateTimeOffset _now = DateTimeOffset.UtcNow;

        public static Transaction Transaction1 { get; } = new Transaction("transaction1", "command1", "workflow1", _now, new[] { CreatedEventFactory.Event1, CreatedEventFactory.Event2 }, "offset1");
        public static Transaction Transaction2 { get; } = new Transaction("transaction2", "command2", "workflow2", _now.AddDays(3), new[] { CreatedEventFactory.Event3 }, "offset2");
        public static Transaction Transaction3 { get; } = new Transaction("transaction1", "command1", "workflow1", _now, new[] { CreatedEventFactory.Event1, CreatedEventFactory.Event2 }, "offset1");
    }
}
