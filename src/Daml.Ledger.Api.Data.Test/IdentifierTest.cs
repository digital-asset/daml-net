// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    [TestFixture]
    public class IdentifierTest
    {
        private readonly Identifier _id1 = new Identifier("package1", "module1", "entity1");
        private readonly Identifier _id2 = new Identifier("package1", "module1", "entity1");
        private readonly Identifier _id3 = new Identifier("package3", "module3", "entity3");

#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            Assert.IsTrue(_id1.Equals(_id1));
            Assert.IsTrue(_id1 == _id1);

            Assert.IsTrue(_id1.Equals(_id2));
            Assert.IsTrue(_id1 == _id2);

            Assert.IsFalse(_id1.Equals(_id3));
            Assert.IsTrue(_id1 != _id3);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            Assert.IsTrue(_id1.GetHashCode() == _id2.GetHashCode());
            Assert.IsTrue(_id1.GetHashCode() != _id3.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(_id1);
        }

        [Test]
        public void CanConstructWithName()
        {
            var id1 = new Identifier("package1", "module1.entity1");
            var id2 = new Identifier("package1", "module1", "entity1");

            Assert.AreEqual("package1", id1.PackageId);
            Assert.AreEqual("module1", id1.ModuleName);
            Assert.AreEqual("entity1", id1.EntityName);
            
            Assert.IsTrue(id1.Equals(id2));
        }

        [Test]
        public void NameMustHaveModuleSpecified()
        {
            Assert.Throws<ArgumentException>(() => new Identifier("Package", ".entity"));
            Assert.Throws<ArgumentException>(() => new Identifier("Package", "entity"));
        }

        private void ConvertThroughProto(Identifier source)
        {
            Com.Daml.Ledger.Api.V1.Identifier protoValue = source.ToProto();
            var test = Identifier.FromProto(protoValue);
            Assert.IsTrue(source == test);
        }
    }
}

