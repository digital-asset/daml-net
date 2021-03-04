/**
 * Source code nabbed from the clojure repo at https://github.com/clojure/clojure-clr/tree/master/Clojure/Clojure/Lib
 * I wanted a java-like BigDecimal but translating it, and BigInteger, was a big task
 * Covered by the Eclipse license, so we are OK :-)
 */

/**
 *   Copyright (c) Rich Hickey. All rights reserved.
 *   The use and distribution terms for this software are covered by the
 *   Eclipse Public License 1.0 (http://opensource.org/licenses/eclipse-1.0.php)
 *   which can be found in the file epl-v10.html at the root of this distribution.
 *   By using this software in any fashion, you are agreeing to be bound by
 *   the terms of this license.
 *   You must not remove this notice, or any other, from this software.
 **/

/**
 *   Author: David Miller
 **/

using System;
using Xunit;

namespace Daml.Ledger.Api.Data.Util.Test
{    
    public class BigIntegerTest
    {

        #region Basic accessor tests

        [Fact]
        public void Signum_is_zero_for_zero()
        {
            BigInteger i = BigInteger.Create(0);
            Assert.Equal(0, i.Signum);
        }

        [Fact]
        public void Magnitude_is_same_for_pos_and_neg()
        {
            BigInteger neg = BigInteger.Create(-100);
            BigInteger pos = BigInteger.Create(+100);
            Assert.True(SameMag(neg.GetMagnitude(), pos.GetMagnitude()));
        }


        [Fact]
        public void Magnitude_is_zero_length_for_zero()
        {
            BigInteger i = BigInteger.Create(0);
            Assert.Empty(i.GetMagnitude());
        }


        [Fact]
        public void Signum_is_m1_for_negative()
        {
            BigInteger i = BigInteger.Create(-100);
            Assert.Equal(-1, i.Signum);
        }

        [Fact]
        public void Signum_is_1_for_negative()
        {
            BigInteger i = BigInteger.Create(+100);
            Assert.Equal(1, i.Signum);
        }

        [Fact]
        public void IsPositive_works()
        {
            BigInteger i = BigInteger.Create(0);
            Assert.False(i.IsPositive);

            i = BigInteger.Create(100);
            Assert.True(i.IsPositive);

            i = BigInteger.Create(-100);
            Assert.False(i.IsPositive);
        }

        [Fact]
        public void IsNegative_works()
        {
            BigInteger i = BigInteger.Create(0);
            Assert.False(i.IsNegative);

            i = BigInteger.Create(-100);
            Assert.True(i.IsNegative);

            i = BigInteger.Create(100);
            Assert.False(i.IsNegative);
        }

        [Fact]
        public void IsZero_works()
        {
            BigInteger i = BigInteger.Create(0);
            Assert.True(i.IsZero);

            i = BigInteger.Create(-100);
            Assert.False(i.IsZero);

            i = BigInteger.Create(100);
            Assert.False(i.IsZero);
        }



        #endregion

        #region Basic factory tests

        [Fact]
        public void Create_ulong_various()
        {
            BigInteger i;

            i = BigInteger.Create((ulong)0);
            Assert.True(SameValue(i, 0, new uint[0]));

            i = BigInteger.Create((ulong)100);
            Assert.True(SameValue(i, 1, new uint[] { 100 }));

            i = BigInteger.Create((ulong)0x00ffeeddccbbaa99);
            Assert.True(SameValue(i, 1, new uint[] { 0x00ffeedd, 0xccbbaa99 }));
        }


        [Fact]
        public void Create_uint_various()
        {
            BigInteger i;

            i = BigInteger.Create((uint)0);
            Assert.True(SameValue(i, 0, new uint[0]));

            i = BigInteger.Create((uint)100);
            Assert.True(SameValue(i, 1, new uint[] { 100 }));

            i = BigInteger.Create((ulong)0xffeeddcc);
            Assert.True(SameValue(i, 1, new uint[] { 0xffeeddcc }));
        }

        [Fact]
        public void Create_long_various()
        {
            BigInteger i;

            i = BigInteger.Create((long)0);
            Assert.True(SameValue(i, 0, new uint[0]));

            i = BigInteger.Create(System.Int64.MinValue);
            Assert.True(SameValue(i, -1, new uint[] { 0x80000000, 0 }));

            i = BigInteger.Create((long)100);
            Assert.True(SameValue(i, 1, new uint[] { 100 }));

            i = BigInteger.Create((long)-100);
            Assert.True(SameValue(i, -1, new uint[] { 100 }));

            i = BigInteger.Create((long)0x00ffeeddccbbaa99);
            Assert.True(SameValue(i, 1, new uint[] { 0x00ffeedd, 0xccbbaa99 }));

            i = BigInteger.Create(unchecked((long)0xffffeeddccbbaa99));
            Assert.True(SameValue(i, -1, new uint[] { 0x00001122, 0x33445567 }));
        }

        [Fact]
        public void Create_int_various()
        {
            BigInteger i;

            i = BigInteger.Create((int)0);
            Assert.True(SameValue(i, 0, new uint[0]));

            i = BigInteger.Create(Int32.MinValue);
            Assert.True(SameValue(i, -1, new uint[] { 0x80000000 }));

            i = BigInteger.Create((int)100);
            Assert.True(SameValue(i, 1, new uint[] { 100 }));

            i = BigInteger.Create((int)-100);
            Assert.True(SameValue(i, -1, new uint[] { 100 }));

            i = BigInteger.Create((int)0x00ffeedd);
            Assert.True(SameValue(i, 1, new uint[] { 0x00ffeedd }));

            i = BigInteger.Create(unchecked((int)0xffffeedd));
            Assert.True(SameValue(i, -1, new uint[] { 0x00001123 }));
        }

        [Fact]
        public void Create_decimal_various()
        {
            BigInteger i;

            i = BigInteger.Create(0M);
            Assert.True(SameValue(i, 0, new uint[0]));

            i = BigInteger.Create(0.9M);
            Assert.True(SameValue(i, 0, new uint[0]));

            i = BigInteger.Create(Decimal.MinValue);
            Assert.True(SameValue(i, -1, new uint[] { uint.MaxValue, uint.MaxValue, uint.MaxValue }));

            i = BigInteger.Create(Decimal.MaxValue);
            Assert.True(SameValue(i, 1, new uint[] { uint.MaxValue, uint.MaxValue, uint.MaxValue }));

            i = BigInteger.Create(280.77M);
            Assert.True(SameValue(i, 1, new uint[] { 280 }));

            i = BigInteger.Create(-18446744073709551615.994M);
            Assert.True(SameValue(i, -1, new uint[] { 0xFFFFFFFF, 0xFFFFFFFF }));
        }

        [Fact]
        public void Create_double_various()
        {
            BigInteger i;

            i = BigInteger.Create(0.0);
            Assert.True(SameValue(i, 0, new uint[0]));

            i = BigInteger.Create(1.0);
            Assert.True(SameValue(i, 1, new uint[] { 1 }));

            i = BigInteger.Create(-1.0);
            Assert.True(SameValue(i, -1, new uint[] { 1 }));

            i = BigInteger.Create(10.0);
            Assert.True(SameValue(i, 1, new uint[] { 10 }));

            i = BigInteger.Create(12345678.123);
            Assert.True(SameValue(i, 1, new uint[] { 12345678 }));

            i = BigInteger.Create(4.2949672950000000E+009);
            Assert.True(SameValue(i, 1, new uint[] { 4294967295 }));

            i = BigInteger.Create(4.2949672960000000E+009);
            Assert.True(SameValue(i, 1, new uint[] { 0x1, 0x0 }));

            i = BigInteger.Create(-1.2345678901234569E+300);
            Assert.True(SameValue(i, -1, new uint[] {
                0x1D,
                0x7EE8BCBB,
                0xD3520000,
                0x0, 0x0, 0x0, 0x0,
                0x0, 0x0, 0x0, 0x0,
                0x0, 0x0, 0x0, 0x0,
                0x0, 0x0, 0x0, 0x0,
                0x0, 0x0, 0x0, 0x0,
                0x0, 0x0, 0x0, 0x0,
                0x0, 0x0, 0x0, 0x0,
                0x0 }));

        }

        [Fact]
        public void Create_double_powers_of_two()
        {
            // Powers of two are special-cased in the code.

            for (int i = 0; i < Math.Log(Double.MaxValue, 2); i++)
            {
                BigInteger b = BigInteger.Create(Math.Pow(2.0, i));
                Assert.True(b == BigInteger.One << i);
            }
        }


        [Fact]
        public void Create_double_fails_on_pos_infinity()
        {
            Assert.Throws<OverflowException>(() => BigInteger.Create(Double.PositiveInfinity));
        }

        [Fact]
        public void Create_double_fails_on_neg_infinity()
        {
            Assert.Throws<OverflowException>(() => BigInteger.Create(Double.NegativeInfinity));
        }

        [Fact]
        public void Create_double_fails_on_NaN()
        {
            Assert.Throws<OverflowException>(() => BigInteger.Create(Double.NaN));
        }

        #endregion

        #region Ctor tests

        [Fact]
        public void BI_basic_ctor_handles_zero()
        {
            BigInteger i = new BigInteger(0);
            Assert.True(SameValue(i, 0, new uint[0]));
        }

        [Fact]
        public void BI_basic_ctor_handles_basic_data_positive()
        {
            uint[] data = new uint[] { 0xffeeddcc, 0xbbaa9988, 0x77665544 };
            BigInteger i = new BigInteger(1, data);
            Assert.True(i.IsPositive);
            Assert.True(SameValue(i, 1, data));
        }

        [Fact]
        public void BI_basic_ctor_handles_basic_data_negative()
        {
            uint[] data = new uint[] { 0xffeeddcc, 0xbbaa9988, 0x77665544 };
            BigInteger i = new BigInteger(-1, data);
            Assert.True(i.IsNegative);
            Assert.True(SameValue(i, -1, data));
        }

        [Fact]
        public void BI_basic_ctor_fails_on_bad_sign_neg()
        {
            uint[] data = new uint[] { 1 };
            Assert.Throws<ArgumentException>(() => new BigInteger(-2, data));
        }

        [Fact]
        public void BI_basic_ctor_fails_on_bad_sign_pos()
        {
            uint[] data = new uint[] { 1 };
            Assert.Throws<ArgumentException>(() => new BigInteger(2, data));
        }

        [Fact]
        public void BI_basic_ctor_fails_on_null_mag()
        {
            Assert.Throws<NullReferenceException>(() => new BigInteger(1, null));
        }

        [Fact]
        public void BI_basic_ctor_fails_on_zero_sign_on_nonzero_mag()
        {
            uint[] data = new uint[] { 1 };
            Assert.Throws<ArgumentException>(() => new BigInteger(0, data));
        }

        [Fact]
        public void BI_basic_ctor_normalized_magnitude()
        {
            uint[] data = new uint[] { 0, 0, 1, 0 };
            uint[] normData = new uint[] { 1, 0 };
            BigInteger i = new BigInteger(1, data);
            Assert.True(SameValue(i, 1, normData));
        }

        [Fact]
        public void BI_basic_ctor_detects_all_zero_mag()
        {
            uint[] data = new uint[] { 0, 0, 0, 0, 0 };
            BigInteger i = new BigInteger(1, data);
            Assert.True(SameValue(i, 0, new uint[0]));
        }

        [Fact]
        public void BI_copy_ctor_works()
        {
            BigInteger i = new BigInteger(1, new uint[] { 1, 2, 3 });
            BigInteger c = new BigInteger(i);
            Assert.True(SameValue(c, i.Signum, i.GetMagnitude()));
        }

