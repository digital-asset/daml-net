// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Google.Protobuf.WellKnownTypes;

namespace Daml.Ledger.Client.Testing
{
    using System.Threading.Tasks;

    public interface IResetClient
    {
        void Reset(string accessToken = null);

        Task ResetAsync(string accessToken = null);
    }
}
