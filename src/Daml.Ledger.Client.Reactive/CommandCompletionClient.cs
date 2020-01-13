// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Grpc.Core;

namespace Daml.Ledger.Client.Reactive
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Concurrency;
    using System.Reactive.Linq;
    using Com.Daml.Ledger.Api.Util;
    using Util;

    using Single = Com.Daml.Ledger.Api.Util.Single;
    using CompletionStreamResponse = Com.Daml.Ledger.Api.Data.CompletionStreamResponse;
    using LedgerOffset = Com.Daml.Ledger.Api.Data.LedgerOffset;
    using TraceContext = Com.DigitalAsset.Ledger.Api.V1.TraceContext;

    public class CommandCompletionClient
    {
        private readonly ICommandCompletionClient _commandCompletionClient;
        private readonly IScheduler _scheduler;

        public CommandCompletionClient(string ledgerId, Channel channel, Optional<string> accessToken, IScheduler scheduler = null)
        {
            _commandCompletionClient = new Client.CommandCompletionClient(ledgerId, channel, accessToken.Reduce((string) null));
            _scheduler = scheduler;
        }

        public IObservable<CompletionStreamResponse> CompletionStream(string applicationId, LedgerOffset offset, HashSet<string> parties, Optional<string> accessToken = null)
        {
            return _commandCompletionClient.CompletionStream(applicationId, offset.ToProto(), parties, accessToken?.Reduce((string) null)).CreateAsyncObservable(_scheduler).Select(CompletionStreamResponse.FromProto);
        }

        public IObservable<CompletionStreamResponse> CompletionStream(string applicationId, HashSet<string> parties, Optional<string> accessToken = null)
        {
            return _commandCompletionClient.CompletionStream(applicationId, parties, accessToken?.Reduce((string) null)).CreateAsyncObservable(_scheduler).Select(CompletionStreamResponse.FromProto);
        }

        public Single<LedgerOffset> CompletionEnd(Optional<string> accessToken = null, TraceContext traceContext = null)
        {
            return Single.Just(LedgerOffset.FromProto(_commandCompletionClient.CompletionEnd(accessToken?.Reduce((string) null), traceContext)));
        }
    }
}
