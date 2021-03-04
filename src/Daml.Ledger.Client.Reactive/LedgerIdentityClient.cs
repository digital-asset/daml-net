// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Reactive
{
    using Daml.Ledger.Api.Data.Util;

    public class LedgerIdentityClient
    {
        private readonly ILedgerIdentityClient _ledgerIdentityClient;

        public LedgerIdentityClient(ILedgerIdentityClient ledgerIdentityClient)
        {
            _ledgerIdentityClient = ledgerIdentityClient;
        }

        public string GetLedgerIdentity(Optional<string> accessToken = null)
        {
            return _ledgerIdentityClient.GetLedgerIdentity(accessToken?.Reduce((string) null));
        }
    }
}
