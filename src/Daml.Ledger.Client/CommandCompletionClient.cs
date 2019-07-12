// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1;
    using Grpc.Core;

    public class CommandCompletionClient : ICommandCompletionClient
    {
        private readonly CommandCompletionService.CommandCompletionServiceClient commandCompletionClient;

        public CommandCompletionClient(Channel channel)
        {
            this.commandCompletionClient = new CommandCompletionService.CommandCompletionServiceClient(channel);
        }

        public IAsyncEnumerator<CompletionStreamResponse> CompletionStream(string ledgerId, string applicationId, LedgerOffset offset, IEnumerable<string> parties)
        {
            var request = new CompletionStreamRequest { LedgerId = ledgerId, ApplicationId = applicationId, Offset = offset };
            request.Parties.AddRange(parties);
            var call = this.commandCompletionClient.CompletionStream(request);
            return call.ResponseStream;
        }

        public IEnumerable<CompletionStreamResponse> CompletionStreamSync(string ledgerId, string applicationId, LedgerOffset offset, IEnumerable<string> parties)
        {
            using (var stream = this.CompletionStream(ledgerId, applicationId, offset, parties))
            {
                while (stream.MoveNext().Result)
                {
                    yield return stream.Current;
                }
            }
        }

        public LedgerOffset CompletionEnd(string ledgerId, TraceContext traceContext = null)
        {
            var request = new CompletionEndRequest { LedgerId = ledgerId, TraceContext = traceContext };
            var response = this.commandCompletionClient.CompletionEnd(request);
            return response.Offset;
        }

        public async Task<LedgerOffset> CompletionEndAsync(string ledgerId, TraceContext traceContext = null)
        {
            var request = new CompletionEndRequest { LedgerId = ledgerId, TraceContext = traceContext };
            var response = await this.commandCompletionClient.CompletionEndAsync(request);
            return response.Offset;
        }

    }
}
