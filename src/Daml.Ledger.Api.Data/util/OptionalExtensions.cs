// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Linq;

namespace Com.Daml.Ledger.Api.Util
{
    public static class OptionalExtensions
    {
        public static Optional<T> When<T>(this T obj, bool condition) => condition ? (Optional<T>)new Some<T>(obj) : None.Value;

        public static Optional<T> When<T>(this T obj, Func<T, bool> predicate) => obj.When(predicate(obj));

        public static Optional<T> NoneIfNull<T>(this T obj) => obj.When(!object.ReferenceEquals(obj, null));

        public static Optional<T> FirstOrNone<T>(this IEnumerable<T> sequence) => sequence.Select(x => (Optional<T>)new Some<T>(x)).DefaultIfEmpty(None.Value).First();

        public static Optional<T> FirstOrNone<T>(this IEnumerable<T> sequence, Func<T, bool> predicate) => sequence.Where(predicate).FirstOrNone();

        // Apply a function returning an Optional to each member of a sequence and then return a sequence without any None items 
        public static IEnumerable<TResult> SelectOptional<T, TResult>(this IEnumerable<T> sequence, Func<T, Optional<TResult>> map)
        {
            return sequence.Select(map).OfType<Some<TResult>>().Select(some => some.Content);
        }

        // Return a Some optional if the dictionary contains a key, else None
        public static Optional<TValue> TryGetValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out TValue value) ? (Optional<TValue>) new Some<TValue>(value) : None.Value;
        }
    }
}
