// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Api.Data.Test
{
    using Factories;
    using Util;

    public class VariantTest
    {
#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {
            {
                var variant1 = new Variant(IdentifierFactory.Id1, "constructor1", new Int64(long.MaxValue));
                var variant2 = new Variant(IdentifierFactory.Id2, "constructor2", new Int64(long.MinValue));
                var variant3 = new Variant(IdentifierFactory.Id1, "constructor1", new Int64(long.MaxValue));

                Assert.True(variant1.Equals(variant1));
                Assert.True(variant1 == variant1);

                Assert.True(variant1.Equals(variant3));
                Assert.True(variant1 == variant3);

                Assert.False(variant1.Equals(variant2));
                Assert.True(variant1 != variant2);
            }

            {
                var variant1 = new Variant("constructor1", new Int64(long.MaxValue));
                var variant2 = new Variant("constructor2", new Int64(long.MinValue));
                var variant3 = new Variant("constructor1", new Int64(long.MaxValue));

                Assert.True(variant1.Equals(variant1));
                Assert.True(variant1 == variant1);

                Assert.True(variant1.Equals(variant3));
                Assert.True(variant1 == variant3);

                Assert.False(variant1.Equals(variant2));
                Assert.True(variant1 != variant2);
            }

        }
#pragma warning restore CS1718

        [Fact]
        public void HashCodeHasValueSemantics()
        {
            {
                var variant1 = new Variant(IdentifierFactory.Id1, "constructor1", new Int64(long.MaxValue));
                var variant2 = new Variant(IdentifierFactory.Id2, "constructor2", new Int64(long.MinValue));
                var variant3 = new Variant(IdentifierFactory.Id1, "constructor1", new Int64(long.MaxValue));

                Assert.True(variant1.GetHashCode() == variant3.GetHashCode());
                Assert.True(variant1.GetHashCode() != variant2.GetHashCode());
            }

            {
                var variant1 = new Variant("constructor1", new Int64(long.MaxValue));
                var variant2 = new Variant("constructor2", new Int64(long.MinValue));
                var variant3 = new Variant("constructor1", new Int64(long.MaxValue));

                Assert.True(variant1.GetHashCode() == variant3.GetHashCode());
                Assert.True(variant1.GetHashCode() != variant2.GetHashCode());
            }
        }

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(new Variant(IdentifierFactory.Id1, "constructor1", new Int64(long.MaxValue)));
            ConvertThroughProto(new Variant("constructor2", new Int64(long.MinValue)));
        }

        private void ConvertThroughProto(Variant source)
        {
            Com.Daml.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsVariant();
            maybe.Should().BeOfType<Some<Variant>>();
            Assert.True(source == (Some<Variant>)maybe);
        }
    }
}


