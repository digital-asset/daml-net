// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Reactive
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Linq;
    using Com.DigitalAsset.Ledger.Api.V1;

    public class CommandCompletionClient
    {
        private readonly ICommandCompletionClient commandCompletionClient;

        public CommandCompletionClient(ICommandCompletionClient commandCompletionClient)
        {
            this.commandCompletionClient = commandCompletionClient;
        }

        public IObservable<CompletionStreamResponse> GetLedgerConfiguration(string ledgerId, string applicationId, LedgerOffset offset, IEnumerable<string> parties)
        {
            var observable = Observable.Create<CompletionStreamResponse>(async observer =>
            {
                using (var stream = this.commandCompletionClient.CompletionStream(ledgerId, applicationId, offset, parties))
                {
                    var hasNext = await stream.MoveNext();
                    while (hasNext)
                    {
                        observer.OnNext(stream.Current);
                        hasNext = await stream.MoveNext();
                    }
                }
            });

            return observable;
        }

    }
}
