// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Linq;

namespace Daml.Ledger.Api.Data.Test.Factories
{
    public static class RecordFactory
    {
        public static Record Record1 { get; } = new Record(IdentifierFactory.Id1, from v in ValuesFactory.Values1 select new Record.Field(v));
        public static Record Record2 { get; } = new Record(from v in ValuesFactory.Values2 select new Record.Field(v));
        public static Record Record3 { get; } = new Record(IdentifierFactory.Id1, from v in ValuesFactory.Values1 select new Record.Field(v));
        public static Record Record4 { get; } = new Record(from v in ValuesFactory.Values4 select new Record.Field(v));
        public static Record Record5 { get; } = new Record(IdentifierFactory.Id5, from v in ValuesFactory.Values5 select new Record.Field(v));
        public static Record Record6 { get; } = new Record(from v in ValuesFactory.Values6 select new Record.Field(v));
    }
}
