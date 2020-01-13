// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Linq;
using Grpc.Core;

namespace Daml.Ledger.Client.Reactive
{
    using System;
    using System.Collections.Generic;
    using Com.Daml.Ledger.Api.Util;
    using Google.Protobuf.WellKnownTypes;

    using Single = Com.Daml.Ledger.Api.Util.Single;
    using Command = Com.Daml.Ledger.Api.Data.Command;

    public class CommandSubmissionClient
    {
        private readonly string _ledgerId;
        private readonly ICommandSubmissionClient _commandSubmissionClient;

        public CommandSubmissionClient(string ledgerId, Channel channel, Optional<string> accessToken)
        {
            _ledgerId = ledgerId;
            _commandSubmissionClient = new Client.CommandSubmissionClient(ledgerId, channel, accessToken.Reduce((string) null));
        }

        public Single<Empty> Submit(string applicationId, string workflowId, string commandId, string party, DateTime ledgerEffectiveTime, DateTime maximumRecordTime, List<Command> commands, Optional<string> accessToken = null)
        {
            return Single.Just(_commandSubmissionClient.Submit(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, from c in commands select c.ToProtoCommand(), accessToken?.Reduce((string) null)));
        }
    }
}
