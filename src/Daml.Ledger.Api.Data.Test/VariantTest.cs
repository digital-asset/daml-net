// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Com.Daml.Ledger.Api.Data.Test.Factories;
using Com.Daml.Ledger.Api.Util;
using NUnit.Framework;

namespace Com.Daml.Ledger.Api.Data.Test
{
    [TestFixture]
    public class VariantTest
    {
#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            {
                var variant1 = new Variant(IdentifierFactory.Id1, "constructor1", new Int64(long.MaxValue));
                var variant2 = new Variant(IdentifierFactory.Id2, "constructor2", new Int64(long.MinValue));
                var variant3 = new Variant(IdentifierFactory.Id1, "constructor1", new Int64(long.MaxValue));

                Assert.IsTrue(variant1.Equals(variant1));
                Assert.IsTrue(variant1 == variant1);

                Assert.IsTrue(variant1.Equals(variant3));
                Assert.IsTrue(variant1 == variant3);

                Assert.IsFalse(variant1.Equals(variant2));
                Assert.IsTrue(variant1 != variant2);
            }

            {
                var variant1 = new Variant("constructor1", new Int64(long.MaxValue));
                var variant2 = new Variant("constructor2", new Int64(long.MinValue));
                var variant3 = new Variant("constructor1", new Int64(long.MaxValue));

                Assert.IsTrue(variant1.Equals(variant1));
                Assert.IsTrue(variant1 == variant1);

                Assert.IsTrue(variant1.Equals(variant3));
                Assert.IsTrue(variant1 == variant3);

                Assert.IsFalse(variant1.Equals(variant2));
                Assert.IsTrue(variant1 != variant2);
            }

        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            {
                var variant1 = new Variant(IdentifierFactory.Id1, "constructor1", new Int64(long.MaxValue));
                var variant2 = new Variant(IdentifierFactory.Id2, "constructor2", new Int64(long.MinValue));
                var variant3 = new Variant(IdentifierFactory.Id1, "constructor1", new Int64(long.MaxValue));

                Assert.IsTrue(variant1.GetHashCode() == variant3.GetHashCode());
                Assert.IsTrue(variant1.GetHashCode() != variant2.GetHashCode());
            }

            {
                var variant1 = new Variant("constructor1", new Int64(long.MaxValue));
                var variant2 = new Variant("constructor2", new Int64(long.MinValue));
                var variant3 = new Variant("constructor1", new Int64(long.MaxValue));

                Assert.IsTrue(variant1.GetHashCode() == variant3.GetHashCode());
                Assert.IsTrue(variant1.GetHashCode() != variant2.GetHashCode());
            }
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(new Variant(IdentifierFactory.Id1, "constructor1", new Int64(long.MaxValue)));
            ConvertThroughProto(new Variant("constructor2", new Int64(long.MinValue)));
        }

        private void ConvertThroughProto(Variant source)
        {
            DigitalAsset.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsVariant();
            Assert.AreEqual(typeof(Some<Variant>), maybe.GetType());
            Assert.IsTrue(source == (Some<Variant>)maybe);
        }
    }
}


