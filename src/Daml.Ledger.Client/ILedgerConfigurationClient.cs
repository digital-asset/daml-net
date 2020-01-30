// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using System.Collections.Generic;
    using Com.DigitalAsset.Ledger.Api.V1;

    public interface ILedgerConfigurationClient
    {
        IAsyncEnumerator<GetLedgerConfigurationResponse> GetLedgerConfiguration(TraceContext traceContext = null);

        IEnumerable<GetLedgerConfigurationResponse> GetLedgerConfigurationSync(TraceContext traceContext = null);
    }
}
