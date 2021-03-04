// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Api.Data.Test.Factories
{
    public static class PartiesFactory
    {
        public static string[] Parties1 { get; } = { "party1", "party2", "party3", "party4", "party5" };
        public static string[] Parties2 { get; } = { "party6", "party7", "party8", "party9", "party10" };
        public static string[] Witnesses1 { get; } = { "witness1", "witness2", "witness3", "witness4", "witness5" };
        public static string[] Witnesses2 { get; } = { "witness6", "witness7", "witness8", "witness9", "witness10" };
        public static string[] Witnesses3 { get; } = { "witness11", "witness12", "witness13", "witness14", "witness15" };
        public static string[] Witnesses4 { get; } = { "witness16", "witness17", "witness18", "witness19", "witness20" };
        public static string[] Signatories1 { get; } = { "signer1", "signer2", "signer3" };
        public static string[] Signatories2 { get; } = { "signer4", "signer5", "signer6" };
        public static string[] Signatories3 { get; } = { "signer7", "signer8", "signer9" };
        public static string[] Signatories4 { get; } = { "signer10", "signer11", "signer12" };
        public static string[] Observers1 { get; } = { "observer1", "observer2", "observer3" };
        public static string[] Observers2 { get; } = { "observer4", "observer5", "observer6" };
        public static string[] Observers3 { get; } = { "observer7", "observer8", "observer9" };
        public static string[] Observers4 { get; } = { "observer10", "observer11", "observer12" };
        public static string[] ActingParties1 { get; } = { "actingParty1", "actingParty2", "actingParty3" };
        public static string[] ActingParties2 { get; } = { "actingParty4", "actingParty5", "actingParty6" };
    }
}
