// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

/**
 * Source code nabbed from the clojure repo at https://github.com/clojure/clojure-clr/tree/master/Clojure/Clojure/Lib
 * I wanted a java-like BigDecimal but translating it, and BigInteger, was a big task
 * Covered by the Eclipse license, so we are OK :-)
 * Removed a lot of this code
 */

/**
 *   Copyright (c) Rich Hickey. All rights reserved.
 *   The use and distribution terms for this software are covered by the
 *   Eclipse Public License 1.0 (http://opensource.org/licenses/eclipse-1.0.php)
 *   which can be found in the file epl-v10.html at the root of this distribution.
 *   By using this software in any fashion, you are agreeing to be bound by
 * 	 the terms of this license.
 *   You must not remove this notice, or any other, from this software.
 **/

/**
 *   Author: David Miller
 **/

namespace Daml.Ledger.Api.Data.Util
{
    public static class Util
    {
        public static int HashCombine(int seed, int hash)
        {
            //a la boost
            return (int)(seed ^ (hash + 0x9e3779b9 + (seed << 6) + (seed >> 2)));
        }

        public static int BitCount(int x)
        {
            x -= ((x >> 1) & 0x55555555);
            x = (((x >> 2) & 0x33333333) + (x & 0x33333333));
            x = (((x >> 4) + x) & 0x0f0f0f0f);
            unchecked
            {
                return ((x * 0x01010101) >> 24);
            }
        }

        // A variant of the above that avoids multiplying
        // This algo is in a lot of places.
        // See, for example, http://aggregate.org/MAGIC/#Population%20Count%20(Ones%20Count)
        public static uint BitCount(uint x)
        {
            x -= ((x >> 1) & 0x55555555);
            x = (((x >> 2) & 0x33333333) + (x & 0x33333333));
            x = (((x >> 4) + x) & 0x0f0f0f0f);
            x += (x >> 8);
            x += (x >> 16);
            return (x & 0x0000003f);
        }

        // This algo is in a lot of places.
        // See, for example, http://aggregate.org/MAGIC/#Leading%20Zero%20Count
        public static uint LeadingZeroCount(uint x)
        {
            x |= (x >> 1);
            x |= (x >> 2);
            x |= (x >> 4);
            x |= (x >> 8);
            x |= (x >> 16);

            return 32u - BitCount(x);

            // THE DLR BigInteger code uses the following.
            // It's probably faster.

            //int shift = 0;

            //if ((value & 0xFFFF0000) == 0) { value <<= 16; shift += 16; }
            //if ((value & 0xFF000000) == 0) { value <<= 8; shift += 8; }
            //if ((value & 0xF0000000) == 0) { value <<= 4; shift += 4; }
            //if ((value & 0xC0000000) == 0) { value <<= 2; shift += 2; }
            //if ((value & 0x80000000) == 0) { value <<= 1; shift += 1; }

            //return shift;
        }
    }
}
