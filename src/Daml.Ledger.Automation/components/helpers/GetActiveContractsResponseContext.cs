// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Com.Daml.Ledger.Api.Util;

namespace Daml.Ledger.Automation.Components.Helpers
{
    public class GetActiveContractsResponseContext : ICreatedContractContext
    {
        public GetActiveContractsResponseContext(string workflowId)
        {
            WorkflowId = workflowId;
        }

        public string WorkflowId { get; }

        public override string ToString() => $"GetActiveContractsResponseContext{{workflowId='{WorkflowId}'}}";

        public override bool Equals(object obj) => this.Compare(obj, rhs => WorkflowId == rhs.WorkflowId);

        public static bool operator ==(GetActiveContractsResponseContext lhs, GetActiveContractsResponseContext rhs) => lhs.Compare(rhs);
        public static bool operator !=(GetActiveContractsResponseContext lhs, GetActiveContractsResponseContext rhs) => !(lhs == rhs);


        public override int GetHashCode() => WorkflowId.GetHashCode();
    }
}
