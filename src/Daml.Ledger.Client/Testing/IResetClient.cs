// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Threading.Tasks;

namespace Daml.Ledger.Client.Testing
{
    public interface IResetClient
    {
        void Reset(string accessToken = null);

        Task ResetAsync(string accessToken = null);
    }
}
