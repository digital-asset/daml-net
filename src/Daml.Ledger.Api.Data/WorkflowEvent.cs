// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Api.Data 
{
    /**
     * A Ledger event regarding a workflow identified by the
     * {@link WorkflowEvent#getWorkflowId()}.
     */
    public interface IWorkflowEvent
    {
        string WorkflowId { get; }
    }
}
