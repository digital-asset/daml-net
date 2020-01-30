// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1.Testing;

    public interface ITimeClient
    {
        IAsyncEnumerator<GetTimeResponse> GetTime(string accessToken = null);

        IEnumerable<GetTimeResponse> GetTimeSync(string accessToken = null);

        void SetTime(DateTime currentTime, DateTime newTime, string accessToken = null);

        Task SetTimeAsync(DateTime currentTime, DateTime newTime, string accessToken = null);
    }
}
