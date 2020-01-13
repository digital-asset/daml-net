// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Com.Daml.Ledger.Api.Util;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Daml.Ledger.Client.Reactive.Testing
{
    public class ResetClient
    {
        private readonly Client.Testing.IResetClient _resetClient;

        public ResetClient(string ledgerId, Channel channel, Optional<string> accessToken)
        {
            _resetClient = new Client.Testing.ResetClient(ledgerId, channel, accessToken.Reduce((string) null));
        }

        Single<Empty> Reset()
        {
            // Should call async version that returns a Task and convert to a Single ?? can wait on a single ? - how about in Java ?
            _resetClient.Reset();
            return Single.Just(new Empty());
        }
    }
}
