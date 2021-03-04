// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Daml.Ledger.Api.Data
{
    using Util;

    public class GetActiveContractsResponse : IWorkflowEvent
    {
        private readonly int _hashCode;

        public GetActiveContractsResponse(string offset, IEnumerable<CreatedEvent> activeContracts, string workflowId)
        {
            // Empty string indicates that the field is not present in the protobuf.
            Offset = Optional.OfNullable(offset);
            CreatedEvents = activeContracts.ToList().AsReadOnly();
            WorkflowId = workflowId;

            _hashCode = new HashCodeHelper().Add(Offset).AddRange(CreatedEvents).Add(WorkflowId).ToHashCode();
        }

        public static GetActiveContractsResponse FromProto(Com.Daml.Ledger.Api.V1.GetActiveContractsResponse response)
        {
            return new GetActiveContractsResponse(response.Offset, (from c in response.ActiveContracts select CreatedEvent.FromProto(c)).ToList(), response.WorkflowId);
        }

        public Com.Daml.Ledger.Api.V1.GetActiveContractsResponse ToProto()
        {
            var response = new Com.Daml.Ledger.Api.V1.GetActiveContractsResponse { WorkflowId = WorkflowId };
            Offset.IfPresent(offset => response.Offset = offset );
            response.ActiveContracts.AddRange(from c in CreatedEvents select c.ToProto());
            return response;
        }

        public Optional<string> Offset { get; }

        public IReadOnlyList<CreatedEvent> CreatedEvents { get; }

        public string WorkflowId { get; }

        public override string ToString() => $"GetActiveContractsResponse{{offset='{Offset}', activeContracts={CreatedEvents}, workflowId={WorkflowId}}}";

        public override bool Equals(object obj)
        {
            return this.Compare(obj, rhs => _hashCode == rhs._hashCode && Offset == rhs.Offset && WorkflowId == rhs.WorkflowId && !CreatedEvents.Except(rhs.CreatedEvents).Any());
        }

        public override int GetHashCode() => _hashCode;

        public static bool operator ==(GetActiveContractsResponse lhs, GetActiveContractsResponse rhs) => lhs.Compare(rhs);
        public static bool operator !=(GetActiveContractsResponse lhs, GetActiveContractsResponse rhs) => !(lhs == rhs);
    }
} 
