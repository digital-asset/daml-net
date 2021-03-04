// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Utils;

namespace Daml.Ledger.Client
{
    using Com.Daml.Ledger.Api.V1;
    using Daml.Ledger.Client.Auth.Client;

    public class CommandCompletionClient : ICommandCompletionClient
    {
        private readonly ClientStub<CommandCompletionService.CommandCompletionServiceClient> _commandCompletionClient;

        public CommandCompletionClient(string ledgerId, Channel channel, string accessToken)
        {
            LedgerId = ledgerId;
            _commandCompletionClient = new ClientStub<CommandCompletionService.CommandCompletionServiceClient>(new CommandCompletionService.CommandCompletionServiceClient(channel), accessToken);
        }

        public string LedgerId { get; }

        public IAsyncEnumerator<CompletionStreamResponse> CompletionStream(string applicationId, LedgerOffset offset, IEnumerable<string> parties, string accessToken = null)
        {
            return CompletionStreamImpl(applicationId, offset, parties, accessToken).ReadAllAsync().GetAsyncEnumerator();
        }

        public IEnumerable<CompletionStreamResponse> CompletionStreamSync(string applicationId, LedgerOffset offset, IEnumerable<string> parties, string accessToken = null)
        {
            return CompletionStreamImpl(applicationId, offset, parties, accessToken).ToListAsync().Result;
        }

        public LedgerOffset CompletionEnd(string accessToken = null, TraceContext traceContext = null)
        {
            var request = new CompletionEndRequest { LedgerId = LedgerId, TraceContext = traceContext };
            var response = _commandCompletionClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.CompletionEnd(r, co));
            return response.Offset;
        }

        public async Task<LedgerOffset> CompletionEndAsync(string accessToken = null, TraceContext traceContext = null)
        {
            var request = new CompletionEndRequest { LedgerId = LedgerId, TraceContext = traceContext };
            var response = await _commandCompletionClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.CompletionEndAsync(r, co));
            return response.Offset;
        }

        private IAsyncStreamReader<CompletionStreamResponse> CompletionStreamImpl(string applicationId, LedgerOffset offset, IEnumerable<string> parties, string accessToken)
        {
            var request = new CompletionStreamRequest { LedgerId = LedgerId, ApplicationId = applicationId };
            if (offset != null)
                request.Offset = offset;
            request.Parties.AddRange(parties);
            var response = _commandCompletionClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.CompletionStream(r, co));
            return response.ResponseStream;
        }

    }
}
