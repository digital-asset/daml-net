// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;

namespace Daml.Ledger.Client
{
    using Com.Daml.Ledger.Api.V1;

    public interface ILedgerConfigurationClient
    {
        string LedgerId { get; }

        IAsyncEnumerator<GetLedgerConfigurationResponse> GetLedgerConfiguration(string accessToken = null, TraceContext traceContext = null);

        IEnumerable<GetLedgerConfigurationResponse> GetLedgerConfigurationSync(string accessToken = null, TraceContext traceContext = null);
    }
}
