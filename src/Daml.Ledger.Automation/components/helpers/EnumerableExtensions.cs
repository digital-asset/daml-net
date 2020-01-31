// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Linq;

namespace Daml.Ledger.Automation.Components.Helpers
{
    public static class EnumerableExtensions
    {

        public static IEnumerable<R> Map<T, R>(this IEnumerable<T> self, Func<T, R> selector) => self.Select(selector);

        public static T Reduce<T>(this IEnumerable<T> self, Func<T, T, T> func) => self.Aggregate(func);

        public static R Reduce<T, R>(this IEnumerable<T> self, R seed, Func<R, T, R> func) => self.Aggregate<T, R>(seed, func);

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> self, Func<T, bool> predicate) => self.Where(predicate);

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> self) => self == null || !self.Any();
    }
}
