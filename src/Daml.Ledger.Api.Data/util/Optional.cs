// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Daml.Ledger.Api.Data.Util
{
#pragma warning disable CS0660  // Use the derived class Equals and GetHashCode
#pragma warning disable CS0661 

    /// <summary>
    /// Class to map an optional value. For example - a person may or may not have a Car - they have an Optional Car...
    /// If they have a car - it is Some Car, whereas if they don't it is None Car - or just None.
    /// See http://codinghelmet.com/articles/custom-implementation-of-the-option-maybe-type-in-cs
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Optional<T>
    {
        // Implicit conversions to convert to a Some or a None

        public static implicit operator Optional<T>(T value) => new Some<T>(value);
        public static implicit operator Optional<T>(None none) => new None<T>();

        // Map the wrapped value to an Optional of another type using the specified mapping function which returns the value as a new type
        public abstract Optional<TResult> Map<TResult>(Func<T, TResult> map);

        // Overload to catch where the mapping function already returns an Optional, matching the type - otherwise the other version will return an Optional<Optional<TResult>>.
        // Renamed to match the Java version
        public abstract Optional<TResult> Map<TResult>(Func<T, Optional<TResult>> map);

        // Return the contained value, or the result of the whenNone function if None
        public abstract TResult MapOrElse<TResult>(Func<T, TResult> map, Func<TResult> whenNone);

        // Return the existing Optional if the predicate returns true, otherwise None. If the value is None then None is returned
        public abstract Optional<T> Filter(Func<T, bool> predicate);

        // Extract the contained value, or the specified value if None
        public abstract T Reduce(T whenNone);

        // Extract the contained value, or the specified function return value if None - allow a lazy-evaluation lambda to be specified to get the value
        public abstract T Reduce(Func<T> whenNone);

        // Return the contained value, or throw the exception provided by the exception providing function
        public abstract T ReduceOrThrow(Func<Exception> exceptionSupplier);

        // Convert the wrapped value to the new type if possible, if not or the types are convertable then the returned value will be None
        public Optional<TNew> OfType<TNew>() where TNew : class => this is Some<T> some && typeof(TNew).IsAssignableFrom(typeof(T))
            ? (Optional<TNew>)new Some<TNew>(some.Content as TNew) : None.Value;

        // Will use the overriden Equals in the derived class
        public static bool operator ==(Optional<T> a, Optional<T> b) => ReferenceEquals(a, b) || (!(a is null) && a.Equals(b));

        public static bool operator !=(Optional<T> a, Optional<T> b) => !(a == b);

        // Some methods to mimic the Java interface for Optional to ease porting from Java

        public abstract bool IsPresent { get; }

        // Perform an action if Some
        public abstract void IfPresent(Action<T> action);
    }

#pragma warning restore CS0661
#pragma warning restore CS0660

    // Java-like factory method
    public static class Optional
    {
        public static Optional<T> Of<T>(T value)
        {
            if (value == null)
                throw new NullReferenceException("Optional value should not be null");
            return new Some<T>(value);
        }

        public static Optional<T> OfNullable<T>(T value)
        {
            if (value == null)
                return None.Value;
            return new Some<T>(value);
        }

        public static Optional<string> OfNullable(string value)
        {
            if (string.IsNullOrEmpty(value))
                return None.Value;
            return new Some<string>(value);
        }
    }
}
