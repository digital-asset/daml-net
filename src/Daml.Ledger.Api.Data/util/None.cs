// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Daml.Ledger.Api.Data.Util
{
    /// <summary>
    /// An Optional wrapper for no value, or None.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class None<T> : Optional<T>, IEquatable<None<T>>, IEquatable<None>
    {
        // A None will always map to a None so don't bother calling the mapping function
        public override Optional<TResult> Map<TResult>(Func<T, TResult> map) => None.Value;

        // A None will always map to an implicitly converted Optional<None> so don't bother calling the mapping function
        public override Optional<TResult> Map<TResult>(Func<T, Optional<TResult>> map) => None.Value;

        public override TResult MapOrElse<TResult>(Func<T, TResult> map, Func<TResult> whenNone) => whenNone();

        // Return None 
        public override Optional<T> Filter(Func<T, bool> predicate) => None.Value;

        // No value so return default value
        public override T Reduce(T whenNone) => whenNone;

        // No value so call function to get default value
        public override T Reduce(Func<T> whenNone) => whenNone();

        public override bool Equals(object obj) => !(obj is null) && (obj is None<T> || obj is None);

        public override int GetHashCode() => 0;

        public bool Equals(None<T> other) => true;

        public bool Equals(None other) => true;

        public override string ToString() => "None";

        public override bool IsPresent => false;

        // Never present so don't call the action
        public override void IfPresent(Action<T> action) { }
    }

    public sealed class None
    {
        public static None Value { get; } = new None();
        private None() { }

        public override bool Equals(object obj) => !(obj is null) && (obj is None || IsGenericNone(obj.GetType()));

        private bool IsGenericNone(Type type) => type.GenericTypeArguments.Length == 1 && typeof(None<>).MakeGenericType(type.GenericTypeArguments[0]) == type;

        public bool Equals(None other) => true;

        public override int GetHashCode() => 0;

        public override string ToString() => "None";
    }
}
