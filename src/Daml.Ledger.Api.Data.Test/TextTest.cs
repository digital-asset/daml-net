// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    using Util;

    [TestFixture]
    public class TextTest
    {
#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            var text1 = new Text("text1");
            var text2 = new Text("text2");
            var text3 = new Text("text1");

            Assert.IsTrue(text1.Equals(text1));
            Assert.IsTrue(text1 == text1);

            Assert.IsTrue(text1.Equals(text3));
            Assert.IsTrue(text1 == text3);

            Assert.IsFalse(text1.Equals(text2));
            Assert.IsTrue(text1 != text2);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            var text1 = new Text("text1");
            var text2 = new Text("text2");
            var text3 = new Text("text1");

            Assert.IsTrue(text1.GetHashCode() == text3.GetHashCode());
            Assert.IsTrue(text1.GetHashCode() != text2.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(new Text("text"));
        }

        private void ConvertThroughProto(Text source)
        {
            Com.Daml.Ledger.Api.V1.Value protoValue = source.ToProto();
            var maybe = Value.FromProto(protoValue).AsText();
            Assert.AreEqual(typeof(Some<Text>), maybe.GetType());
            Assert.IsTrue(source == (Some<Text>)maybe);
        }
    }
}

