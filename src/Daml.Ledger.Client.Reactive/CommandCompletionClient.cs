// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Reactive
{
    using System;
    using System.Reactive.Concurrency;
    using System.Collections.Generic;
    using Com.DigitalAsset.Ledger.Api.V1;
    using Daml.Ledger.Client.Reactive.Util;

    public class CommandCompletionClient
    {
        private readonly ICommandCompletionClient _commandCompletionClient;
        private readonly IScheduler _scheduler;

        public CommandCompletionClient(ICommandCompletionClient commandCompletionClient, IScheduler scheduler = null)
        {
            _commandCompletionClient = commandCompletionClient;
            _scheduler = scheduler;
        }

        public IObservable<CompletionStreamResponse> GetLedgerConfiguration(string ledgerId, string applicationId, LedgerOffset offset, IEnumerable<string> parties)
        {
            return _commandCompletionClient.CompletionStream(ledgerId, applicationId, offset, parties).CreateAsyncObservable(_scheduler);
        }
    }
}
