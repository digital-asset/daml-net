// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Api.Data.Test.Factories
{
    using Util;

    public static class ValuesFactory
    {
        public static Value[] Values1 { get; } = { new Int64(12345), new Party("party1"), new Bool(false) };
        public static Value[] Values2 { get; } = { new Int64(54321), new Party("party2"), new Bool(true) };
        public static Value[] Values3 { get; } = { new Int64(12345), new Party("party1"), new Bool(false) };
        public static Value[] Values4 { get; } = { new Int64(11234), new Party("party4"), Unit.Instance };
        public static Value[] Values5 { get; } = { new Int64(122345), new Party("party5"), new Numeric(BigDecimal.Create("123456.789")) };
        public static Value[] Values6 { get; } = { new Int64(123455), new Party("party6"), new Date(0) };
    }
}
