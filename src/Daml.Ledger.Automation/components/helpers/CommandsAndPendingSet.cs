// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Com.Daml.Ledger.Api.Data;
using Com.Daml.Ledger.Api.Util;

using SubmitCommandsRequest = Com.Daml.Ledger.Api.Data.SubmitCommandsRequest;
using Identifier = Com.Daml.Ledger.Api.Data.Identifier;

namespace Daml.Ledger.Automation.Components.Helpers
{
    public class CommandsAndPendingSet
    {
        private readonly int _hashCode;

        // we use this as "invalid" value to signal that no submitCommandsRequest have been emitted by the bot
        public static readonly CommandsAndPendingSet Empty = new CommandsAndPendingSet(new SubmitCommandsRequest(string.Empty, string.Empty, string.Empty, string.Empty, DateTimeOffset.FromUnixTimeSeconds(0), DateTimeOffset.FromUnixTimeSeconds(0), new List<Command>()),
                                                                                       ImmutableDictionary<Identifier, ImmutableHashSet<string>>.Empty);

        public CommandsAndPendingSet(SubmitCommandsRequest submitCommandsRequest, ImmutableDictionary<Identifier, ImmutableHashSet<string>> contractIdsPendingIfSucceed)
        {
            SubmitCommandsRequest = submitCommandsRequest;
            ContractIdsPendingIfSucceed = contractIdsPendingIfSucceed;

            _hashCode = new HashCodeHelper().Add(SubmitCommandsRequest).AddDictionary(ContractIdsPendingIfSucceed).ToHashCode();
        }

        public SubmitCommandsRequest SubmitCommandsRequest { get; }

        public ImmutableDictionary<Identifier, ImmutableHashSet<string>> ContractIdsPendingIfSucceed { get; }

        public override string ToString() => $"CommandsAndPendingSet{{submitCommandsRequest={SubmitCommandsRequest}, contractIdsPendingIfSucceed={ContractIdsPendingIfSucceed}}}";

        public override bool Equals(object obj)
        {
            return this.Compare(obj, rhs =>
            {
                if (_hashCode == rhs._hashCode && SubmitCommandsRequest == rhs.SubmitCommandsRequest && ContractIdsPendingIfSucceed.Count == rhs.ContractIdsPendingIfSucceed.Count && !ContractIdsPendingIfSucceed.Keys.Except(rhs.ContractIdsPendingIfSucceed.Keys).Any())
                {
                    foreach (var pair in ContractIdsPendingIfSucceed)
                    {
                        var rhsSet = rhs.ContractIdsPendingIfSucceed[pair.Key];
                        if (pair.Value.Except(rhsSet).Any())
                            return false;
                    }
                    return true;
                }
                return false;
            });
        }

        public static bool operator ==(CommandsAndPendingSet lhs, CommandsAndPendingSet rhs) => lhs.Compare(rhs);
        public static bool operator !=(CommandsAndPendingSet lhs, CommandsAndPendingSet rhs) => !(lhs == rhs);

        public override int GetHashCode() => _hashCode;
    }
}
