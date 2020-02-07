// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Daml.Ledger.Api.Data.Util
{
    /// <summary>
    /// Little helper class that eases calling in a fluent and compact manner
    /// </summary>
    public class HashCodeHelper
    {
        public HashCodeHelper()
        {
            _hashCode = new HashCode();
        }

        public HashCodeHelper Add<T>(T value)
        {
            _hashCode.Add(value);
            return this;
        }

        public HashCodeHelper AddRange<T>(IEnumerable<T> values, Comparison<T> comparer = null)
        {
            var list = values.ToList();
            if (comparer != null)
                list.Sort(comparer);
            else
                list.Sort();

            foreach (var value in list)
                _hashCode.Add(value);
        
            return this;
        }

        public HashCodeHelper AddSortedRange<T>(IEnumerable<T> values)
        {
            foreach (var value in values)
                _hashCode.Add(value);

            return this;
        }

        public HashCodeHelper AddDictionary<TKey, TValue>(IReadOnlyDictionary<TKey, TValue> dictionary)
        {
            foreach (var pair in dictionary)
            {
                _hashCode.Add(pair.Key);
                if (pair.Value is IEnumerable)
                {
                    foreach (var value in (IEnumerable)pair.Value)
                        _hashCode.Add(value);
                }
                else
                    _hashCode.Add(pair.Value);
            }

            return this;
        }

        public int ToHashCode() => _hashCode.ToHashCode();

        private HashCode _hashCode;
    }

    /// <summary>
    /// Copy of the HashCode class that will be in .net standard 2.1 but we can use that yet, so I've copied it...
    /// Remove when .net standard 2.1 is available.
    /// Changed GenerateGlobalSeed to not use Interop
    /// </summary>
    public struct HashCode
    {
        private static readonly uint s_seed = HashCode.GenerateGlobalSeed();
        private uint _v1;
        private uint _v2;
        private uint _v3;
        private uint _v4;
        private uint _queue1;
        private uint _queue2;
        private uint _queue3;
        private uint _length;

        private static uint GenerateGlobalSeed()
        {
            var random = new Random();
            uint thirtyBits = (uint) random.Next(1 << 30);
            uint twoBits = (uint) random.Next(1 << 2);
            return (thirtyBits << 2) | twoBits;
        }

        public static int Combine<T1>(T1 value1)
        {
            return (int)HashCode.MixFinal(HashCode.QueueRound(HashCode.MixEmptyState() + 4U, (object)value1 != null ? (uint)value1.GetHashCode() : 0U));
        }

        public static int Combine<T1, T2>(T1 value1, T2 value2)
        {
            return (int)HashCode.MixFinal(HashCode.QueueRound(HashCode.QueueRound(HashCode.MixEmptyState() + 8U, (object)value1 != null ? (uint)value1.GetHashCode() : 0U), (object)value2 != null ? (uint)value2.GetHashCode() : 0U));
        }

        public static int Combine<T1, T2, T3>(T1 value1, T2 value2, T3 value3)
        {
            return (int)HashCode.MixFinal(HashCode.QueueRound(HashCode.QueueRound(HashCode.QueueRound(HashCode.MixEmptyState() + 12U, (object)value1 != null ? (uint)value1.GetHashCode() : 0U), (object)value2 != null ? (uint)value2.GetHashCode() : 0U), (object)value3 != null ? (uint)value3.GetHashCode() : 0U));
        }

        public static int Combine<T1, T2, T3, T4>(T1 value1, T2 value2, T3 value3, T4 value4)
        {
            uint input1 = (object)value1 != null ? (uint)value1.GetHashCode() : 0U;
            uint input2 = (object)value2 != null ? (uint)value2.GetHashCode() : 0U;
            uint input3 = (object)value3 != null ? (uint)value3.GetHashCode() : 0U;
            uint input4 = (object)value4 != null ? (uint)value4.GetHashCode() : 0U;
            uint v1_1;
            uint v2;
            uint v3;
            uint v4;
            HashCode.Initialize(out v1_1, out v2, out v3, out v4);
            uint v1_2 = HashCode.Round(v1_1, input1);
            v2 = HashCode.Round(v2, input2);
            v3 = HashCode.Round(v3, input3);
            v4 = HashCode.Round(v4, input4);
            return (int)HashCode.MixFinal(HashCode.MixState(v1_2, v2, v3, v4) + 16U);
        }

        public static int Combine<T1, T2, T3, T4, T5>(
          T1 value1,
          T2 value2,
          T3 value3,
          T4 value4,
          T5 value5)
        {
            uint input1 = (object)value1 != null ? (uint)value1.GetHashCode() : 0U;
            uint input2 = (object)value2 != null ? (uint)value2.GetHashCode() : 0U;
            uint input3 = (object)value3 != null ? (uint)value3.GetHashCode() : 0U;
            uint input4 = (object)value4 != null ? (uint)value4.GetHashCode() : 0U;
            uint queuedValue = (object)value5 != null ? (uint)value5.GetHashCode() : 0U;
            uint v1_1;
            uint v2;
            uint v3;
            uint v4;
            HashCode.Initialize(out v1_1, out v2, out v3, out v4);
            uint v1_2 = HashCode.Round(v1_1, input1);
            v2 = HashCode.Round(v2, input2);
            v3 = HashCode.Round(v3, input3);
            v4 = HashCode.Round(v4, input4);
            return (int)HashCode.MixFinal(HashCode.QueueRound(HashCode.MixState(v1_2, v2, v3, v4) + 20U, queuedValue));
        }

        public static int Combine<T1, T2, T3, T4, T5, T6>(
          T1 value1,
          T2 value2,
          T3 value3,
          T4 value4,
          T5 value5,
          T6 value6)
        {
            uint input1 = (object)value1 != null ? (uint)value1.GetHashCode() : 0U;
            uint input2 = (object)value2 != null ? (uint)value2.GetHashCode() : 0U;
            uint input3 = (object)value3 != null ? (uint)value3.GetHashCode() : 0U;
            uint input4 = (object)value4 != null ? (uint)value4.GetHashCode() : 0U;
            uint queuedValue1 = (object)value5 != null ? (uint)value5.GetHashCode() : 0U;
            uint queuedValue2 = (object)value6 != null ? (uint)value6.GetHashCode() : 0U;
            uint v1_1;
            uint v2;
            uint v3;
            uint v4;
            HashCode.Initialize(out v1_1, out v2, out v3, out v4);
            uint v1_2 = HashCode.Round(v1_1, input1);
            v2 = HashCode.Round(v2, input2);
            v3 = HashCode.Round(v3, input3);
            v4 = HashCode.Round(v4, input4);
            return (int)HashCode.MixFinal(HashCode.QueueRound(HashCode.QueueRound(HashCode.MixState(v1_2, v2, v3, v4) + 24U, queuedValue1), queuedValue2));
        }

        public static int Combine<T1, T2, T3, T4, T5, T6, T7>(
          T1 value1,
          T2 value2,
          T3 value3,
          T4 value4,
          T5 value5,
          T6 value6,
          T7 value7)
        {
            uint input1 = (object)value1 != null ? (uint)value1.GetHashCode() : 0U;
            uint input2 = (object)value2 != null ? (uint)value2.GetHashCode() : 0U;
            uint input3 = (object)value3 != null ? (uint)value3.GetHashCode() : 0U;
            uint input4 = (object)value4 != null ? (uint)value4.GetHashCode() : 0U;
            uint queuedValue1 = (object)value5 != null ? (uint)value5.GetHashCode() : 0U;
            uint queuedValue2 = (object)value6 != null ? (uint)value6.GetHashCode() : 0U;
            uint queuedValue3 = (object)value7 != null ? (uint)value7.GetHashCode() : 0U;
            uint v1_1;
            uint v2;
            uint v3;
            uint v4;
            HashCode.Initialize(out v1_1, out v2, out v3, out v4);
            uint v1_2 = HashCode.Round(v1_1, input1);
            v2 = HashCode.Round(v2, input2);
            v3 = HashCode.Round(v3, input3);
            v4 = HashCode.Round(v4, input4);
            return (int)HashCode.MixFinal(HashCode.QueueRound(HashCode.QueueRound(HashCode.QueueRound(HashCode.MixState(v1_2, v2, v3, v4) + 28U, queuedValue1), queuedValue2), queuedValue3));
        }

        public static int Combine<T1, T2, T3, T4, T5, T6, T7, T8>(
          T1 value1,
          T2 value2,
          T3 value3,
          T4 value4,
          T5 value5,
          T6 value6,
          T7 value7,
          T8 value8)
        {
            uint input1 = (object)value1 != null ? (uint)value1.GetHashCode() : 0U;
            uint input2 = (object)value2 != null ? (uint)value2.GetHashCode() : 0U;
            uint input3 = (object)value3 != null ? (uint)value3.GetHashCode() : 0U;
            uint input4 = (object)value4 != null ? (uint)value4.GetHashCode() : 0U;
            uint input5 = (object)value5 != null ? (uint)value5.GetHashCode() : 0U;
            uint input6 = (object)value6 != null ? (uint)value6.GetHashCode() : 0U;
            uint input7 = (object)value7 != null ? (uint)value7.GetHashCode() : 0U;
            uint input8 = (object)value8 != null ? (uint)value8.GetHashCode() : 0U;
            uint v1_1;
            uint v2;
            uint v3;
            uint v4;
            HashCode.Initialize(out v1_1, out v2, out v3, out v4);
            uint hash = HashCode.Round(v1_1, input1);
            v2 = HashCode.Round(v2, input2);
            v3 = HashCode.Round(v3, input3);
            v4 = HashCode.Round(v4, input4);
            uint v1_2 = HashCode.Round(hash, input5);
            v2 = HashCode.Round(v2, input6);
            v3 = HashCode.Round(v3, input7);
            v4 = HashCode.Round(v4, input8);
            return (int)HashCode.MixFinal(HashCode.MixState(v1_2, v2, v3, v4) + 32U);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint Rol(uint value, int count)
        {
            return value << count | value >> 32 - count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Initialize(out uint v1, out uint v2, out uint v3, out uint v4)
        {
            v1 = (uint)((int)HashCode.s_seed - 1640531535 - 2048144777);
            v2 = HashCode.s_seed + 2246822519U;
            v3 = HashCode.s_seed;
            v4 = HashCode.s_seed - 2654435761U;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint Round(uint hash, uint input)
        {
            hash += input * 2246822519U;
            hash = HashCode.Rol(hash, 13);
            hash *= 2654435761U;
            return hash;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint QueueRound(uint hash, uint queuedValue)
        {
            hash += queuedValue * 3266489917U;
            return HashCode.Rol(hash, 17) * 668265263U;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint MixState(uint v1, uint v2, uint v3, uint v4)
        {
            return HashCode.Rol(v1, 1) + HashCode.Rol(v2, 7) + HashCode.Rol(v3, 12) + HashCode.Rol(v4, 18);
        }

        private static uint MixEmptyState()
        {
            return HashCode.s_seed + 374761393U;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint MixFinal(uint hash)
        {
            hash ^= hash >> 15;
            hash *= 2246822519U;
            hash ^= hash >> 13;
            hash *= 3266489917U;
            hash ^= hash >> 16;
            return hash;
        }

        public void Add<T>(T value)
        {
            this.Add((object)value != null ? value.GetHashCode() : 0);
        }

        public void Add<T>(T value, IEqualityComparer<T> comparer)
        {
            this.Add(comparer != null ? comparer.GetHashCode(value) : ((object)value != null ? value.GetHashCode() : 0));
        }

        private void Add(int value)
        {
            uint input = (uint)value;
            uint num = this._length++;
            switch (num % 4U)
            {
                case 0:
                    this._queue1 = input;
                    break;
                case 1:
                    this._queue2 = input;
                    break;
                case 2:
                    this._queue3 = input;
                    break;
                default:
                    if (num == 3U)
                        HashCode.Initialize(out this._v1, out this._v2, out this._v3, out this._v4);
                    this._v1 = HashCode.Round(this._v1, this._queue1);
                    this._v2 = HashCode.Round(this._v2, this._queue2);
                    this._v3 = HashCode.Round(this._v3, this._queue3);
                    this._v4 = HashCode.Round(this._v4, input);
                    break;
            }
        }

        public int ToHashCode()
        {
            uint length = this._length;
            uint num = length % 4U;
            uint hash = (length < 4U ? HashCode.MixEmptyState() : HashCode.MixState(this._v1, this._v2, this._v3, this._v4)) + length * 4U;
            if (num > 0U)
            {
                hash = HashCode.QueueRound(hash, this._queue1);
                if (num > 1U)
                {
                    hash = HashCode.QueueRound(hash, this._queue2);
                    if (num > 2U)
                        hash = HashCode.QueueRound(hash, this._queue3);
                }
            }
            return (int)HashCode.MixFinal(hash);
        }
    }
}

