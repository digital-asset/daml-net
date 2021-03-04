// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Api.Data.Test
{
    using Factories;
    using Util;
    
    public class DamlEnumTest
    {
#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {
            // With identifier
            {
                var de1 = new DamlEnum(IdentifierFactory.Id1, "constructor1");
                var de2 = new DamlEnum(IdentifierFactory.Id2, "constructor2");
                var de3 = new DamlEnum(IdentifierFactory.Id1, "constructor1");

                Assert.True(de1.Equals(de1));
                Assert.True(de1 == de1);

                Assert.True(de1.Equals(de3));
                Assert.True(de1 == de3);

                Assert.False(de1.Equals(de2));
                Assert.True(de1 != de2);
            }

            // Without identifier
            {
                var de1 = new DamlEnum("constructor1");
                var de2 = new DamlEnum("constructor2");
                var de3 = new DamlEnum("constructor1");

                Assert.True(de1.Equals(de3));
                Assert.True(de1 == de3);

                Assert.False(de1.Equals(de2));
                Assert.True(de1 != de2);
            }
        }
#pragma warning restore CS1718

        [Fact]
        public void HashCodeHasValueSemantics()
        {
            // With identifier
            {
                var de1 = new DamlEnum(IdentifierFactory.Id1, "constructor1");
                var de2 = new DamlEnum(IdentifierFactory.Id2, "constructor2");
                var de3 = new DamlEnum(IdentifierFactory.Id1, "constructor1");

                Assert.True(de1.GetHashCode() == de3.GetHashCode());
                Assert.True(de1.GetHashCode() != de2.GetHashCode());
            }

            // Without identifier
            {
                var de1 = new DamlEnum("constructor1");
                var de2 = new DamlEnum("constructor2");
                var de3 = new DamlEnum("constructor1");

                Assert.True(de1.GetHashCode() == de3.GetHashCode());
                Assert.True(de1.GetHashCode() != de2.GetHashCode());
            }
        }

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(new DamlEnum(IdentifierFactory.Id1, "constructor1"));
            ConvertThroughProto(new DamlEnum("constructor1"));
        }

        private void ConvertThroughProto(DamlEnum source)
        {
            Com.Daml.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsEnum();
            maybe.Should().BeOfType<Some<DamlEnum>>();
            Assert.True(source == (Some<DamlEnum>)maybe);
        }
    }
}

