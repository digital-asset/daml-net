// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1.Testing;

    public interface ITimeClient
    {
        IAsyncEnumerator<GetTimeResponse> GetTime(string ledgerId);

        IEnumerable<GetTimeResponse> GetTimeSync(string ledgerId);

        void SetTime(string ledgerId, DateTime currentTime, DateTime newTime);

        Task SetTimeAsync(string ledgerId, DateTime currentTime, DateTime newTime);
    }
}
