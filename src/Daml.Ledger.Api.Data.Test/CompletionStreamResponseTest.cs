// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using Xunit;

namespace Daml.Ledger.Api.Data.Test
{
    using Util;

    using Completion = Com.Daml.Ledger.Api.V1.Completion;

    public class CompletionStreamResponseTest
    {
        private static readonly Completion[] _completions1 = 
        {
            new Completion {CommandId = "commandId1", Status = new Google.Rpc.Status {Code = 1, Message = "success"}, TransactionId = "transaction1"},
            new Completion {CommandId = "commandId2", Status = new Google.Rpc.Status {Code = -1, Message = "failure"}, TransactionId = "transaction1"}
        };

        private static readonly Completion[] _completions2 =
        {
            new Completion {CommandId = "commandId3", Status = new Google.Rpc.Status {Code = 1, Message = "success"}, TransactionId = "transaction2"},
            new Completion {CommandId = "commandId4", Status = new Google.Rpc.Status {Code = -1, Message = "failure"}, TransactionId = "transaction2"}
        };

        private static readonly CompletionStreamResponse _request1 = new CompletionStreamResponse(Optional.Of(new Checkpoint(DateTimeOffset.UnixEpoch, LedgerOffset.LedgerBegin.Instance)), _completions1);
        private static readonly CompletionStreamResponse _request2 = new CompletionStreamResponse(Optional.Of(new Checkpoint(DateTimeOffset.UtcNow, LedgerOffset.LedgerBegin.Instance)), _completions2);
        private static readonly CompletionStreamResponse _request3 = new CompletionStreamResponse(Optional.Of(new Checkpoint(DateTimeOffset.UnixEpoch, LedgerOffset.LedgerBegin.Instance)), _completions1);

#pragma warning disable CS1718
        [Fact]
        public void EqualityHasValueSemantics()
        {
            Assert.True(_request1.Equals(_request1));
            Assert.True(_request1 == _request1);

            Assert.True(_request1.Equals(_request3));
            Assert.True(_request1 == _request3);

            Assert.False(_request1.Equals(_request2));
            Assert.True(_request1 != _request2);
        }
#pragma warning restore CS1718

        [Fact]
        public void HashCodeHasValueSemantics()
        {
            Assert.True(_request1.GetHashCode() == _request3.GetHashCode());
            Assert.True(_request1.GetHashCode() != _request2.GetHashCode());
        }

        [Fact]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(_request1);
            ConvertThroughProto(new CompletionStreamResponse(None.Value, _completions1));
        }

        private void ConvertThroughProto(CompletionStreamResponse source)
        {
            Com.Daml.Ledger.Api.V1.CompletionStreamResponse protoValue = source.ToProto();
            CompletionStreamResponse target = CompletionStreamResponse.FromProto(protoValue);
            Assert.True(source == target);
        }
    }
}




