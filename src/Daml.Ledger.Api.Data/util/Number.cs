// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Runtime.Serialization;

namespace Daml.Ledger.Api.Util
{
    /// Implementation/Translation of the Java Number class to C#, as a base to the BigDecimal translation
    /// I've left the Java comments

    /**
     * The abstract class {@code Number} is the superclass of platform
     * classes representing numeric values that are convertible to the
     * primitive types {@code byte}, {@code double}, {@code float}, {@code
     * int}, {@code long}, and {@code short}.
     *
     * The specific semantics of the conversion from the numeric value of
     * a particular {@code Number} implementation to a given primitive
     * type is defined by the {@code Number} implementation in question.
     *
     * For platform classes, the conversion is often analogous to a
     * narrowing primitive conversion or a widening primitive conversion
     * as defining in <cite>The Java&trade; Language Specification</cite>
     * for converting between primitive types.  Therefore, conversions may
     * lose information about the overall magnitude of a numeric value, may
     * lose precision, and may even return a result of a different sign
     * than the input.
     *
     * See the documentation of a given {@code Number} implementation for
     * conversion details.
     *
     * @author      Lee Boynton
     * @author      Arthur van Hoff
     * @jls 5.1.2 Widening Primitive Conversions
     * @jls 5.1.3 Narrowing Primitive Conversions
     * @since   JDK1.0
     */
    public abstract class Number : ISerializable
    {
        /**
          * Returns the value of the specified number as an {@code int},
          * which may involve rounding or truncation.
          *
          * @return  the numeric value represented by this object after conversion
          *          to type {@code int}.
          */
        public abstract int IntValue();

        /**
          * Returns the value of the specified number as a {@code long},
          * which may involve rounding or truncation.
          *
          * @return  the numeric value represented by this object after conversion
          *          to type {@code long}.
          */
        public abstract long LongValue();

        /**
          * Returns the value of the specified number as a {@code float},
          * which may involve rounding.
          *
          * @return  the numeric value represented by this object after conversion
          *          to type {@code float}.
          */
        public abstract float FloatValue();

        /**
          * Returns the value of the specified number as a {@code double},
          * which may involve rounding.
          *
          * @return  the numeric value represented by this object after conversion
          *          to type {@code double}.
          */
        public abstract double DoubleValue();

        /**
          * Returns the value of the specified number as a {@code byte},
          * which may involve rounding or truncation.
          *
          * <p>This implementation returns the result of {@link #intValue} cast
          * to a {@code byte}.
          *
          * @return  the numeric value represented by this object after conversion
          *          to type {@code byte}.
          * @since   JDK1.1
          */
        public byte ByteValue()
        {
            return (byte)IntValue();
        }

        /**
          * Returns the value of the specified number as a {@code short},
          * which may involve rounding or truncation.
          *
          * <p>This implementation returns the result of {@link #intValue} cast
          * to a {@code short}.
          *
          * @return  the numeric value represented by this object after conversion
          *          to type {@code short}.
          * @since   JDK1.1
          */
        public short ShortValue()
        {
            return (short)IntValue();
        }

        public abstract void GetObjectData(SerializationInfo info, StreamingContext context);
    }
}
