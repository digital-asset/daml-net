// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;

namespace Com.Daml.Ledger.Api.Data 
{
    public class SubmitRequest
    {
        public static DigitalAsset.Ledger.Api.V1.SubmitRequest ToProto(string ledgerId, string workflowId, string applicationId, string commandId, string party,
                                                                       DateTimeOffset ledgerEffectiveTime, DateTimeOffset maximumRecordTime, IEnumerable<Command> commands)
        {
            return new DigitalAsset.Ledger.Api.V1.SubmitRequest { Commands = SubmitCommandsRequest.ToProto(ledgerId, workflowId, applicationId, commandId, party, ledgerEffectiveTime, maximumRecordTime, commands) };
        }
    }
}
