// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Xunit;

namespace Daml.Ledger.Api.Data.Test
{
    public class RecordFieldTest
    {
#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {
            // with label
            {
                var field1 = new Record.Field("label1", new Party("party1"));
                var field2 = new Record.Field("label2", new Party("party2"));
                var field3 = new Record.Field("label1", new Party("party1"));

                Assert.True(field1.Equals(field1));
                Assert.True(field1 == field1);

                Assert.True(field1.Equals(field3));
                Assert.True(field1 == field3);

                Assert.False(field1.Equals(field2));
                Assert.True(field1 != field2);
            }

            // without label
            {
                var field1 = new Record.Field(new Party("party1"));
                var field2 = new Record.Field(new Party("party2"));
                var field3 = new Record.Field(new Party("party1"));

                Assert.True(field1.Equals(field1));
                Assert.True(field1 == field1);

                Assert.True(field1.Equals(field3));
                Assert.True(field1 == field3);

                Assert.False(field1.Equals(field2));
                Assert.True(field1 != field2);
            }
        }
#pragma warning restore CS1718

        [Fact]
        public void HashCodeHasValueSemantics()
        {
            // with label
            {
                var field1 = new Record.Field("label1", new Party("party1"));
                var field2 = new Record.Field("label2", new Party("party2"));
                var field3 = new Record.Field("label1", new Party("party1"));

                Assert.True(field1.GetHashCode() == field3.GetHashCode());
                Assert.True(field1.GetHashCode() != field2.GetHashCode());
            }

            // without label
            {
                var field1 = new Record.Field(new Party("party1"));
                var field2 = new Record.Field(new Party("party2"));
                var field3 = new Record.Field(new Party("party1"));

                Assert.True(field1.GetHashCode() == field3.GetHashCode());
                Assert.True(field1.GetHashCode() != field2.GetHashCode());
            }
        }

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(new Record.Field("label", new Party("party")));
        }

        private void ConvertThroughProto(Record.Field source)
        {
            Com.Daml.Ledger.Api.V1.RecordField protoValue = source.ToProto();
            Record.Field field = Record.Field.FromProto(protoValue);
            Assert.True(source == field);
        }
    }
}