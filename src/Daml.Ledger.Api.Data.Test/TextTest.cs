// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Api.Data.Test
{
    using Util;

    public class TextTest
    {
#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {
            var text1 = new Text("text1");
            var text2 = new Text("text2");
            var text3 = new Text("text1");

            Assert.True(text1.Equals(text1));
            Assert.True(text1 == text1);

            Assert.True(text1.Equals(text3));
            Assert.True(text1 == text3);

            Assert.False(text1.Equals(text2));
            Assert.True(text1 != text2);
        }
#pragma warning restore CS1718

        [Fact]
        public void HashCodeHasValueSemantics()
        {
            var text1 = new Text("text1");
            var text2 = new Text("text2");
            var text3 = new Text("text1");

            Assert.True(text1.GetHashCode() == text3.GetHashCode());
            Assert.True(text1.GetHashCode() != text2.GetHashCode());
        }

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(new Text("text"));
        }

        private void ConvertThroughProto(Text source)
        {
            Com.Daml.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsText();
            maybe.Should().BeOfType<Some<Text>>();
            Assert.True(source == (Some<Text>)maybe);
        }
    }
}

