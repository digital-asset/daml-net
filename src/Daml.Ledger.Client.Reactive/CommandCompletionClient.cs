// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Reactive.Concurrency;
using System.Collections.Generic;

namespace Daml.Ledger.Client.Reactive
{
    using Daml.Ledger.Client.Reactive.Util;
    using Daml.Ledger.Api.Data.Util;

    using CompletionStreamResponse = Com.DigitalAsset.Ledger.Api.V1.CompletionStreamResponse;
    using LedgerOffset = Com.DigitalAsset.Ledger.Api.V1.LedgerOffset;
    using TraceContext = Com.DigitalAsset.Ledger.Api.V1.TraceContext;

    public class CommandCompletionClient
    {
        private readonly ICommandCompletionClient _commandCompletionClient;
        private readonly IScheduler _scheduler;

        public CommandCompletionClient(ICommandCompletionClient commandCompletionClient, IScheduler scheduler = null)
        {
            _commandCompletionClient = commandCompletionClient;
            _scheduler = scheduler;
        }
        
        public IObservable<CompletionStreamResponse> CompletionStream(string applicationId, Optional<LedgerOffset> offset, IEnumerable<string> parties, Optional<string> accessToken = null)
        {
            return _commandCompletionClient.CompletionStream(applicationId, offset.Reduce((LedgerOffset) null), parties, accessToken?.Reduce((string) null)).CreateAsyncObservable(_scheduler);
        }
        
        public LedgerOffset CompletionEnd(Optional<string> accessToken = null, TraceContext traceContext = null)
        {
            return _commandCompletionClient.CompletionEnd(accessToken?.Reduce((string) null), traceContext);
        } 
    }
}
