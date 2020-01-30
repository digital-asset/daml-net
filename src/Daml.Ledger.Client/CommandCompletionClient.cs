// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1;
    using Daml.Ledger.Client.Auth.Client;
    using Grpc.Core;

    public class CommandCompletionClient : ICommandCompletionClient
    {
        private readonly string _ledgerId;
        private readonly ClientStub<CommandCompletionService.CommandCompletionServiceClient> _commandCompletionClient;

        public CommandCompletionClient(string ledgerId, Channel channel)
        {
            _ledgerId = ledgerId;
            _commandCompletionClient = new ClientStub<CommandCompletionService.CommandCompletionServiceClient>(new CommandCompletionService.CommandCompletionServiceClient(channel));
        }

        public IAsyncEnumerator<CompletionStreamResponse> CompletionStream(string applicationId, LedgerOffset offset, IEnumerable<string> parties)
        {
            var request = new CompletionStreamRequest { LedgerId = _ledgerId, ApplicationId = applicationId };
           if (offset != null)
                request.Offset = offset;
            request.Parties.AddRange(parties);
            var response = _commandCompletionClient.Dispatch(request, (c, r, co) => c.CompletionStream(r, co));
            return response.ResponseStream;
        }

        public IEnumerable<CompletionStreamResponse> CompletionStreamSync(string applicationId, LedgerOffset offset, IEnumerable<string> parties)
        {
            using (var stream = CompletionStream(applicationId, offset, parties))
            {
                while (stream.MoveNext().Result)
                {
                    yield return stream.Current;
                }
            }
        }

        public LedgerOffset CompletionEnd(TraceContext traceContext = null)
        {
            var request = new CompletionEndRequest { LedgerId = _ledgerId, TraceContext = traceContext };
            var response = _commandCompletionClient.Dispatch(request, (c, r, co) => c.CompletionEnd(r, co));
            return response.Offset;
        }

        public async Task<LedgerOffset> CompletionEndAsync(TraceContext traceContext = null)
        {
            var request = new CompletionEndRequest { LedgerId = _ledgerId, TraceContext = traceContext };
            var response = await _commandCompletionClient.Dispatch(request, (c, r, co) => c.CompletionEndAsync(r, co));
            return response.Offset;
        }
    }
}
