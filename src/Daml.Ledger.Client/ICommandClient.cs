// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daml.Ledger.Client
{
    using Com.DigitalAsset.Ledger.Api.V1;

    public interface ICommandClient
    {
        string LedgerId { get; }

        void SubmitAndWait(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands,
            string accessToken = null);

        Task SubmitAndWaitAsync(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands,
            string accessToken = null);

        void SubmitAndWait(Commands commands, string accessToken = null);

        Task SubmitAndWaitAsync(Commands commands, string accessToken = null);

        Transaction SubmitAndWaitForTransaction(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands,
            string accessToken = null);

        string SubmitAndWaitForTransactionId(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands,
            string accessToken = null);

        TransactionTree SubmitAndWaitForTransactionTree(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands,
            string accessToken = null);

        Task<Transaction> SubmitAndWaitForTransactionAsync(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands,
            string accessToken = null);

        Task<string> SubmitAndWaitForTransactionIdAsync(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands,
            string accessToken = null);

        Task<TransactionTree> SubmitAndWaitForTransactionTreeAsync(
            string applicationId,
            string workflowId,
            string commandId,
            string party,
            DateTime ledgerEffectiveTime,
            DateTime maximumRecordTime,
            IEnumerable<Command> commands,
            string accessToken = null);
    }
}
