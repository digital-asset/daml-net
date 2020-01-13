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

        public CommandCompletionClient(string ledgerId, Channel channel, string accessToken)
        {
            _ledgerId = ledgerId;
            _commandCompletionClient = new ClientStub<CommandCompletionService.CommandCompletionServiceClient>(new CommandCompletionService.CommandCompletionServiceClient(channel));
        }

        public IAsyncEnumerator<CompletionStreamResponse> CompletionStream(string applicationId, LedgerOffset offset, IEnumerable<string> parties, string accessToken = null) => CompletionStreamImpl(applicationId, offset, parties, accessToken);

        public IAsyncEnumerator<CompletionStreamResponse> CompletionStream(string applicationId, IEnumerable<string> parties, string accessToken = null) => CompletionStreamImpl(applicationId, null, parties, accessToken);

        public IEnumerable<CompletionStreamResponse> CompletionStreamSync(string applicationId, LedgerOffset offset, IEnumerable<string> parties, string accessToken = null) => CompletionStreamSyncImpl(applicationId, offset, parties, accessToken);

        public IEnumerable<CompletionStreamResponse> CompletionStreamSync(string applicationId, IEnumerable<string> parties, string accessToken = null) => CompletionStreamSyncImpl(applicationId, null, parties, accessToken);

        public LedgerOffset CompletionEnd(string accessToken = null, TraceContext traceContext = null)
        {
            var request = new CompletionEndRequest { LedgerId = _ledgerId, TraceContext = traceContext };

            var response = _commandCompletionClient.WithAccess(accessToken).DispatchRequest(request, (c, r, co) => c.CompletionEnd(r, co), (c, r) => c.CompletionEnd(r));
            
            return response.Offset;
        }

        public async Task<LedgerOffset> CompletionEndAsync(string accessToken = null, TraceContext traceContext = null)
        {
            var request = new CompletionEndRequest { LedgerId = _ledgerId, TraceContext = traceContext };

            var response = await _commandCompletionClient.WithAccess(accessToken).DispatchRequest(request, (c, r, co) => c.CompletionEndAsync(r, co), (c, r) => c.CompletionEndAsync(r));

            return response.Offset;
        }

        private IAsyncEnumerator<CompletionStreamResponse> CompletionStreamImpl(string applicationId, LedgerOffset offset, IEnumerable<string> parties, string accessToken)
        {
            var request = new CompletionStreamRequest { LedgerId = _ledgerId, ApplicationId = applicationId };
            if (offset != null)
                request.Offset = offset;

            request.Parties.AddRange(parties);

            var response = _commandCompletionClient.WithAccess(accessToken).DispatchRequest(request, (c, r, co) => c.CompletionStream(r, co), (c, r) => c.CompletionStream(r));
            
            return response.ResponseStream;
        }

        private IEnumerable<CompletionStreamResponse> CompletionStreamSyncImpl(string applicationId, LedgerOffset offset, IEnumerable<string> parties, string accessToken)
        {
            using (var stream = CompletionStream(applicationId, offset, parties, accessToken))
            {
                while (stream.MoveNext().Result)
                {
                    yield return stream.Current;
                }
            }
        }
    }
}
