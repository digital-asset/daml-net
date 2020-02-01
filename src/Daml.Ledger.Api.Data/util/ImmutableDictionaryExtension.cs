// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Immutable;

namespace Daml.Ledger.Api.Data.Util
{
    using Daml.Ledger.Api.Data;
    
    public static class ImmutableDictionaryExtension
    {
        public static ImmutableDictionary<string, T> GetOrEmpty<T>(this ImmutableDictionary<Identifier, ImmutableDictionary<string, T>> map, Identifier key)
        {
            return map.TryGetValue(key, out var val) ? val : ImmutableDictionary<string, T>.Empty;
        }

        public static ImmutableDictionary<Identifier, ImmutableHashSet<string>> GetOrEmpty(this ImmutableDictionary<string, ImmutableDictionary<Identifier, ImmutableHashSet<string>>> map, string key)
        {
            return map.TryGetValue(key, out var val) ? val : ImmutableDictionary<Identifier, ImmutableHashSet<string>>.Empty;
        }

        public static ImmutableHashSet<string> GetOrEmpty(this ImmutableDictionary<Identifier, ImmutableHashSet<string>> map, Identifier key)
        {
            return map.TryGetValue(key, out var val) ? val : ImmutableHashSet<string>.Empty;
        }

        public static ImmutableDictionary<string, ImmutableHashSet<string>> GetOrEmpty(this ImmutableDictionary<Identifier, ImmutableDictionary<string, ImmutableHashSet<string>>> map, Identifier key)
        {
            return map.TryGetValue(key, out var val) ? val : ImmutableDictionary<string, ImmutableHashSet<string>>.Empty;
        }

        public static ImmutableHashSet<string> GetOrEmpty(this ImmutableDictionary<string, ImmutableHashSet<string>> map, string key)
        {
            return map.TryGetValue(key, out var val) ? val : ImmutableHashSet<string>.Empty;
        }
    }
}
