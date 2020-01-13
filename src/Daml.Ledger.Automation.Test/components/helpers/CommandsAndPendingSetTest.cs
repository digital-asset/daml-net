// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Daml.Ledger.Automation.Components.Helpers;
using Com.Daml.Ledger.Api.Data.Test.Factories;
using NUnit.Framework;

using SubmitCommandsRequest = Com.Daml.Ledger.Api.Data.SubmitCommandsRequest;
using Identifier = Com.Daml.Ledger.Api.Data.Identifier;

namespace Daml.Ledger.Automation.Test.Components.Helpers
{
    [TestFixture]
    public class CommandsAndPendingSetTest
    {
        private static readonly ImmutableHashSet<string> _set1 = ImmutableHashSet.CreateRange(new[] { "one", "two", "three" });
        private static readonly ImmutableHashSet<string> _set2 = ImmutableHashSet.CreateRange(new[] { "four", "five", "six" });
        private static readonly ImmutableHashSet<string> _set3 = ImmutableHashSet.CreateRange(new[] { "seven", "eight", "nine" });
        private static readonly ImmutableHashSet<string> _set4 = ImmutableHashSet.CreateRange(new[] { "ten", "eleven", "twelve" });

        private static readonly ImmutableDictionary<Identifier, ImmutableHashSet<string>> _dictionary1;
        private static readonly ImmutableDictionary<Identifier, ImmutableHashSet<string>> _dictionary2;

        private static readonly CommandsAndPendingSet _commandsAndPendingSet1;
        private static readonly CommandsAndPendingSet _commandsAndPendingSet2;
        private static readonly CommandsAndPendingSet _commandsAndPendingSet3;

        static CommandsAndPendingSetTest()
        {
            var dictionary = new Dictionary<Identifier, ImmutableHashSet<string>>()
            {
                { IdentifierFactory.Id1, _set1 },
                { IdentifierFactory.Id2, _set2 }
            };

            _dictionary1 = ImmutableDictionary.CreateRange(dictionary);
            _dictionary2 = ImmutableDictionary.CreateRange(new Dictionary<Identifier, ImmutableHashSet<string>>()
            {
                { IdentifierFactory.Id3, _set3 },
                { IdentifierFactory.Id4, _set4 }
            });

            var now = DateTimeOffset.UtcNow;

            var request = new SubmitCommandsRequest("workflowId1", "applicationId1", "commandId1", "party1", now, now.AddDays(1), CreateCommandsFactory.Commands1);

            _commandsAndPendingSet1 = new CommandsAndPendingSet(request, _dictionary1);
            _commandsAndPendingSet2 = new CommandsAndPendingSet(new SubmitCommandsRequest("workflowId2", "applicationId2", "commandId2", "party2", now.AddDays(1), now.AddDays(2), CreateCommandsFactory.Commands2), _dictionary2);
            _commandsAndPendingSet3 = new CommandsAndPendingSet(request, _dictionary1);
        }

#pragma warning disable CS1718
        [Test]
        public void EqualityHasValueSemantics()
        {
            Assert.IsTrue(_commandsAndPendingSet1.Equals(_commandsAndPendingSet1));
            Assert.IsTrue(_commandsAndPendingSet1 == _commandsAndPendingSet1);

            Assert.IsTrue(_commandsAndPendingSet1.Equals(_commandsAndPendingSet3));
            Assert.IsTrue(_commandsAndPendingSet1 == _commandsAndPendingSet3);

            Assert.IsFalse(_commandsAndPendingSet1.Equals(_commandsAndPendingSet2));
            Assert.IsTrue(_commandsAndPendingSet1 != _commandsAndPendingSet2);
        }
#pragma warning restore CS1718

        [Test]
        public void HashCodeHasValueSemantics()
        {
            Assert.IsTrue(_commandsAndPendingSet1.GetHashCode() == _commandsAndPendingSet3.GetHashCode());
            Assert.IsTrue(_commandsAndPendingSet1.GetHashCode() != _commandsAndPendingSet2.GetHashCode());
        }

        [Test]
        public void ImmutableCollectionsAffectEqualityAndHashCode()
        {
            // Different keys to _commandsAndPendingSet1
            var commandsAndPendingSet1 = new CommandsAndPendingSet(_commandsAndPendingSet1.SubmitCommandsRequest, ImmutableDictionary.CreateRange(new Dictionary<Identifier, ImmutableHashSet<string>>()
            {
                { IdentifierFactory.Id3, _set1 },
                { IdentifierFactory.Id4, _set2 }
            }));

            Assert.IsFalse(commandsAndPendingSet1.Equals(_commandsAndPendingSet1));
            Assert.IsTrue(commandsAndPendingSet1.GetHashCode() != _commandsAndPendingSet1.GetHashCode());

            // Same keys, different sets to _commandsAndPendingSet1

            var commandsAndPendingSet2 = new CommandsAndPendingSet(_commandsAndPendingSet1.SubmitCommandsRequest, ImmutableDictionary.CreateRange(new Dictionary<Identifier, ImmutableHashSet<string>>()
            {
                { IdentifierFactory.Id1, _set3 },
                { IdentifierFactory.Id2, _set4 }
            }));

            Assert.IsFalse(commandsAndPendingSet2.Equals(_commandsAndPendingSet1));
            Assert.IsTrue(commandsAndPendingSet2.GetHashCode() != _commandsAndPendingSet1.GetHashCode());
        }

        [Test]
        public void HashCodeIncludesSetValues()
        {
            var commandsAndPendingSet = new CommandsAndPendingSet(_commandsAndPendingSet1.SubmitCommandsRequest, ImmutableDictionary.CreateRange(new Dictionary<Identifier, ImmutableHashSet<string>>()
            {
                { IdentifierFactory.Id1, ImmutableHashSet.CreateRange(new[] {"one", "two", "three" }) },
                { IdentifierFactory.Id2, ImmutableHashSet.CreateRange(new[] { "four", "five", "six" }) }
            }));

            Assert.IsTrue(commandsAndPendingSet.GetHashCode() == _commandsAndPendingSet1.GetHashCode());
        }
    }
}
