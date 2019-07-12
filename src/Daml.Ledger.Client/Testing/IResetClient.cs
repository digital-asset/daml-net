// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using System.Threading.Tasks;

    public interface IResetClient
    {
        void Reset(string ledgerId);

        Task ResetAsync(string ledgerId);
    }
}
