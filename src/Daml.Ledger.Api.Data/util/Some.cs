// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;

namespace Com.Daml.Ledger.Api.Util
{
    /// <summary>
    /// An Optional wrapper for an actual value, as opposed to no value, or None
    /// see http://codinghelmet.com/articles/custom-implementation-of-the-option-maybe-type-in-cs
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Some<T> : Optional<T>, IEquatable<Some<T>>
    {
        public T Content { get; }

        public Some(T value) { Content = value; }

        public static implicit operator T(Some<T> some) => some.Content;

        public static implicit operator Some<T>(T value) => new Some<T>(value);

        // Return the value converted to the new type, which is then implicitly converted to an Optional of that type
        public override Optional<TResult> Map<TResult>(Func<T, TResult> map) => map(Content); 

        // Return an Optional of the new type - no conversion needed 
        public override Optional<TResult> Map<TResult>(Func<T, Optional<TResult>> map) => map(Content);

        public override TResult MapOrElse<TResult>(Func<T, TResult> map, Func<TResult> whenNone) => map(Content);
        
        // Return the existing Optional if the predicate returns true, otherwise None
        public override Optional<T> Filter(Func<T, bool> predicate)
        {
            if (predicate(Content))
                return this;
            return None.Value;
        }

        public override T Reduce(T whenNone) => Content;

        // No need to call the whenNone function as there is a value
        public override T Reduce(Func<T> whenNone) => Content;

        public bool Equals(Some<T> other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return EqualityComparer<T>.Default.Equals(Content, other.Content);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Some<T> && Equals((Some<T>)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(Content);
        }

        public override string ToString() => $"Some({ContentToString})";

        private string ContentToString => Content?.ToString() ?? "<null>";

        public override bool IsPresent => true;

        public override void IfPresent(Action<T> action) => action(Content);
    }
}