        #endregion

        #region Parsing tests

        [Fact]
        public void Parse_detects_radix_too_small()
        {
            BigInteger i;
            bool result = BigInteger.TryParse("0", 1, out i);
            Assert.False(result);
        }

        [Fact]
        public void Parse_detects_radix_too_large()
        {
            BigInteger i;
            bool result = BigInteger.TryParse("0", 37, out i);
            Assert.False(result);
        }

        [Fact]
        public void Parse_zero()
        {
            BigInteger i;
            bool result = BigInteger.TryParse("0", 10, out i);
            Assert.True(result);
            Assert.True(SameValue(i, 0, new uint[0]));
        }

        [Fact]
        public void Parse_negative_zero_just_zero()
        {
            BigInteger i;
            bool result = BigInteger.TryParse("-0", 10, out i);
            Assert.True(result);
            Assert.True(SameValue(i, 0, new uint[0]));
        }

        [Fact]
        public void Parse_multiple_zeros_is_zero()
        {
            BigInteger i;
            bool result = BigInteger.TryParse("00000", 10, out i);
            Assert.True(result);
            Assert.True(SameValue(i, 0, new uint[0]));
        }

        [Fact]
        public void Parse_multiple_zeros_with_leading_minus_is_zero()
        {
            BigInteger i;
            bool result = BigInteger.TryParse("-00000", 10, out i);
            Assert.True(result);
            Assert.True(SameValue(i, 0, new uint[0]));
        }

        [Fact]
        public void Parse_multiple_hyphens_fails()
        {
            BigInteger i;
            bool result = BigInteger.TryParse("-123-4", 10, out i);
            Assert.False(result);
        }

        [Fact]
        public void Parse_adjacent_hyphens_fails()
        {
            BigInteger i;
            bool result = BigInteger.TryParse("--1234", 10, out i);
            Assert.False(result);
        }

        [Fact]
        public void Parse_just_adjacent_hyphens_fails()
        {
            BigInteger i;
            bool result = BigInteger.TryParse("--", 10, out i);
            Assert.False(result);
        }

        [Fact]
        public void Parse_hyphen_only_fails()
        {
            BigInteger i;
            bool result = BigInteger.TryParse("-", 10, out i);
            Assert.False(result);
        }

        [Fact]
        public void Parse_fails_on_bogus_char()
        {
            BigInteger i;
            bool result = BigInteger.TryParse("123.56", 10, out i);
            Assert.False(result);
        }

        [Fact]
        public void Parse_fails_on_digit_out_of_range_base_2()
        {
            BigInteger i;
            bool result = BigInteger.TryParse("01010120101", 2, out i);
            Assert.False(result);
        }


        [Fact]
        public void Parse_fails_on_digit_out_of_range_base_8()
        {
            BigInteger i;
            bool result = BigInteger.TryParse("01234567875", 8, out i);
            Assert.False(result);
        }

        [Fact]
        public void Parse_fails_on_digit_out_of_range_base_16()
        {
            BigInteger i;
            bool result = BigInteger.TryParse("CabBaGe", 16, out i);
            Assert.False(result);
        }

        [Fact]
        public void Parse_fails_on_digit_out_of_range_in_later_super_digit_base_16()
        {
            BigInteger i;
            bool result = BigInteger.TryParse("AAAAAAAAAAAAAAAAAAAAAAACabBaGe", 16, out i);
            Assert.False(result);
        }

        [Fact]
        public void Parse_simple_base_2()
        {
            BigInteger i;
            bool result = BigInteger.TryParse("100", 2, out i);
            Assert.True(result);
            Assert.True(SameValue(i, 1, new uint[] { 4 }));
        }

        [Fact]
        public void Parse_simple_base_10()
        {
            BigInteger i;
            bool result = BigInteger.TryParse("100", 10, out i);
            Assert.True(result);
            Assert.True(SameValue(i, 1, new uint[] { 100 }));
        }

        [Fact]
        public void Parse_simple_base_16()
        {
            BigInteger i;
            bool result = BigInteger.TryParse("100", 16, out i);
            Assert.True(result);
            Assert.True(SameValue(i, 1, new uint[] { 0x100 }));
        }

        [Fact]
        public void Parse_simple_base_36()
        {
            BigInteger i;
            bool result = BigInteger.TryParse("100", 36, out i);
            Assert.True(result);
            Assert.True(SameValue(i, 1, new uint[] { 36 * 36 }));
        }

        [Fact]
        public void Parse_works_on_long_string_base_16()
        {
            BigInteger i;
            bool result = BigInteger.TryParse("100000000000000000000", 16, out i);
            Assert.True(result);
            Assert.True(SameValue(i, 1, new uint[] { 0x00010000, 0, 0 }));
        }


        [Fact]
        public void Parse_works_on_long_string_base_10()
        {
            BigInteger i;
            bool result = BigInteger.TryParse("123456789012345678901234567890", 10, out i);
            Assert.True(result);
            Assert.True(SameValue(i, 1, new uint[] { 0x1, 0x8ee90ff6, 0xc373e0ee, 0x4e3f0ad2 }));
        }

        [Fact]
        public void Parse_works_with_leading_minus_sign()
        {
            BigInteger i;
            bool result = BigInteger.TryParse("-123456789012345678901234567890", 10, out i);
            Assert.True(result);
            Assert.True(SameValue(i, -1, new uint[] { 0x1, 0x8ee90ff6, 0xc373e0ee, 0x4e3f0ad2 }));
        }

        [Fact]
        public void Parse_works_with_leading_plus_sign()
        {
            BigInteger i;
            bool result = BigInteger.TryParse("+123456789012345678901234567890", 10, out i);
            Assert.True(result);
            Assert.True(SameValue(i, 1, new uint[] { 0x1, 0x8ee90ff6, 0xc373e0ee, 0x4e3f0ad2 }));
        }

        [Fact]
        public void Parse_works_on_long_string_base_10_2()
        {
            BigInteger i;
            bool result = BigInteger.TryParse("1024000001024000001024", 10, out i);
            Assert.True(result);
            Assert.True(SameValue(i, 1, new uint[] { 0x37, 0x82dacf8b, 0xfb280400 }));
        }

        #endregion

        #region ToString tests

        [Fact]
        public void ToString_fails_on_radix_too_small()
        {
            BigInteger i = new BigInteger(0, new uint[0]);
            Assert.Throws<ArgumentOutOfRangeException>(() => i.ToString(1));
        }

        [Fact]
        public void ToString_detects_radix_too_large()
        {
            BigInteger i = new BigInteger(0, new uint[0]);
            Assert.Throws<ArgumentOutOfRangeException>(() => i.ToString(37));
        }

        [Fact]
        public void ToString_on_zero_works_for_all_radixes()
        {
            BigInteger i = new BigInteger(0, new uint[0]);
            for (uint radix = BigInteger.MinRadix; radix <= BigInteger.MaxRadix; radix++)
                Assert.Equal("0", i.ToString(radix));
        }

        [Fact]
        public void ToString_simple_base_2()
        {
            BigInteger i = new BigInteger(1, new uint[] { 4 });
            string result = i.ToString(2);
            Assert.Equal("100", result);
        }

        [Fact]
        public void ToString_simple_base_10()
        {
            BigInteger i = new BigInteger(1, new uint[] { 927 });
            string result = i.ToString(10);
            Assert.Equal("927", result);
        }

        [Fact]
        public void ToString_simple_base_16()
        {
            BigInteger i = new BigInteger(1, new uint[] { 0xa20f5 });
            string result = i.ToString(16);
            Assert.Equal("A20F5", result);
        }

        [Fact]
        public void ToString_simple_base_26()
        {
            BigInteger i = new BigInteger(1, new uint[] { 23 * 26 * 26 + 12 * 26 + 15 });
            string result = i.ToString(26);
            Assert.Equal("NCF", result);
        }

        [Fact]
        public void ToString_long_base_16()
        {
            BigInteger i = new BigInteger(-1, new uint[] { 0x00FEDCBA, 0x12345678, 0x87654321 });
            string result = i.ToString(16);
            Assert.Equal("-FEDCBA1234567887654321", result);
        }

        [Fact]
        public void ToString_long_base_10()
        {
            BigInteger i = new BigInteger(1, new uint[] { 0x1, 0x8ee90ff6, 0xc373e0ee, 0x4e3f0ad2 });
            string result = i.ToString(10);
            Assert.Equal("123456789012345678901234567890", result);
        }

        [Fact]
        public void ToString_long_base_10_2()
        {
            BigInteger i = new BigInteger(1, new uint[] { 0x37, 0x82dacf8b, 0xfb280400 });
            string result = i.ToString(10);
            Assert.Equal("1024000001024000001024", result);
        }

        #endregion

        #region Comparison tests

        [Fact]
        public void Compare_on_zeros_is_0()
        {
            BigInteger x = new BigInteger(0, new uint[0]);
            BigInteger y = new BigInteger(0, new uint[0]);
            Assert.Equal(0, BigInteger.Compare(x, y));
        }

        void Compare_neg_pos_is_minus1()
        {
            BigInteger x = new BigInteger(-1, new uint[] { 0xffffffff, 0xffffffff });
            BigInteger y = new BigInteger(1, new uint[] { 0x1 });
            Assert.Equal(-1, BigInteger.Compare(x, y));
        }

