// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Google.Protobuf.WellKnownTypes;

namespace Daml.Ledger.Client.Reactive.Testing
{
    using Daml.Ledger.Client.Testing;

    public class ResetClient
    {
        private readonly IResetClient _resetClient;

        public ResetClient(IResetClient resetClient)
        {
            _resetClient = resetClient;
        }

        void Reset()
        {
            _resetClient.Reset();
        }
    }
}
