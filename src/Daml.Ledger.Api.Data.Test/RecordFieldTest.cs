// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    [TestFixture]
    public class RecordFieldTest
    {
#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            // with label
            {
                var field1 = new Record.Field("label1", new Party("party1"));
                var field2 = new Record.Field("label2", new Party("party2"));
                var field3 = new Record.Field("label1", new Party("party1"));

                Assert.IsTrue(field1.Equals(field1));
                Assert.IsTrue(field1 == field1);

                Assert.IsTrue(field1.Equals(field3));
                Assert.IsTrue(field1 == field3);

                Assert.IsFalse(field1.Equals(field2));
                Assert.IsTrue(field1 != field2);
            }

            // without label
            {
                var field1 = new Record.Field(new Party("party1"));
                var field2 = new Record.Field(new Party("party2"));
                var field3 = new Record.Field(new Party("party1"));

                Assert.IsTrue(field1.Equals(field1));
                Assert.IsTrue(field1 == field1);

                Assert.IsTrue(field1.Equals(field3));
                Assert.IsTrue(field1 == field3);

                Assert.IsFalse(field1.Equals(field2));
                Assert.IsTrue(field1 != field2);
            }
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            // with label
            {
                var field1 = new Record.Field("label1", new Party("party1"));
                var field2 = new Record.Field("label2", new Party("party2"));
                var field3 = new Record.Field("label1", new Party("party1"));

                Assert.IsTrue(field1.GetHashCode() == field3.GetHashCode());
                Assert.IsTrue(field1.GetHashCode() != field2.GetHashCode());
            }

            // without label
            {
                var field1 = new Record.Field(new Party("party1"));
                var field2 = new Record.Field(new Party("party2"));
                var field3 = new Record.Field(new Party("party1"));

                Assert.IsTrue(field1.GetHashCode() == field3.GetHashCode());
                Assert.IsTrue(field1.GetHashCode() != field2.GetHashCode());
            }
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(new Record.Field("label", new Party("party")));
        }

        private void ConvertThroughProto(Record.Field source)
        {
            Com.Daml.Ledger.Api.V1.RecordField protoValue = source.ToProto();
            Record.Field field = Record.Field.FromProto(protoValue);
            Assert.IsTrue(source == field);
        }
    }
}