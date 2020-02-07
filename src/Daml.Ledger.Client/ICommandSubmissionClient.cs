// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daml.Ledger.Client
{
    using Com.DigitalAsset.Ledger.Api.V1;

    public interface ICommandSubmissionClient
    {
        string LedgerId { get; }

        void Submit(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands,
            string accessToken = null);

        Task SubmitAsync(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands,
            string accessToken = null);

        void Submit(Commands commands, string accessToken = null);

        Task SubmitAsync(Commands commands, string accessToken = null);
    }
}
