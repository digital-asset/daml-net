// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Linq;
using Com.Daml.Ledger.Api.Util;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

using Single = Com.Daml.Ledger.Api.Util.Single;
using Command = Com.Daml.Ledger.Api.Data.Command;
using Transaction = Com.Daml.Ledger.Api.Data.Transaction;
using TransactionTree = Com.Daml.Ledger.Api.Data.TransactionTree;

namespace Daml.Ledger.Client.Reactive
{
    public class CommandClient
    {
        private readonly ICommandClient _commandClient;

        public CommandClient(string ledgerId, Channel channel, Optional<string> accessToken)
        {
            _commandClient = new Client.CommandClient(ledgerId, channel, accessToken.Reduce((string) null));
        }

        public Single<Empty> SubmitAndWait(string applicationId, string workflowId, string commandId, string party, DateTime ledgerEffectiveTime, DateTime maximumRecordTime, IEnumerable<Command> commands, Optional<string> accessToken = null)
        {
            return Single.Just(_commandClient.SubmitAndWait(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, from c in commands select c.ToProtoCommand(), accessToken?.Reduce((string) null)));
        }

        public Single<string> SubmitAndWaitForTransactionId(string applicationId, string workflowId, string commandId, string party, DateTime ledgerEffectiveTime, DateTime maximumRecordTime, IEnumerable<Command> commands, Optional<string> accessToken = null)
        {
            return Single.Just(_commandClient.SubmitAndWaitForTransactionId(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, from c in commands select c.ToProtoCommand(), accessToken?.Reduce((string) null)));
        }

        public Single<Transaction> SubmitAndWaitForTransaction(string applicationId, string workflowId, string commandId, string party, DateTime ledgerEffectiveTime, DateTime maximumRecordTime, IEnumerable<Command> commands, Optional<string> accessToken = null)
        {
            var txn = _commandClient.SubmitAndWaitForTransaction(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, from c in commands select c.ToProtoCommand(), accessToken?.Reduce((string) null));

            return Single.Just(Transaction.FromProto(txn));
        }

        public Single<TransactionTree> SubmitAndWaitForTransactionTree(string applicationId, string workflowId, string commandId, string party, DateTime ledgerEffectiveTime, DateTime maximumRecordTime, IEnumerable<Command> commands, Optional<string> accessToken = null)
        {
            var txnTree = _commandClient.SubmitAndWaitForTransactionTree(applicationId, workflowId, commandId, party, ledgerEffectiveTime, maximumRecordTime, from c in commands select c.ToProtoCommand(), accessToken?.Reduce((string) null));

            return Single.Just(TransactionTree.FromProto(txnTree));
        }
    }
}
