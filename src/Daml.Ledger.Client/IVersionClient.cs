// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Threading.Tasks;

namespace Daml.Ledger.Client
{
    public interface IVersionClient
    {
        string LedgerId { get; }

        string GetLedgerApiVersion(string accessToken = null);

        Task<string> GetLedgerApiVersionAsync(string accessToken = null);
    }
}