        void Compare_pos_neg_is_plus1()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0x1 });
            BigInteger y = new BigInteger(-1, new uint[] { 0xffffffff, 0xffffffff });
            Assert.Equal(1, BigInteger.Compare(x, y));
        }

        void Compare_negs_smaller_len_first_is_plus1()
        {
            BigInteger x = new BigInteger(-1, new uint[] { 0xffffffff });
            BigInteger y = new BigInteger(-1, new uint[] { 0xffffffff, 0xffffffff });
            Assert.Equal(1, BigInteger.Compare(x, y));
        }

        void Compare_negs_larger_len_first_is_minus1()
        {
            BigInteger x = new BigInteger(-1, new uint[] { 0xffffffff, 0xffffffff });
            BigInteger y = new BigInteger(-1, new uint[] { 0xffffffff });
            Assert.Equal(-1, BigInteger.Compare(x, y));
        }

        void Compare_pos_smaller_len_first_is_minus1()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0xffffffff });
            BigInteger y = new BigInteger(1, new uint[] { 0xffffffff, 0xffffffff });
            Assert.Equal(-1, BigInteger.Compare(x, y));
        }

        void Compare_pos_larger_len_first_is_plus1()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0xffffffff, 0xffffffff });
            BigInteger y = new BigInteger(1, new uint[] { 0xffffffff });
            Assert.Equal(1, BigInteger.Compare(x, y));
        }

        void Compare_same_len_smaller_first_diff_in_MSB_is_minus1()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0xfffffffe, 0x12345678, 0xffffffff });
            BigInteger y = new BigInteger(1, new uint[] { 0xffffffff, 0x12345678, 0xffffffff });
            Assert.Equal(-1, BigInteger.Compare(x, y));
        }

        void Compare_same_len_smaller_first_diff_in_LSB_is_minus1()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0xffffffff, 0x12345678, 0xfffffffe });
            BigInteger y = new BigInteger(1, new uint[] { 0xffffffff, 0x12345678, 0xffffffff });
            Assert.Equal(-1, BigInteger.Compare(x, y));
        }

        void Compare_same_len_smaller_first_diff_in_middle_is_minus1()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0xffffffff, 0x12335678, 0xffffffff });
            BigInteger y = new BigInteger(1, new uint[] { 0xffffffff, 0x12345678, 0xffffffff });
            Assert.Equal(-1, BigInteger.Compare(x, y));
        }

        void Compare_same_len_larger_first_diff_in_MSB_is_plus1()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0xfffffffe, 0x12345678, 0xffffffff });
            BigInteger y = new BigInteger(1, new uint[] { 0xffffffff, 0x12345678, 0xffffffff });
            Assert.Equal(-1, BigInteger.Compare(x, y));
        }

        void Compare_same_len_larger_first_diff_in_LSB_is_plus1()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0xffffffff, 0x12345678, 0xfffffffe });
            BigInteger y = new BigInteger(1, new uint[] { 0xffffffff, 0x12345678, 0xffffffff });
            Assert.Equal(-1, BigInteger.Compare(x, y));
        }

        void Compare_same_len_larger_first_diff_in_middle_is_plus1()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0xffffffff, 0x12335678, 0xffffffff });
            BigInteger y = new BigInteger(1, new uint[] { 0xffffffff, 0x12345678, 0xffffffff });
            Assert.Equal(-1, BigInteger.Compare(x, y));
        }

        void Compare_same_is_0()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0xffffffff, 0x12345678, 0xffffffff });
            BigInteger y = new BigInteger(1, new uint[] { 0xffffffff, 0x12345678, 0xffffffff });
            Assert.Equal(0, BigInteger.Compare(x, y));
        }

        #endregion

        #region Add/Subtract/Negate/Abs tests

        [Fact]
        public void Negate_zero_is_zero()
        {
            BigInteger x = new BigInteger(0, new uint[0]);
            BigInteger xn = x.Negate();
            Assert.True(SameValue(xn, 0, new uint[0]));
        }

        [Fact]
        public void Negate_positive_is_same_mag_neg()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0xfedcba98, 0x87654321 });
            BigInteger xn = x.Negate();
            Assert.True(SameValue(xn, -1, x.GetMagnitude()));
        }

        [Fact]
        public void Negate_negative_is_same_mag_pos()
        {
            BigInteger x = new BigInteger(-1, new uint[] { 0xfedcba98, 0x87654321 });
            BigInteger xn = x.Negate();
            Assert.True(SameValue(xn, 1, x.GetMagnitude()));
        }

        [Fact]
        public void Add_pos_same_length_no_carry()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0x12345678, 0x12345678 });
            BigInteger y = new BigInteger(1, new uint[] { 0x23456789, 0x13243546 });
            BigInteger z = x.Add(y);

            Assert.True(SameValue(z, 1, new uint[] { 0x3579BE01, 0x25588BBE }));
        }

        [Fact]
        public void Add_neg_same_length_no_carry()
        {
            BigInteger x = new BigInteger(-1, new uint[] { 0x12345678, 0x12345678 });
            BigInteger y = new BigInteger(-1, new uint[] { 0x23456789, 0x13243546 });
            BigInteger z = x.Add(y);

            Assert.True(SameValue(z, -1, new uint[] { 0x3579BE01, 0x25588BBE }));
        }

        [Fact]
        public void Add_pos_same_length_some_carry()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0x12345678, 0x12345678, 0xFFFFFFFF });
            BigInteger y = new BigInteger(1, new uint[] { 0x23456789, 0x13243546, 0x11111111 });
            BigInteger z = x.Add(y);

            Assert.True(SameValue(z, 1, new uint[] { 0x3579BE01, 0x25588BBF, 0x11111110 }));
        }

        [Fact]
        public void Add_neg_same_length_some_carry()
        {
            BigInteger x = new BigInteger(-1, new uint[] { 0x12345678, 0x12345678, 0xFFFFFFFF });
            BigInteger y = new BigInteger(-1, new uint[] { 0x23456789, 0x13243546, 0x11111111 });
            BigInteger z = x.Add(y);

            Assert.True(SameValue(z, -1, new uint[] { 0x3579BE01, 0x25588BBF, 0x11111110 }));
        }


        [Fact]
        public void Add_pos_first_longer_one_carry()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0x12345678, 0x12345678, 0xFFFFFFFF, 0x22222222 });
            BigInteger y = new BigInteger(1, new uint[] { 0x11111111, 0x11111111 });
            BigInteger z = x.Add(y);

            Assert.True(SameValue(z, 1, new uint[] { 0x12345678, 0x12345679, 0x11111110, 0x33333333 }));
        }


        [Fact]
        public void Add_pos_first_longer_more_carry()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0x12345678, 0x12345678, 0xFFFFFFFF, 0xFFFFFFFF, 0x22222222 });
            BigInteger y = new BigInteger(1, new uint[] { 0x11111111, 0x11111111 });
            BigInteger z = x.Add(y);

            Assert.True(SameValue(z, 1, new uint[] { 0x12345678, 0x12345679, 0x00000000, 0x11111110, 0x33333333 }));
        }

        [Fact]
        public void Add_pos_first_longer_carry_extend()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x22222222 });
            BigInteger y = new BigInteger(1, new uint[] { 0x11111111, 0x11111111 });
            BigInteger z = x.Add(y);

            Assert.True(SameValue(z, 1, new uint[] { 0x00000001, 0x00000000, 0x00000000, 0x11111110, 0x33333333 }));
        }

        [Fact]
        public void Add_pos_neg_first_larger_mag()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0x5 });
            BigInteger y = new BigInteger(-1, new uint[] { 0x3 });
            BigInteger z = x.Add(y);
            Assert.True(SameValue(z, 1, new uint[] { 0x2 }));
        }


        [Fact]
        public void Add_pos_neg_second_larger_mag()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0x3 });
            BigInteger y = new BigInteger(-1, new uint[] { 0x5 });
            BigInteger z = x.Add(y);
            Assert.True(SameValue(z, -1, new uint[] { 0x2 }));
        }


        [Fact]
        public void Add_pos_neg_same_mag()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0x3 });
            BigInteger y = new BigInteger(-1, new uint[] { 0x3 });
            BigInteger z = x.Add(y);
            Assert.True(z.IsZero);
        }


        [Fact]
        public void Add_neg_pos_first_larger_mag()
        {
            BigInteger x = new BigInteger(-1, new uint[] { 0x5 });
            BigInteger y = new BigInteger(1, new uint[] { 0x3 });
            BigInteger z = x.Add(y);
            Assert.True(SameValue(z, -1, new uint[] { 0x2 }));
        }


        [Fact]
        public void Add_neg_pos_second_larger_mag()
        {
            BigInteger x = new BigInteger(-1, new uint[] { 0x3 });
            BigInteger y = new BigInteger(1, new uint[] { 0x5 });
            BigInteger z = x.Add(y);
            Assert.True(SameValue(z, 1, new uint[] { 0x2 }));
        }


        [Fact]
        public void Add_neg_pos_same_mag()
        {
            BigInteger x = new BigInteger(-1, new uint[] { 0x3 });
            BigInteger y = new BigInteger(1, new uint[] { 0x3 });
            BigInteger z = x.Add(y);
            Assert.True(z.IsZero);
        }

        [Fact]
        public void Add_zero_to_pos()
        {
            BigInteger x = new BigInteger(0, new uint[0]);
            BigInteger y = new BigInteger(1, new uint[] { 0x3 });
            BigInteger z = x.Add(y);
            Assert.Equal(y, z);
        }


        [Fact]
        public void Subtract_zero_yields_this()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0x12345678, 0x12345678, 0xFFFFFFFF, 0xFFFFFFFF, 0x22222222 });
            BigInteger y = new BigInteger(0, new uint[0]);
            BigInteger z = x.Subtract(y);

            Assert.Equal(x, z);
        }

        [Fact]
        public void Subtract_from_zero_yields_negation()
        {
            BigInteger x = new BigInteger(0, new uint[0]);
            BigInteger y = new BigInteger(1, new uint[] { 0x12345678, 0x12345678, 0xFFFFFFFF, 0xFFFFFFFF, 0x22222222 });
            BigInteger z = x.Subtract(y);

            Assert.True(SameValue(z, -1, y.GetMagnitude()));
        }

        [Fact]
        public void Subtract_opposite_sign_first_pos_is_add()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x22222222 });
            BigInteger y = new BigInteger(-1, new uint[] { 0x11111111, 0x11111111 });
            BigInteger z = x.Subtract(y);

            Assert.True(SameValue(z, 1, new uint[] { 0x00000001, 0x00000000, 0x00000000, 0x11111110, 0x33333333 }));
        }

        [Fact]
        public void Subtract_opposite_sign_first_neg_is_add()
        {
            BigInteger x = new BigInteger(-1, new uint[] { 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x22222222 });
            BigInteger y = new BigInteger(1, new uint[] { 0x11111111, 0x11111111 });
            BigInteger z = x.Subtract(y);

            Assert.True(SameValue(z, -1, new uint[] { 0x00000001, 0x00000000, 0x00000000, 0x11111110, 0x33333333 }));
        }

        [Fact]
        public void Subtract_equal_pos_is_zero()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x22222222 });
            BigInteger y = new BigInteger(1, new uint[] { 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x22222222 });
            BigInteger z = x.Subtract(y);

            Assert.True(z.IsZero);
        }

        [Fact]
        public void Subtract_equal_neg_is_zero()
        {
            BigInteger x = new BigInteger(-1, new uint[] { 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x22222222 });
            BigInteger y = new BigInteger(-1, new uint[] { 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x22222222 });
            BigInteger z = x.Subtract(y);

            Assert.True(z.IsZero);
        }

        [Fact]
        public void Subtract_both_pos_first_larger_no_borrow()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0x12345678, 0x12345678, 0xFFFFFFFF, 0x33333333 });
            BigInteger y = new BigInteger(1, new uint[] { 0x11111111, 0x11111111 });
            BigInteger z = x.Subtract(y);

            Assert.True(SameValue(z, 1, new uint[] { 0x12345678, 0x12345678, 0xEEEEEEEE, 0x22222222 }));
        }

        [Fact]
        public void Subtract_both_pos_first_smaller_no_borrow()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0x11111111, 0x11111111 });
            BigInteger y = new BigInteger(1, new uint[] { 0x12345678, 0x12345678, 0xFFFFFFFF, 0x33333333 });
            BigInteger z = x.Subtract(y);

            Assert.True(SameValue(z, -1, new uint[] { 0x12345678, 0x12345678, 0xEEEEEEEE, 0x22222222 }));
        }

        [Fact]
        public void Subtract_both_neg_first_larger_no_borrow()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0x12345678, 0x12345678, 0xFFFFFFFF, 0x33333333 });
            BigInteger y = new BigInteger(1, new uint[] { 0x11111111, 0x11111111 });
            BigInteger z = x.Subtract(y);

            Assert.True(SameValue(z, 1, new uint[] { 0x12345678, 0x12345678, 0xEEEEEEEE, 0x22222222 }));
        }

        [Fact]
        public void Subtract_both_neg_first_smaller_no_borrow()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0x11111111, 0x11111111 });
            BigInteger y = new BigInteger(1, new uint[] { 0x12345678, 0x12345678, 0xFFFFFFFF, 0x33333333 });
            BigInteger z = x.Subtract(y);

            Assert.True(SameValue(z, -1, new uint[] { 0x12345678, 0x12345678, 0xEEEEEEEE, 0x22222222 }));
        }

        [Fact]
        public void Subtract_both_pos_first_larger_some_borrow()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0x12345678, 0x12345678, 0x00000000, 0x03333333 });
            BigInteger y = new BigInteger(1, new uint[] { 0x11111111, 0x11111111 });
            BigInteger z = x.Subtract(y);

            Assert.True(SameValue(z, 1, new uint[] { 0x12345678, 0x12345677, 0xEEEEEEEE, 0xF2222222 }));
        }

        [Fact]
        public void Subtract_both_pos_first_larger_lose_MSB()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0x12345678, 0x12345678, 0x00000000, 0x33333333 });
            BigInteger y = new BigInteger(1, new uint[] { 0x12345678, 0x12345676, 0x00000000, 0x44444444 });
            BigInteger z = x.Subtract(y);

            Assert.True(SameValue(z, 1, new uint[] { 0x1, 0xFFFFFFFF, 0xEEEEEEEF }));
        }


        [Fact]
        public void Subtract_both_pos_first_larger_lose_several_MSB()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0x12345678, 0x12345678, 0x12345678, 0x00000000, 0x33333333 });
            BigInteger y = new BigInteger(1, new uint[] { 0x12345678, 0x12345678, 0x12345676, 0x00000000, 0x44444444 });
            BigInteger z = x.Subtract(y);

            Assert.True(SameValue(z, 1, new uint[] { 0x1, 0xFFFFFFFF, 0xEEEEEEEF }));
        }


        [Fact]
        public void Abs_zero_is_zero()
        {
            BigInteger z = new BigInteger(0);
            Assert.True(z.Abs().IsZero);
        }

        [Fact]
        public void Abs_pos_is_pos()
        {
            uint[] data = new uint[] { 0x1, 0x2, 0x3 };
            BigInteger i = new BigInteger(1, data);
            Assert.True(SameValue(i.Abs(), 1, data));
        }

        [Fact]
        public void Abs_neg_is_pos()
        {
            uint[] data = new uint[] { 0x1, 0x2, 0x3 };
            BigInteger i = new BigInteger(-1, data);
            Assert.True(SameValue(i.Abs(), 1, data));
        }

        #endregion

        #region Multiplication

        [Fact]
        public void Mult_x_by_zero_is_zero()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0x12345678 });
            BigInteger y = new BigInteger(0, new uint[0]);
            BigInteger z = x.Multiply(y);
            Assert.True(z.IsZero);
        }

        [Fact]
        public void Mult_zero_by_y_is_zero()
        {
            BigInteger x = new BigInteger(0, new uint[0]);
            BigInteger y = new BigInteger(1, new uint[] { 0x12345678 });
            BigInteger z = x.Multiply(y);
            Assert.True(z.IsZero);
        }

        [Fact]
        public void Mult_two_pos_is_pos()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0xDEFCBA98 });
            BigInteger y = new BigInteger(1, new uint[] { 0x12345678 });
            BigInteger z = x.Multiply(y);
            Assert.True(z.IsPositive);
        }

        [Fact]
        public void Mult_two_neg_is_pos()
        {
            BigInteger x = new BigInteger(-1, new uint[] { 0xDEFCBA98 });
            BigInteger y = new BigInteger(-1, new uint[] { 0x12345678 });
            BigInteger z = x.Multiply(y);
            Assert.True(z.IsPositive);
        }

        [Fact]
        public void Mult_pos_neg_is_neg()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0xDEFCBA98 });
            BigInteger y = new BigInteger(-1, new uint[] { 0x12345678 });
            BigInteger z = x.Multiply(y);
            Assert.True(z.IsNegative);
        }

        [Fact]
        public void Mult_neg_pos_is_neg()
        {
            BigInteger x = new BigInteger(-1, new uint[] { 0xDEFCBA98 });
            BigInteger y = new BigInteger(1, new uint[] { 0x12345678 });
            BigInteger z = x.Multiply(y);
            Assert.True(z.IsNegative);
        }

        [Fact]
        public void Mult_1()
        {
            BigInteger x = new BigInteger(1, new uint[] { 100 });
            BigInteger y = new BigInteger(1, new uint[] { 200 });
            BigInteger z = x.Multiply(y);
            Assert.True(SameValue(z, 1, new uint[] { 20000 }));
        }

        [Fact]
        public void Mult_2()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0xFFFFFFFF, 0xF0000000 });
            BigInteger y = new BigInteger(1, new uint[] { 0x2 });
            BigInteger z = x.Multiply(y);
            Assert.True(SameValue(z, 1, new uint[] { 0x1, 0xFFFFFFFF, 0xE0000000 }));
        }

        [Fact]
        public void Mult_3()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF });
            BigInteger y = new BigInteger(1, new uint[] { 0x1, 0x1 });
            BigInteger z = x.Multiply(y);
            Assert.True(SameValue(z, 1, new uint[] { 0x1, 0x0, 0xFFFFFFFF, 0xFFFFFFFE, 0xFFFFFFFF }));
        }

        #endregion

        #region Division

        // We test some of the internals, too.
        [Fact]
        public void Normalize_shifts_happens_different_len()
        {
            uint[] x = new uint[] { 0x8421FEC8, 0xFE62F731 };
            uint[] xn = new uint[3];

            BigInteger.Normalize(xn, 3, x, 2, 0);
            Assert.Equal(x[1], xn[2]);
            Assert.Equal(x[0], xn[1]);
            Assert.Equal(0U, xn[0]);

            for (int i = 1; i < 32; i++)
            {
                int rshift = 32 - i;
                BigInteger.Normalize(xn, 3, x, 2, i);
                Assert.Equal(x[1] << i, xn[2]);
                Assert.Equal(x[0] << i | x[1] >> rshift, xn[1]);
                Assert.Equal(x[0] >> rshift, xn[0]);
            }
        }

        [Fact]
        public void Normalize_shifts_happens_same_len()
        {
            uint[] x = new uint[] { 0x0421FEC8, 0xFE62F731 };
            uint[] xn = new uint[2];

            BigInteger.Normalize(xn, 2, x, 2, 0);
            Assert.Equal(x[1], xn[1]);
            Assert.Equal(x[0], xn[0]);

            for (int i = 1; i < 5; i++)
            {
                int rshift = 32 - i;
                BigInteger.Normalize(xn, 2, x, 2, i);
                Assert.Equal(x[1] << i, xn[1]);
                Assert.Equal(x[0] << i | x[1] >> rshift, xn[0]);
            }
        }

        [Fact]
        public void Normalize_shifts_over_left_end_throws()
        {
            uint[] x = new uint[] { 0x0421FEC8, 0xFE62F731 };
            uint[] xn = new uint[2];

            Assert.Throws<InvalidOperationException>(() => BigInteger.Normalize(xn, 2, x, 2, 8));
        }

        [Fact]
        public void Divide_by_zero_throws()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0xFFFFFFFF });
            BigInteger y = new BigInteger(0, new uint[0]);
            Assert.Throws<DivideByZeroException>(() => x.Divide(y));
        }

        [Fact]
        public void Divide_into_zero_is_zero()
        {
            BigInteger y = new BigInteger(1, new uint[] { 0x1234, 0xabcd });
            BigInteger q = new BigInteger(0, new uint[0]);
            BigInteger r = new BigInteger(0, new uint[0]);
            TestDivRem(y, q, r);
        }

        [Fact]
        public void Divide_into_smaller_is_zero_plus_remainder_is_dividend()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0x12345678, 0xabcdef23, 0x88776654 });
            BigInteger y = new BigInteger(1, new uint[] { 0x12345678, 0xabcdef23, 0x88776655 });
            BigInteger q;
            BigInteger r;
            q = x.DivRem(y, out r);
            Assert.True(r == x);
            Assert.True(q.IsZero);
        }

        // This is because the code had a fall-through error due to a missing return statement.
        [Fact]
        public void Divide_into_smaller_is_zero_plus_remainder_is_dividend_len_difference()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0x12345678 });
            BigInteger y = new BigInteger(1, new uint[] { 0x12345678, 0xabcdef23, 0x88776655 });
            BigInteger q;
            BigInteger r;
            q = x.DivRem(y, out r);
            Assert.True(r == x);
            Assert.True(q.IsZero);
        }

        [Fact]
        public void Divide_same_on_len_1()
        {
            BigInteger y = new BigInteger(1, new uint[] { 0x12345678 });
            BigInteger q = new BigInteger(1, new uint[] { 0x1 });
            BigInteger r = new BigInteger(0, new uint[0]);

            TestDivRem(y, q, r);
        }

        [Fact]
        public void Divide_same_on_len_3()
        {
            BigInteger y = new BigInteger(1, new uint[] { 0x12345678, 0xabcdef23, 0x88776655 });
            BigInteger q = new BigInteger(1, new uint[] { 0x1 });
            BigInteger r = new BigInteger(0, new uint[0]);

            TestDivRem(y, q, r);
        }

        [Fact]
        public void Divide_same_except_small_remainder()
        {
            BigInteger y = new BigInteger(1, new uint[] { 0x12345678 });
            BigInteger q = new BigInteger(1, new uint[] { 0x1 });
            BigInteger r = new BigInteger(1, new uint[] { 0x45 });

            TestDivRem(y, q, r);
        }

        [Fact]
        public void Divide_same_except_small_remainder_2()
        {
            BigInteger y = new BigInteger(1, new uint[] { 0x12345678, 0xabcdef23, 0x88776655 });
            BigInteger q = new BigInteger(1, new uint[] { 0x1 });
            BigInteger r = new BigInteger(1, new uint[] { 0x45 });

            TestDivRem(y, q, r);
        }


        [Fact]
        public void Divide_two_digits_with_small_remainder_no_shift()
        {
            BigInteger y = new BigInteger(1, new uint[] { 0xFF000000 });
            BigInteger q = new BigInteger(1, new uint[] { 0x1, 0x1, 0x1 });
            BigInteger r = new BigInteger(1, new uint[] { 0xab });

            TestDivRem(y, q, r);
        }


        [Fact]
        public void Divide_two_digits_with_small_remainder_no_shift2()
        {
            BigInteger y = new BigInteger(1, new uint[] { 0xFF000000, 0x000000aa });
            BigInteger q = new BigInteger(1, new uint[] { 0x1, 0x1, 0x1 });
            BigInteger r = new BigInteger(1, new uint[] { 0xab, 0x45 });

            TestDivRem(y, q, r);
        }

        [Fact]
        public void Divide_two_digits_with_small_remainder_small_shift()
        {
            BigInteger y = new BigInteger(1, new uint[] { 0x0F000000, 0x000000aa });
            BigInteger q = new BigInteger(1, new uint[] { 0x1, 0x1, 0x1 });
            BigInteger r = new BigInteger(1, new uint[] { 0xab, 0x45 });

            TestDivRem(y, q, r);
        }


        // THe following is taken from a suggestion in Knuth.
        // In Radix t,
        //  (t^m - 1)*(t^n - 1) has expansion
        //    (t-1) ... (t-1) (t-2) (t-1) ... (t-1) 0 ... 0 1
        //    --------------------- --------------- --------
        //         m-1 places         n-m places    m-1 places
        //
        //  if m < n
        //
        //  Of course, (t^m -1) is (t-1)  repeated m-1 places.
        //
        //  So, we can construct lots of cute little test samples

        private void GenerateKnuthExample(int m, int n, out BigInteger bmn, out BigInteger bm, out BigInteger bn)
        {
            if (m >= n)
                throw new InvalidOperationException("m must be less than n");

            uint[] bmnArray = new uint[m + n];
            uint[] bmArray = new uint[m];
            uint[] bnArray = new uint[n];

            for (int i = 0; i < m; i++)
                bmArray[i] = 0xFFFFFFFF;

            for (int i = 0; i < n; i++)
                bnArray[i] = 0xFFFFFFFF;

            for (int i = 0; i < m - 1; i++)
                bmnArray[i] = 0xFFFFFFFF;

            bmnArray[m - 1] = 0xFFFFFFFE;

            for (int i = 0; i < n - m; i++)
                bmnArray[m + i] = 0xFFFFFFFF;

            for (int i = 0; i < m - 2; i++)
                bmnArray[n + i] = 0;

            bmnArray[m + n - 1] = 1;

            bmn = new BigInteger(1, bmnArray);
            bm = new BigInteger(1, bmArray);
            bn = new BigInteger(1, bnArray);
        }


        [Fact]
        public void TestKnuthExamples()
        {
            BigInteger bm;
            BigInteger bn;
            BigInteger bmn;

            for (int m = 2; m < 5; m++)
                for (int n = m + 1; n < m + 5; n++)
                {
                    GenerateKnuthExample(m, n, out bmn, out bm, out bn);

                    BigInteger add = bm - new BigInteger(1, new uint[] { 0xabcd });
                    BigInteger x = bmn + add;

                    BigInteger q;
                    BigInteger r;

                    q = x.DivRem(bm, out r);
                    Assert.True(r == add);
                    Assert.True(q == bn);
                }
        }

        private void TestDivRem(BigInteger y, BigInteger mult, BigInteger add)
        {
            BigInteger x = y * mult + add;

            BigInteger q;
            BigInteger r;
            q = x.DivRem(y, out r);

            Assert.True(q == mult);
            Assert.True(r == add);
        }


        #endregion

        #region Power, ModPower tests

        [Fact]
        public void Power_on_negative_exponent_fails()
        {
            BigInteger i = new BigInteger(1, 0x1);
            Assert.Throws<ArgumentOutOfRangeException>(() => i.Power(-2));
        }

        [Fact]
        public void Power_with_exponent_0_is_one()
        {
            BigInteger i = new BigInteger(1, 0x1);
            Assert.True(SameValue(i.Power(0), 1, new uint[] { 1 }));
        }

        [Fact]
        public void Power_on_zero_is_zero()
        {
            BigInteger z = BigInteger.Create(0);
            Assert.True(z.Power(12).IsZero);
        }

        [Fact]
        public void Power_on_small_exponent_works()
        {
            BigInteger i = BigInteger.Create(3);
            BigInteger p = i.Power(6);
            BigInteger e = BigInteger.Create(729);
            Assert.True(p == e);
        }

        [Fact]
        public void Power_on_completely_odd_exponent_works()
        {
            BigInteger i = new BigInteger(1, 0x2);
            BigInteger p = i.Power(255);
            BigInteger p2_to_255 = new BigInteger(1,
                0x80000000, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0);
            Assert.True(p == p2_to_255);
        }


        [Fact]
        public void Power_on_power_of_two_exponent_works()
        {
            BigInteger i = new BigInteger(1, 0x2);
            BigInteger p = i.Power(256);
            BigInteger p2_to_256 = new BigInteger(1,
                0x1, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0);
            Assert.True(p == p2_to_256);
        }

        [Fact]
        public void ModPow_on_negative_exponent_fails()
        {
            BigInteger i = new BigInteger(1, 0x1);
            BigInteger m = new BigInteger(1, 0x1);
            Assert.Throws<ArgumentOutOfRangeException>(() => i.ModPow(-2, m));
        }

        [Fact]
        public void ModPow_with_exponent_0_is_one()
        {
            BigInteger i = new BigInteger(1, 0x1);
            BigInteger m = new BigInteger(1, 0x1);
            Assert.True(SameValue(i.ModPow(0, m), 1, new uint[] { 1 }));
        }

        [Fact]
        public void ModPow_on_zero_is_zero()
        {
            BigInteger z = BigInteger.Create(0);
            BigInteger m = new BigInteger(1, 0x1);
            Assert.True(z.ModPow(12, m).IsZero);
        }

        [Fact]
        public void ModPow_on_small_exponent_works()
        {
            BigInteger i = BigInteger.Create(3);
            BigInteger m = BigInteger.Create(100);
            BigInteger e = BigInteger.Create(6);
            BigInteger p = i.ModPow(e, m);
            BigInteger a = BigInteger.Create(29);
            Assert.True(p == a);
        }

        [Fact]
        public void ModPow_on_completely_odd_exponent_works()
        {
            BigInteger i = new BigInteger(1, 0x2);
            BigInteger m = new BigInteger(1, 0x7);
            BigInteger e = BigInteger.Create(255);
            BigInteger p = i.ModPow(e, m);
            BigInteger a = new BigInteger(1, 0x1);
            Assert.True(p == a);
        }


        [Fact]
        public void ModPow_on_power_of_two_exponent_works()
        {
            BigInteger i = new BigInteger(1, 0x2);
            BigInteger m = new BigInteger(1, 0x7);
            BigInteger e = BigInteger.Create(256);
            BigInteger p = i.ModPow(e, m);
            BigInteger a = new BigInteger(1, 0x2);
            Assert.True(p == a);
        }


        #endregion

        #region misc tests

        [Fact]
        public void IsOddWorks()
        {
            BigInteger x1 = new BigInteger(0, new uint[] { 0x0 });
            BigInteger x2 = new BigInteger(1, new uint[] { 0x1 });
            BigInteger x3 = new BigInteger(-1, new uint[] { 0x1 });
            BigInteger x4 = new BigInteger(1, new uint[] { 0x2 });
            BigInteger x5 = new BigInteger(-1, new uint[] { 0x2 });
            BigInteger x6 = new BigInteger(1, new uint[] { 0xFFFFFFF0 });
            BigInteger x7 = new BigInteger(-1, new uint[] { 0xFFFFFFF0 });
            BigInteger x8 = new BigInteger(1, new uint[] { 0xFFFFFFF1 });
            BigInteger x9 = new BigInteger(-1, new uint[] { 0xFFFFFFF1 });
            BigInteger x10 = new BigInteger(1, new uint[] { 0x1, 0x2 });
            BigInteger x11 = new BigInteger(1, new uint[] { 0x2, 0x1 });
            BigInteger x12 = new BigInteger(1, new uint[] { 0x2, 0x2 });
            BigInteger x13 = new BigInteger(1, new uint[] { 0x1, 0x1 });

            Assert.False(x1.IsOdd);
            Assert.True(x2.IsOdd);
            Assert.True(x3.IsOdd);
            Assert.False(x4.IsOdd);
            Assert.False(x5.IsOdd);
            Assert.False(x6.IsOdd);
            Assert.False(x7.IsOdd);
            Assert.True(x8.IsOdd);
            Assert.True(x9.IsOdd);
            Assert.False(x10.IsOdd);
            Assert.True(x11.IsOdd);
            Assert.False(x12.IsOdd);
            Assert.True(x13.IsOdd);
        }

        #endregion

        #region GCD tests

        [Fact]
        public void GDC_simple_case()
        {
            int[] primes = { 2, 3 };
            int[] apowers = { 2, 5 };
            int[] bpowers = { 4, 3 };

            TestGDC(primes, true, apowers, true, bpowers);
        }

        [Fact]
        public void GDC_test_signs()
        {
            int[] primes = { 2, 3, 5, 17, 73 };
            int[] apowers = { 50, 10, 12, 4, 0 };
            int[] bpowers = { 40, 20, 6, 0, 5 };
            TestGDC(primes, true, apowers, true, bpowers);
            TestGDC(primes, true, apowers, false, bpowers);
            TestGDC(primes, false, apowers, true, bpowers);
            TestGDC(primes, false, apowers, false, bpowers);
        }


        [Fact]
        public void GDC_test_disparity_in_size()
        {
            int[] primes = { 2, 3, 5, 17, 73 };
            int[] apowers = { 50, 100, 120, 4, 0 };
            int[] bpowers = { 5, 20, 6, 30, 5 };
            TestGDC(primes, true, apowers, true, bpowers);
        }

        [Fact]
        public void GDC_test_many_powers_of_two()
        {
            int[] primes = { 2, 3, 5, 17, 73 };
            int[] apowers = { 500, 100, 120, 4, 0 };
            int[] bpowers = { 499, 20, 6, 30, 5 };
            TestGDC(primes, true, apowers, true, bpowers);
        }

        [Fact]
        public void GDC_test_relatively_prime()
        {
            int[] primes = { 2, 3, 5, 17, 73 };
            int[] apowers = { 500, 0, 120, 0, 0 };
            int[] bpowers = { 0, 120, 0, 130, 5 };
            TestGDC(primes, true, apowers, true, bpowers);
        }

        [Fact]
        public void GDC_test_multiple()
        {
            int[] primes = { 2, 3, 5, 17, 73 };
            int[] apowers = { 500, 50, 120, 10, 12 };
            int[] bpowers = { 0, 30, 20, 10, 10 };
            TestGDC(primes, true, apowers, true, bpowers);
        }


        void TestGDC(int[] primes, bool apos, int[] apowers, bool bpos, int[] bpowers)
        {
            BigInteger a = CreateFromPrimePowers(primes, apowers);
            if (!apos)
                a = a.Negate();

            BigInteger b = CreateFromPrimePowers(primes, bpowers);
            if (!bpos)
                b = b.Negate();

            BigInteger c = CreateFromPrimePowers(primes, MinPowers(apowers, bpowers));
            BigInteger g = a.Gcd(b);
            Assert.True(g == c);
        }

        BigInteger CreateFromPrimePowers(int[] primes, int[] powers)
        {
            BigInteger a = BigInteger.One;

            for (int i = 0; i < primes.Length; i++)
                if (powers[i] > 0)
                    a *= BigInteger.Create(primes[i]).Power(powers[i]);
            return a;
        }

        int[] MinPowers(int[] apowers, int[] bpowers)
        {
            int[] mins = new int[apowers.Length];
            for (int i = 0; i < apowers.Length; i++)
                mins[i] = Math.Min(apowers[i], bpowers[i]);
            return mins;
        }


        #endregion

        #region Bitwise operation tests -- Boolean ops

        [Fact]
        public void BitAnd_pos_pos()
        {
            uint digit1 = 0xACACACAC;
            uint digit2 = 0xCACACACA;

            BigInteger x = new BigInteger(1, new uint[] { digit1, digit2, digit1, digit2 });
            BigInteger y = new BigInteger(1, new uint[] { digit1, digit2, 0 });

            uint d0 = digit2 & digit1;
            uint d1 = digit1 & digit2;
            uint d2 = 0;

            BigInteger z = new BigInteger(1, new uint[] { d0, d1, d2 });
            //BigInteger z = new BigInteger(1, new uint[] { digit2 & digit1, digit1 & digit2, 0 });
            BigInteger w = x & y;

            Assert.True(w == z);
        }

        [Fact]
        public void BitAnd_pos_neg()
        {
            uint digit1 = 0xACACACAC;
            uint digit2 = 0xCACACACA;

            BigInteger x = new BigInteger(1, new uint[] { digit1, digit2, digit1, digit2 });
            BigInteger y = new BigInteger(-1, new uint[] { digit1, digit2, 0 });

            uint d0 = digit1;
            uint d1 = digit2 & ~digit1;
            uint d2 = digit1 & (~digit2 + 1);
            uint d3 = 0;

            BigInteger z = new BigInteger(1, new uint[] { d0, d1, d2, d3 });
            //BigInteger z = new BigInteger(1, new uint[] { digit1, digit2 & ~digit1, digit1 & (~digit2 + 1), 0 });
            BigInteger w = x & y;

            Assert.True(w == z);
        }

        [Fact]
        public void BitAnd_neg_pos()
        {
            uint digit1 = 0xACACACAC;
            uint digit2 = 0xCACACACA;

            BigInteger x = new BigInteger(-1, new uint[] { digit1, digit2, digit1, digit2 });
            BigInteger y = new BigInteger(1, new uint[] { digit1, digit2, 0 });

            uint d0 = ~digit2 & digit1;
            uint d1 = ~digit1 & digit2;
            uint d2 = 0;

            BigInteger z = new BigInteger(1, new uint[] { d0, d1, d2 });
            //BigInteger z = new BigInteger(1, new uint[] { ~digit2 & digit1, ~digit1 & digit2, 0 });
            BigInteger w = x & y;

            Assert.True(w == z);
        }

        [Fact]
        public void BitAnd_neg_neg()
        {
            uint digit1 = 0xACACACAC;
            uint digit2 = 0xCACACACA;

            BigInteger x = new BigInteger(-1, new uint[] { digit1, digit2, digit1, digit2 });
            BigInteger y = new BigInteger(-1, new uint[] { digit1, digit2, 0 });

            uint d0 = digit1;
            uint d1 = ~(~digit2 & ~digit1);
            uint d2 = ~(~digit1 & (~digit2 + 1)) + 1;
            uint d3 = 0;

            BigInteger z = new BigInteger(-1, new uint[] { d0, d1, d2, d3 });
            BigInteger w = x & y;

            Assert.True(w == z);
        }

        [Fact]
        public void BitOr_pos_pos()
        {
            uint digit1 = 0xACACACAC;
            uint digit2 = 0xCACACACA;

            BigInteger x = new BigInteger(1, new uint[] { digit1, digit2, digit1, digit2 });
            BigInteger y = new BigInteger(1, new uint[] { digit1, digit2, 0 });

            uint d0 = digit1;
            uint d1 = digit2 | digit1;
            uint d2 = digit1 | digit2;
            uint d3 = digit2;

            BigInteger z = new BigInteger(1, new uint[] { d0, d1, d2, d3 });
            BigInteger w = x | y;

            Assert.True(w == z);
        }

        [Fact]
        public void BitOr_pos_neg()
        {
            uint digit1 = 0xACACACAC;
            uint digit2 = 0xCACACACA;

            BigInteger x = new BigInteger(1, new uint[] { digit1, digit2, digit1, digit2 });
            BigInteger y = new BigInteger(-1, new uint[] { digit1, digit2, 0 });

            uint d0 = ~(digit2 | ~digit1);
            uint d1 = ~(digit1 | (~digit2 + 1));
            uint d2 = ~digit2 + 1;

            BigInteger z = new BigInteger(-1, new uint[] { d0, d1, d2 });
            BigInteger w = x | y;

            Assert.True(w == z);
        }

        [Fact]
        public void BitOr_neg_pos()
        {
            uint digit1 = 0xACACACAC;
            uint digit2 = 0xCACACACA;

            BigInteger x = new BigInteger(-1, new uint[] { digit1, digit2, digit1, digit2 });
            BigInteger y = new BigInteger(1, new uint[] { digit1, digit2, 0 });

            uint d0 = digit1;
            uint d1 = ~(~digit2 | digit1);
            uint d2 = ~(~digit1 | digit2);
            uint d3 = ~(~digit2 + 1) + 1;

            BigInteger z = new BigInteger(-1, new uint[] { d0, d1, d2, d3 });
            BigInteger w = x | y;

            Assert.True(w == z);
        }

        [Fact]
        public void BitOr_neg_neg()
        {
            uint digit1 = 0xACACACAC;
            uint digit2 = 0xCACACACA;

            BigInteger x = new BigInteger(-1, new uint[] { digit1, digit2, digit1, digit2 });
            BigInteger y = new BigInteger(-1, new uint[] { digit1, digit2, 0 });

            uint d0 = ~(~digit2 | ~digit1);
            uint d1 = ~(~digit1 | ~digit2);
            uint d2 = ~(~digit2 + 1) + 1;

            BigInteger z = new BigInteger(-1, new uint[] { d0, d1, d2 });
            BigInteger w = x | y;

            Assert.True(w == z);
        }

        [Fact]
        public void BitXor_pos_pos()
        {
            uint digit1 = 0xACACACAC;
            uint digit2 = 0xCACACACA;

            BigInteger x = new BigInteger(1, new uint[] { digit1, digit2, digit1, digit2 });
            BigInteger y = new BigInteger(1, new uint[] { digit1, digit2, 0 });

            uint d0 = ~(digit1 ^ 0x0);
            uint d1 = ~(digit2 ^ digit1);
            uint d2 = ~(digit1 ^ digit2);
            uint d3 = ~(digit2 ^ 0x0) + 1;

            BigInteger z = new BigInteger(-1, new uint[] { d0, d1, d2, d3 });
            BigInteger w = x ^ y;

            Assert.True(w == z);
        }

        [Fact]
        public void BitXor_pos_neg()
        {
            uint digit1 = 0xACACACAC;
            uint digit2 = 0xCACACACA;

            BigInteger x = new BigInteger(1, new uint[] { digit1, digit2, digit1, digit2 });
            BigInteger y = new BigInteger(-1, new uint[] { digit1, digit2, 0 });

            uint d0 = digit1 ^ (~0x0u);
            uint d1 = digit2 ^ ~digit1;
            uint d2 = digit1 ^ (~digit2 + 1);
            uint d3 = digit2 ^ 0x0;

            BigInteger z = new BigInteger(1, new uint[] { d0, d1, d2, d3 });
            BigInteger w = x ^ y;

            Assert.True(w == z);
        }

        [Fact]
        public void BitXor_neg_pos()
        {
            uint digit1 = 0xACACACAC;
            uint digit2 = 0xCACACACA;

            BigInteger x = new BigInteger(-1, new uint[] { digit1, digit2, digit1, digit2 });
            BigInteger y = new BigInteger(1, new uint[] { digit1, digit2, 0 });

            uint d0 = ~digit1 ^ 0x0;
            uint d1 = ~digit2 ^ digit1;
            uint d2 = ~digit1 ^ digit2;
            uint d3 = (~digit2 + 1) ^ 0x0u;

            BigInteger z = new BigInteger(1, new uint[] { d0, d1, d2, d3 });
            BigInteger w = x ^ y;

            Assert.True(w == z);
        }

        [Fact]
        public void BitXor_neg_neg()
        {
            uint digit1 = 0xACACACAC;
            uint digit2 = 0xCACACACA;

            BigInteger x = new BigInteger(-1, new uint[] { digit1, digit2, digit1, digit2 });
            BigInteger y = new BigInteger(-1, new uint[] { digit1, digit2, 0 });

            uint d0 = ~(~digit1 ^ (~0x0u));
            uint d1 = ~(~digit2 ^ ~digit1);
            uint d2 = ~(~digit1 ^ (~digit2 + 1));
            uint d3 = ~((~digit2 + 1) ^ 0x0) + 1;

            BigInteger z = new BigInteger(-1, new uint[] { d0, d1, d2, d3 });
            BigInteger w = x ^ y;

            Assert.True(w == z);
        }


        [Fact]
        public void BitAndNot_pos_pos()
        {
            uint digit1 = 0xACACACAC;
            uint digit2 = 0xCACACACA;

            BigInteger x = new BigInteger(1, new uint[] { digit1, digit2, digit1, digit2 });
            BigInteger y = new BigInteger(1, new uint[] { digit1, digit2, 0 });

            uint d0 = digit1;
            uint d1 = digit2 & ~digit1;
            uint d2 = digit1 & ~digit2;
            uint d3 = digit2;

            BigInteger z = new BigInteger(1, new uint[] { d0, d1, d2, d3 });
            BigInteger w = x.BitwiseAndNot(y);

            Assert.True(w == z);
        }

        [Fact]
        public void BitAndNot_pos_neg()
        {
            uint digit1 = 0xACACACAC;
            uint digit2 = 0xCACACACA;

            BigInteger x = new BigInteger(1, new uint[] { digit1, digit2, digit1, digit2 });
            BigInteger y = new BigInteger(-1, new uint[] { digit1, digit2, 0 });

            uint d0 = digit2 & digit1;
            uint d1 = digit1 & ~(~digit2 + 1);
            uint d2 = digit2;

            BigInteger z = new BigInteger(1, new uint[] { d0, d1, d2 });
            BigInteger w = x.BitwiseAndNot(y);

            Assert.True(w == z);
        }

        [Fact]
        public void BitAndNot_neg_pos()
        {
            uint digit1 = 0xACACACAC;
            uint digit2 = 0xCACACACA;

            BigInteger x = new BigInteger(-1, new uint[] { digit1, digit2, digit1, digit2 });
            BigInteger y = new BigInteger(1, new uint[] { digit1, digit2, 0 });

            uint d0 = digit1;
            uint d1 = digit2 | digit1;
            uint d2 = digit1 | digit2;
            uint d3 = digit2;


            BigInteger z = new BigInteger(-1, new uint[] { d0, d1, d2, d3 });
            BigInteger w = x.BitwiseAndNot(y);

            Assert.True(w == z);
        }

        [Fact]
        public void BitAndNot_neg_neg()
        {
            uint digit1 = 0xACACACAC;
            uint digit2 = 0xCACACACA;

            BigInteger x = new BigInteger(-1, new uint[] { digit1, digit2, digit1, digit2 });
            BigInteger y = new BigInteger(-1, new uint[] { digit1, digit2, 0 });

            uint d0 = ~digit2 & digit1;
            uint d1 = ~digit1 & ~(~digit2 + 1);
            uint d2 = ~digit2 + 1;

            BigInteger z = new BigInteger(1, new uint[] { d0, d1, d2 });
            BigInteger w = x.BitwiseAndNot(y);

            Assert.True(w == z);
        }


        [Fact]
        public void BitNot_pos()
        {
            uint digit1 = 0xACACACAC;
            uint digit2 = 0xCACACACA;

            BigInteger x = new BigInteger(1, new uint[] { digit1, digit2, 0 });

            uint d0 = digit1;
            uint d1 = digit2;
            uint d2 = 1;

            BigInteger z = new BigInteger(-1, new uint[] { d0, d1, d2 });
            BigInteger w = x.OnesComplement();

            Assert.True(w == z);
        }

        void BitNot_neg()
        {
            uint digit1 = 0xACACACAC;
            uint digit2 = 0xCACACACA;

            BigInteger x = new BigInteger(-1, new uint[] { digit1, digit2, 0 });

            uint d0 = digit1;
            uint d1 = ~(~digit2 + 1);
            uint d2 = 0xFFFFFFFF;

            BigInteger z = new BigInteger(1, new uint[] { d0, d1, d2 });
            BigInteger w = x.OnesComplement();

            Assert.True(w == z);
        }

        #endregion

        #region Bitwise operation tests -- single bit

        [Fact]
        public void TestBit_pos_inside()
        {
            BigInteger x = new BigInteger(1, 0xAAAAAAAA, 0xAAAAAAAA);

            for (int i = 0; i < 64; i++)
                Assert.Equal(i % 2 != 0, x.TestBit(i));
        }

        [Fact]
        public void TestBit_neg_inside()
        {
            BigInteger x = new BigInteger(-1, 0xAAAAAAAA, 0xAAAAAAAA);

            Assert.False(x.TestBit(0));
            Assert.True(x.TestBit(1));

            for (int i = 2; i < 64; i++)
                Assert.Equal(i % 2 == 0, x.TestBit(i));
        }

        [Fact]
        public void TestBit_pos_outside()
        {
            BigInteger x = new BigInteger(1, 0xAAAAAAAA, 0xAAAAAAAA);
            Assert.False(x.TestBit(1000));
        }

        [Fact]
        public void TestBit_neg_outside()
        {
            BigInteger x = new BigInteger(-1, 0xAAAAAAAA, 0xAAAAAAAA);
            Assert.True(x.TestBit(1000));
        }

        [Fact]
        public void SetBit_pos_inside_initial_set()
        {
            BigInteger x = new BigInteger(1, 0xFFFF0000, 0xFFFF0000);
            BigInteger y = x.SetBit(56);
            Assert.Same(y, x);
        }

        [Fact]
        public void SetBit_pos_inside_initial_clear()
        {
            BigInteger x = new BigInteger(1, 0xFFFF0000, 0xFFFF0000);
            BigInteger w = new BigInteger(1, 0xFFFF0080, 0xFFFF0000);
            BigInteger y = x.SetBit(39);
            Assert.True(y == w);
        }

        [Fact]
        public void SetBit_pos_outside()
        {
            BigInteger x = new BigInteger(1, 0xFFFF0000, 0xFFFF0000);
            BigInteger y = x.SetBit(99);
            Assert.True(SameValue(y, 1, new uint[] { 8, 0, 0xFFFF0000, 0xFFFF0000 }));
        }

        [Fact]
        public void SetBit_neg_inside_initial_set()
        {
            BigInteger x = new BigInteger(-1, 0xFFFF0000, 0xFFFF0000);
            BigInteger y = x.SetBit(39);
            Assert.Same(y, x);
        }

        [Fact]
        public void SetBit_neg_inside_initial_clear()
        {
            BigInteger x = new BigInteger(-1, 0xFFFF0000, 0xFFFF0000);
            BigInteger w = new BigInteger(-1, 0xFEFF0000, 0xFFFF0000);
            BigInteger y = x.SetBit(56);
            Assert.True(y == w);
        }

        [Fact]
        public void SetBit_neg_outside()
        {
            BigInteger x = new BigInteger(-1, 0xFFFF0000, 0xFFFF0000);
            BigInteger y = x.SetBit(99);
            Assert.True(SameValue(y, -1, new uint[] { 0xFFFF0000, 0xFFFF0000 }));
        }

        [Fact]
        public void ClearBit_pos_inside_initial_set()
        {
            BigInteger x = new BigInteger(1, 0xFFFF0000, 0xFFFF0000);
            BigInteger w = new BigInteger(1, 0xFEFF0000, 0xFFFF0000);
            BigInteger y = x.ClearBit(56);
            Assert.True(y == w);
        }

        [Fact]
        public void ClearBit_pos_inside_initial_clear()
        {
            BigInteger x = new BigInteger(1, 0xFFFF0000, 0xFFFF0000);
            BigInteger y = x.ClearBit(39);
            Assert.Same(y, x);
        }

        [Fact]
        public void ClearBit_pos_outside()
        {
            BigInteger x = new BigInteger(1, 0xFFFF0000, 0xFFFF0000);
            BigInteger y = x.ClearBit(99);
            Assert.Same(y, x);
        }

        [Fact]
        public void ClearBit_neg_inside_initial_set()
        {
            BigInteger x = new BigInteger(-1, 0xFFFF0000, 0xFFFF0000);
            BigInteger y = x.ClearBit(39);
            Assert.True(SameValue(y, -1, new uint[] { 0xFFFF0080, 0xFFFF0000 }));
        }

        [Fact]
        public void ClearBit_neg_inside_initial_clear()
        {
            BigInteger x = new BigInteger(-1, 0xFFFF0000, 0xFFFF0000);
            BigInteger y = x.ClearBit(56);
            Assert.Same(y, x);
        }

        [Fact]
        public void ClearBit_neg_outside()
        {
            BigInteger x = new BigInteger(-1, 0xFFFF0000, 0xFFFF0000);
            BigInteger y = x.ClearBit(99);
            Assert.True(SameValue(y, -1, new uint[] { 8, 0, 0xFFFF0000, 0xFFFF0000 }));
        }

        [Fact]
        public void FlipBit_pos_inside_initial_set()
        {
            BigInteger x = new BigInteger(1, 0xFFFF0000, 0xFFFF0000);
            BigInteger w = new BigInteger(1, 0xFEFF0000, 0xFFFF0000);
            BigInteger y = x.FlipBit(56);
            Assert.True(y == w);
        }

        [Fact]
        public void FlipBit_pos_inside_initial_clear()
        {
            BigInteger x = new BigInteger(1, 0xFFFF0000, 0xFFFF0000);
            BigInteger w = new BigInteger(1, 0xFFFF0080, 0xFFFF0000);
            BigInteger y = x.FlipBit(39);
            Assert.True(y == w);
        }

        [Fact]
        public void FlipBit_pos_outside()
        {
            BigInteger x = new BigInteger(1, 0xFFFF0000, 0xFFFF0000);
            BigInteger y = x.FlipBit(99);
            Assert.True(SameValue(y, 1, new uint[] { 8, 0, 0xFFFF0000, 0xFFFF0000 }));
        }

        [Fact]
        public void FlipBit_neg_inside_initial_set()
        {
            BigInteger x = new BigInteger(-1, 0xFFFF0000, 0xFFFF0000);
            BigInteger w = new BigInteger(-1, 0xFFFF0080, 0xFFFF0000);
            BigInteger y = x.FlipBit(39);
            Assert.True(y == w);
        }

        [Fact]
        public void FlipBit_neg_inside_initial_clear()
        {
            BigInteger x = new BigInteger(-1, 0xFFFF0000, 0xFFFF0000);
            BigInteger w = new BigInteger(-1, 0xFEFF0000, 0xFFFF0000);
            BigInteger y = x.FlipBit(56);
            Assert.True(y == w);
        }

        [Fact]
        public void FlipBit_neg_outside()
        {
            BigInteger x = new BigInteger(-1, 0xFFFF0000, 0xFFFF0000);
            BigInteger y = x.FlipBit(99);
            Assert.True(SameValue(y, -1, new uint[] { 8, 0, 0xFFFF0000, 0xFFFF0000 }));
        }

        #endregion

        #region Bitwise operation tests -- shifts

        [Fact]
        public void LeftShift_zero_is_zero()
        {
            BigInteger x = new BigInteger(0);
            BigInteger y = x.LeftShift(1000);
            Assert.True(y.IsZero);
        }

        [Fact]
        public void LeftShift_neg_shift_same_as_right_shift()
        {
            uint digit1 = 0xC1F0F1CD;
            uint digit2 = 0xB38F4F83;
            uint digit3 = 0x1234678;
            BigInteger x = new BigInteger(1, digit1, digit2, digit3);
            BigInteger y = x.LeftShift(-40);
            BigInteger z = x.RightShift(40);
            Assert.True(y == z);
        }

        [Fact]
        public void LeftShift_zero_shift_is_this()
        {
            uint digit1 = 0xC1F0F1CD;
            uint digit2 = 0xB38F4F83;
            uint digit3 = 0x1234678;
            BigInteger x = new BigInteger(1, digit1, digit2, digit3);
            BigInteger y = x.LeftShift(0);
            Assert.Same(y, x);
        }

        void LeftShift_pos_whole_digit_shift_adds_zeros_at_end()
        {
            uint digit1 = 0xC1F0F1CD;
            uint digit2 = 0xB38F4F83;
            uint digit3 = 0x1234678;
            BigInteger x = new BigInteger(1, digit1, digit2, digit3);
            BigInteger y = x.LeftShift(64);
            BigInteger w = new BigInteger(1, digit1, digit2, digit3, 0, 0);
            Assert.True(y == w);
        }

        void LeftShift_neg_whole_digit_shift_adds_zeros_at_end()
        {
            uint digit1 = 0xC1F0F1CD;
            uint digit2 = 0xB38F4F83;
            uint digit3 = 0x1234678;
            BigInteger x = new BigInteger(-1, digit1, digit2, digit3);
            BigInteger y = x.LeftShift(64);
            BigInteger w = new BigInteger(-1, digit1, digit2, digit3, 0, 0);
            Assert.True(y == w);
        }


        void LeftShift_pos_small_shift()
        {
            uint digit1 = 0xC1F0F1CD;
            uint digit2 = 0xB38F4F83;
            uint digit3 = 0x1234678;
            BigInteger x = new BigInteger(1, digit1, digit2, digit3);
            BigInteger y = x.LeftShift(7);
            BigInteger w = new BigInteger(1,
                digit1 >> 25,
                digit1 << 7 | digit2 >> 25,
                digit2 << 7 | digit3 >> 25,
                digit3 << 7);
            Assert.True(y == w);
        }

        void LeftShift_neg_small_shift()
        {
            uint digit1 = 0xC1F0F1CD;
            uint digit2 = 0xB38F4F83;
            uint digit3 = 0x1234678;
            BigInteger x = new BigInteger(-1, digit1, digit2, digit3);
            BigInteger y = x.LeftShift(7);
            BigInteger w = new BigInteger(-1,
                digit1 >> 25,
                digit1 << 7 | digit2 >> 25,
                digit2 << 7 | digit3 >> 25,
                digit3 << 7);
            Assert.True(y == w);
        }


        void LeftShift_pos_big_shift()
        {
            uint digit1 = 0xC1F0F1CD;
            uint digit2 = 0xB38F4F83;
            uint digit3 = 0x1234678;
            BigInteger x = new BigInteger(1, digit1, digit2, digit3);
            BigInteger y = x.LeftShift(7 + 64);
            BigInteger w = new BigInteger(1,
                digit1 >> 25,
                digit1 << 7 | digit2 >> 25,
                digit2 << 7 | digit3 >> 25,
                digit3 << 7,
                0, 0);
            Assert.True(y == w);
        }


        void LeftShift_neg_big_shift()
        {
            uint digit1 = 0xC1F0F1CD;
            uint digit2 = 0xB38F4F83;
            uint digit3 = 0x1234678;
            BigInteger x = new BigInteger(-1, digit1, digit2, digit3);
            BigInteger y = x.LeftShift(7 + 64);
            BigInteger w = new BigInteger(-1,
                digit1 >> 25,
                digit1 << 7 | digit2 >> 25,
                digit2 << 7 | digit3 >> 25,
                digit3 << 7,
                0, 0);
            Assert.True(y == w);
        }

        void LeftShift_pos_big_shift_zero_high_bits()
        {
            uint digit1 = 0x0000F1CD;
            uint digit2 = 0xB38F4F83;
            uint digit3 = 0x1234678;
            BigInteger x = new BigInteger(1, digit1, digit2, digit3);
            BigInteger y = x.LeftShift(7 + 64);
            BigInteger w = new BigInteger(1,
                digit1 >> 25,
                digit1 << 7 | digit2 >> 25,
                digit2 << 7 | digit3 >> 25,
                digit3 << 7,
                0, 0);
            Assert.True(y == w);
        }

        [Fact]
        public void RightShift_zero_is_zero()
        {
            BigInteger x = new BigInteger(0);
            BigInteger y = x.RightShift(1000);
            Assert.True(y.IsZero);
        }


        [Fact]
        public void RightShift_neg_shift_same_as_left_shift()
        {
            uint digit1 = 0xC1F0F1CD;
            uint digit2 = 0xB38F4F83;
            uint digit3 = 0x1234678;
            BigInteger x = new BigInteger(1, digit1, digit2, digit3);
            BigInteger y = x.RightShift(-40);
            BigInteger z = x.LeftShift(40);
            Assert.True(y == z);
        }

        [Fact]
        public void RightShift_zero_shift_is_this()
        {
            uint digit1 = 0xC1F0F1CD;
            uint digit2 = 0xB38F4F83;
            uint digit3 = 0x1234678;
            BigInteger x = new BigInteger(1, digit1, digit2, digit3);
            BigInteger y = x.RightShift(0);
            Assert.Same(y, x);
        }

        void RightShift_pos_whole_digit_shift_loses_whole_digits()
        {
            uint digit1 = 0xC1F0F1CD;
            uint digit2 = 0xB38F4F83;
            uint digit3 = 0x1234678;
            BigInteger x = new BigInteger(1, digit1, digit2, digit3);
            BigInteger y = x.RightShift(64);
            BigInteger w = new BigInteger(1, digit1);
            Assert.True(y == w);
        }

        void RightShift_neg_whole_digit_shift_loses_whole_digits()
        {
            uint digit1 = 0xC1F0F1CD;
            uint digit2 = 0xB38F4F83;
            uint digit3 = 0x1234678;
            BigInteger x = new BigInteger(-1, digit1, digit2, digit3);
            BigInteger y = x.RightShift(64);
            BigInteger w = new BigInteger(-1, digit1);
            Assert.True(y == w);
        }


        void RightShift_pos_small_shift()
        {
            uint digit1 = 0xC1F0F1CD;
            uint digit2 = 0xB38F4F83;
            uint digit3 = 0x1234678;
            BigInteger x = new BigInteger(1, digit1, digit2, digit3);
            BigInteger y = x.RightShift(7);
            BigInteger w = new BigInteger(1,
                digit1 >> 7,
                digit1 << 25 | digit2 >> 7,
                digit2 << 25 | digit3 >> 7);
            Assert.True(y == w);
        }

        void RightShift_neg_small_shift()
        {
            uint digit1 = 0xC1F0F1CD;
            uint digit2 = 0xB38F4F83;
            uint digit3 = 0x1234678;
            BigInteger x = new BigInteger(-1, digit1, digit2, digit3);
            BigInteger y = x.RightShift(7);
            BigInteger w = new BigInteger(-1,
                digit1 >> 7,
                digit1 << 25 | digit2 >> 7,
                digit2 << 25 | digit3 >> 7);
            Assert.True(y == w);
        }


        void RightShift_pos_big_shift()
        {
            uint digit1 = 0xC1F0F1CD;
            uint digit2 = 0xB38F4F83;
            uint digit3 = 0x1234678;
            BigInteger x = new BigInteger(1, digit1, digit2, digit3);
            BigInteger y = x.RightShift(7 + 32);
            BigInteger w = new BigInteger(1,
                digit1 >> 7,
                digit1 << 25 | digit2 >> 7);
            Assert.True(y == w);
        }


        void RightShift_neg_big_shift()
        {
            uint digit1 = 0xC1F0F1CD;
            uint digit2 = 0xB38F4F83;
            uint digit3 = 0x1234678;
            BigInteger x = new BigInteger(-1, digit1, digit2, digit3);
            BigInteger y = x.RightShift(7 + 32);
            BigInteger w = new BigInteger(-1,
                digit1 >> 7,
                digit1 << 25 | digit2 >> 7);
            Assert.True(y == w);
        }

        void RightShift_pos_big_shift_zero_high_bits()
        {
            uint digit1 = 0x00001D;
            uint digit2 = 0xB38F4F83;
            uint digit3 = 0x1234678;
            BigInteger x = new BigInteger(1, digit1, digit2, digit3);
            BigInteger y = x.RightShift(7 + 32);
            BigInteger w = new BigInteger(1,
                digit1 >> 7,
                digit1 << 25 | digit2 >> 7);
            Assert.True(y == w);
        }

        #endregion

        #region Attempted conversion methods

        private void AsInt32Test(BigInteger i, bool expRet, int expInt)
        {
            int v;
            bool b = i.AsInt32(out v);
            Assert.Equal(expRet, b);
            Assert.Equal(expInt, v);
        }

        [Fact]
        public void AsInt32_various()
        {
            AsInt32Test(BigInteger.Create(0),
                true, 0);

            AsInt32Test(new BigInteger(-1, new uint[] { 0x80000001 }),
                false, 0);

            AsInt32Test(new BigInteger(1, new uint[] { 0x80000000 }),
                false, 0);

            AsInt32Test(new BigInteger(-1, new uint[] { 0x80000000 }),
                true, Int32.MinValue);

            AsInt32Test(BigInteger.Create(100),
                 true, 100);

            AsInt32Test(BigInteger.Create(-100),
                 true, -100);

            AsInt32Test(new BigInteger(1, new uint[] { 1, 0 }),
                false, 0);
        }


        private void AsInt64Test(BigInteger i, bool expRet, long expInt)
        {
            long v;
            bool b = i.AsInt64(out v);
            Assert.Equal(expRet, b);
            Assert.Equal(expInt, v);
        }

        [Fact]
        public void AsInt64_various()
        {
            AsInt64Test(BigInteger.Create(0),
                true, 0);

            AsInt64Test(new BigInteger(-1, new uint[] { 0x80000001 }),
                true, ((long)Int32.MinValue) - 1);

            AsInt64Test(new BigInteger(1, new uint[] { 0x80000000 }),
                true, 0x80000000);

            AsInt64Test(new BigInteger(-1, new uint[] { 0x80000000 }),
                true, (long)Int32.MinValue);

            AsInt64Test(new BigInteger(-1, new uint[] { 0x80000000, 0x00000001 }),
                false, 0);

            AsInt64Test(new BigInteger(1, new uint[] { 0x80000000, 0x0 }),
                false, 0);

            AsInt64Test(new BigInteger(-1, new uint[] { 0x80000000, 0x0 }),
                true, System.Int64.MinValue);

            AsInt64Test(BigInteger.Create(100),
                 true, 100);

            AsInt64Test(BigInteger.Create(-100),
                 true, -100);

            AsInt64Test(BigInteger.Create(123456789123456),
                 true, 123456789123456);

            AsInt64Test(BigInteger.Create(-123456789123456),
                 true, -123456789123456);

            AsInt64Test(new BigInteger(1, new uint[] { 1, 0, 0 }),
                false, 0);
        }

        private void AsUInt32Test(BigInteger i, bool expRet, uint expInt)
        {
            uint v;
            bool b = i.AsUInt32(out v);
            Assert.Equal(expRet, b);
            Assert.Equal(expInt, v);
        }

        [Fact]
        public void AsUInt32_various()
        {
            AsUInt32Test(BigInteger.Create(-1),
                false, 0);

            AsUInt32Test(new BigInteger(0),
                true, 0);

            AsUInt32Test(new BigInteger(1, 0xFFFFFFFF),
                true, 0xFFFFFFFF);

            AsUInt32Test(new BigInteger(1, 0x1, 0x0),
                false, 0);
        }

        private void AsUInt64Test(BigInteger i, bool expRet, ulong expInt)
        {
            ulong v;
            bool b = i.AsUInt64(out v);
            Assert.Equal(expRet, b);
            Assert.Equal(expInt, v);
        }

        [Fact]
        public void AsUInt64_various()
        {
            AsUInt64Test(BigInteger.Create(-1),
                false, 0);

            AsUInt64Test(new BigInteger(0),
                true, 0);

            AsUInt64Test(new BigInteger(1, 0xFFFFFFFF),
                true, 0xFFFFFFFF);

            AsUInt64Test(new BigInteger(1, 0xFFFFFFFF, 0xFFFFFFFF),
                true, 0xFFFFFFFFFFFFFFFF);

            AsUInt64Test(new BigInteger(1, 0x1, 0x0, 0x0),
                false, 0);
        }


        private void AsDecimalTest(BigInteger i, bool expRet, decimal expDec)
        {
            decimal v;
            bool b = i.AsDecimal(out v);
            Assert.Equal(expRet, b);
            Assert.Equal(expDec, v);
        }

        [Fact]
        public void AsDecimal_various()
        {
            AsDecimalTest(new BigInteger(0),
                true, Decimal.Zero);

            AsDecimalTest(new BigInteger(1, 0x1, 0x0, 0x0, 0x0),
                false, default(Decimal));

            AsDecimalTest(BigInteger.Create(1234),
                true, 1234M);

            AsDecimalTest(BigInteger.Create(-1234),
                true, -1234);

            AsDecimalTest(BigInteger.Create(123456789123456789123456.789M),
                true, 123456789123456789123456M);

            AsDecimalTest(BigInteger.Create(Decimal.MinValue),
                true, Decimal.MinValue);

            AsDecimalTest(BigInteger.Create(Decimal.MaxValue),
                true, Decimal.MaxValue);
        }

        #endregion

        // TODO: tests for IConvertible
        // TODO: tests for conversion methods

        #region IEquatable<BigInteger>

        [Fact]
        public void Equals_BI_on_null_is_false()
        {
            BigInteger i = new BigInteger(1, 0x1, 0x2, 0x3);
            Assert.False(i.Equals(null));
        }

        [Fact]
        public void Equals_BI_on_same_is_true()
        {
            BigInteger i = new BigInteger(1, 0x1, 0x2, 0x3);
            BigInteger j = new BigInteger(1, 0x1, 0x2, 0x3);
            Assert.True(i.Equals(j));
        }

        [Fact]
        public void Equals_BI_on_different_is_false()
        {
            BigInteger i = new BigInteger(1, 0x1, 0x2, 0x3);
            BigInteger j = new BigInteger(1, 0x1, 0x2, 0x4);
            Assert.False(i.Equals(j));
        }

        #endregion

        #region Precision tests

        [Fact]
        public void PrecisionSingleDigitsIsOne()
        {
            for (int i = -9; i <= 9; i++)
            {
                BigInteger bi = BigInteger.Create(i);
                Assert.Equal(1U, bi.Precision);
            }
        }

        [Fact]
        public void PrecisionTwoDigitsIsTwo()
        {
            int[] values = { -99, -50, -11, -10, 10, 11, 50, 99 };
            foreach (int v in values)
            {
                BigInteger bi = BigInteger.Create(v);
                Assert.Equal(2U, bi.Precision);
            }
        }

        [Fact]
        public void PrecisionThreeDigitsIsThree()
        {
            int[] values = { -999, -509, -101, -100, 100, 101, 500, 999 };
            foreach (int v in values)
            {
                BigInteger bi = BigInteger.Create(v);
                Assert.Equal(3U, bi.Precision);
            }
        }

        [Fact]
        public void PrecisionBoundaryCases()
        {
            string nines = "";
            string tenpow = "1";

            for (uint i = 1; i < 30; i++)
            {
                nines += "9";
                tenpow += "0";
                BigInteger bi9 = BigInteger.Parse(nines);
                BigInteger bi0 = BigInteger.Parse(tenpow);
                Assert.Equal(i, bi9.Precision);
                Assert.Equal(i + 1, bi0.Precision);
            }
        }

        [Fact]
        public void PrecisionBoundaryCase2()
        {
            BigInteger x = new BigInteger(1, new uint[] { 0xFFFFFFFF });
            BigInteger y = new BigInteger(1, new uint[] { 0x1, 0x0 });
            Assert.Equal(10U, x.Precision);
            Assert.Equal(10U, y.Precision);
        }

        #endregion

        #region Some utilities

        static bool SameValue(BigInteger i, int sign, uint[] mag)
        {
            return SameSign(i.Signum, sign)
                && SameMag(i.GetMagnitude(), mag);
        }

        static bool SameSign(int s1, int s2)
        {
            return s1 == s2;
        }

        static bool SameMag(uint[] xs, uint[] ys)
        {
            if (xs.Length != ys.Length)
                return false;

            for (int i = 0; i < xs.Length; i++)
                if (xs[i] != ys[i])
                    return false;

            return true;
        }

        #endregion

    }
}
