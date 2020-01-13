// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Grpc.Core;

namespace Daml.Ledger.Client.Reactive
{
    using Com.Daml.Ledger.Api.Util;

    public class LedgerIdentityClient
    {
        private readonly ILedgerIdentityClient _ledgerIdentityClient;

        public LedgerIdentityClient(Channel channel, Optional<string> accessToken)
        {
            _ledgerIdentityClient = new Client.LedgerIdentityClient(channel, accessToken.Reduce((string) null));
        }

        public Single<string> GetLedgerIdentity(Optional<string> accessToken = null)
        {
            return Single.Just(_ledgerIdentityClient.GetLedgerIdentity(accessToken?.Reduce((string) null)));
        }
    }
}
