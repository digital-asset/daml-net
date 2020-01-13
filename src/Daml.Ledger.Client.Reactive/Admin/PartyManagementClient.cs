// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Grpc.Core;

namespace Daml.Ledger.Client.Reactive.Admin
{
    using System;
    using System.Reactive.Linq;
    using Com.Daml.Ledger.Api.Util;
    using Com.DigitalAsset.Ledger.Api.V1.Admin;
    using Single = Com.Daml.Ledger.Api.Util.Single;

    public class PartyManagementClient
    {
        private readonly Client.Admin.IPartyManagementClient _partyManagementClient;

        public PartyManagementClient(Channel channel, Optional<string> accessToken)
        {
            _partyManagementClient = new Client.Admin.PartyManagementClient(channel, accessToken.Reduce((string) null));
        }

        public Single<PartyDetails> AllocateParty(string displayName, string partyIdHint, Optional<string> accessToken = null)
        {
            return Single.Just(_partyManagementClient.AllocateParty(displayName, partyIdHint, accessToken?.Reduce((string) null)));
        }

        Single<string> GetParticipantId(Optional<string> accessToken = null)
        {
            return Single.Just(_partyManagementClient.GetParticipantId(accessToken?.Reduce((string) null)));

        }

        IObservable<PartyDetails> ListKnownParties(Optional<string> accessToken = null)
        {
            return _partyManagementClient.ListKnownParties(accessToken?.Reduce((string) null)).ToObservable();
        }
    }
}
