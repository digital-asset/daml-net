// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Com.Daml.Ledger.Api.Data.Test.Factories
{
    public static class TransactionTreeFactory
    {
        private static readonly DateTimeOffset _now = DateTimeOffset.UtcNow;

        public static TransactionTree TransactionTree1 { get; } = new TransactionTree("transaction1", "command1", "workflow1", _now, CreatedEventFactory.EventsById1, CreatedEventFactory.EventsById1.Keys, "offset1");
        public static TransactionTree TransactionTree2 = new TransactionTree("transaction2", "command2", "workflow2", _now.AddDays(2), CreatedEventFactory.EventsById2, CreatedEventFactory.EventsById2.Keys, "offset2");
        public static TransactionTree TransactionTree3 = new TransactionTree("transaction1", "command1", "workflow1", _now, CreatedEventFactory.EventsById1, CreatedEventFactory.EventsById1.Keys, "offset1");
    }
}
