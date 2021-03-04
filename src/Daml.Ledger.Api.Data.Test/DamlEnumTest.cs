// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    using Daml.Ledger.Api.Data.Test.Factories;
    using Util;
    
    [TestFixture]
    public class DamlEnumTest
    {
#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            // With identifier
            {
                var de1 = new DamlEnum(IdentifierFactory.Id1, "constructor1");
                var de2 = new DamlEnum(IdentifierFactory.Id2, "constructor2");
                var de3 = new DamlEnum(IdentifierFactory.Id1, "constructor1");

                Assert.IsTrue(de1.Equals(de1));
                Assert.IsTrue(de1 == de1);

                Assert.IsTrue(de1.Equals(de3));
                Assert.IsTrue(de1 == de3);

                Assert.IsFalse(de1.Equals(de2));
                Assert.IsTrue(de1 != de2);
            }

            // Without identifier
            {
                var de1 = new DamlEnum("constructor1");
                var de2 = new DamlEnum("constructor2");
                var de3 = new DamlEnum("constructor1");

                Assert.IsTrue(de1.Equals(de3));
                Assert.IsTrue(de1 == de3);

                Assert.IsFalse(de1.Equals(de2));
                Assert.IsTrue(de1 != de2);
            }
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            // With identifier
            {
                var de1 = new DamlEnum(IdentifierFactory.Id1, "constructor1");
                var de2 = new DamlEnum(IdentifierFactory.Id2, "constructor2");
                var de3 = new DamlEnum(IdentifierFactory.Id1, "constructor1");

                Assert.IsTrue(de1.GetHashCode() == de3.GetHashCode());
                Assert.IsTrue(de1.GetHashCode() != de2.GetHashCode());
            }

            // Without identifier
            {
                var de1 = new DamlEnum("constructor1");
                var de2 = new DamlEnum("constructor2");
                var de3 = new DamlEnum("constructor1");

                Assert.IsTrue(de1.GetHashCode() == de3.GetHashCode());
                Assert.IsTrue(de1.GetHashCode() != de2.GetHashCode());
            }
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(new DamlEnum(IdentifierFactory.Id1, "constructor1"));
            ConvertThroughProto(new DamlEnum("constructor1"));
        }

        private void ConvertThroughProto(DamlEnum source)
        {
            Com.DigitalAsset.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsEnum();
            Assert.AreEqual(typeof(Some<DamlEnum>), maybe.GetType());
            Assert.IsTrue(source == (Some<DamlEnum>)maybe);
        }
    }
}

