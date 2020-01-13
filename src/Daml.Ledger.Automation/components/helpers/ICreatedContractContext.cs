// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Automation.Components.Helpers
{
    public interface ICreatedContractContext
    {
        string WorkflowId { get; }
    }
}
