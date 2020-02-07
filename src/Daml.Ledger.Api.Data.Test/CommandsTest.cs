// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    using Daml.Ledger.Api.Data.Test.Factories;

    [TestFixture]
    public class CommandsTest
    {
        private static readonly DateTimeOffset _now = DateTimeOffset.UtcNow;

        private static readonly Commands _commands1 = new Commands("workflowId1", "applicationId1", "commandId1", "party1", _now, _now.AddDays(5), CreateCommandsFactory.Commands1);
        private static readonly Commands _commands2 = new Commands("workflowId2", "applicationId2", "commandId2", "party2", _now.AddDays(1), _now.AddDays(6), CreateCommandsFactory.Commands2);
        private static readonly Commands _commands3 = new Commands("workflowId1", "applicationId1", "commandId1", "party1", _now, _now.AddDays(5), CreateCommandsFactory.Commands1);

#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            Assert.IsTrue(_commands1.Equals(_commands1));
            Assert.IsTrue(_commands1 == _commands1);

            Assert.IsTrue(_commands1.Equals(_commands3));
            Assert.IsTrue(_commands1 == _commands3);

            Assert.IsFalse(_commands1.Equals(_commands2));
            Assert.IsTrue(_commands1 != _commands2);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            Assert.IsTrue(_commands1.GetHashCode() == _commands3.GetHashCode());
            Assert.IsTrue(_commands1.GetHashCode() != _commands2.GetHashCode());
        }

        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(_commands1);
        }

        private void ConvertThroughProto(Commands source)
        {
            Com.DigitalAsset.Ledger.Api.V1.Commands protoValue = source.ToProto("ledgerId");
            Commands target = Commands.FromProto(protoValue);
            Assert.IsTrue(source == target);
        }
    }
}
