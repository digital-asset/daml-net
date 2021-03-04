// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Daml.Ledger.Api.Data 
{
    using Util;
    
    using Completion = Com.Daml.Ledger.Api.V1.Completion;

    public class CompletionStreamResponse
    {
        private readonly int _hashCode;

        public CompletionStreamResponse(Optional<Checkpoint> checkpoint, IEnumerable<Com.Daml.Ledger.Api.V1.Completion> completions)
        {
            Checkpoint = checkpoint;
            Completions.AddRange(completions);

            _hashCode = new HashCodeHelper().Add(Checkpoint).AddRange(Completions, CompletionComparer).ToHashCode();
        }

        public static CompletionStreamResponse FromProto(Com.Daml.Ledger.Api.V1.CompletionStreamResponse response)
        {
            if (response.Checkpoint != null)
                return new CompletionStreamResponse(Optional.Of(Data.Checkpoint.FromProto(response.Checkpoint)), response.Completions);

            return new CompletionStreamResponse(None.Value, response.Completions);
        }

        public Com.Daml.Ledger.Api.V1.CompletionStreamResponse ToProto()
        {
            var response = new Com.Daml.Ledger.Api.V1.CompletionStreamResponse();
            Checkpoint.IfPresent(c => response.Checkpoint = c.ToProto());
            response.Completions.AddRange(Completions);
            return response;
        }
        
        public Optional<Checkpoint> Checkpoint {  get; }

        public List<Completion> Completions { get; } = new List<Completion>();

        public override string ToString() => $"CompletionStreamResponse{{checkpoint={Checkpoint}, completions={Completions}}}";

        public override bool Equals(object obj) => this.Compare(obj, rhs => _hashCode == rhs._hashCode && Checkpoint == rhs.Checkpoint && !Completions.Except(rhs.Completions).Any());

        public override int GetHashCode() => _hashCode;

        public static bool operator ==(CompletionStreamResponse lhs, CompletionStreamResponse rhs) => lhs.Compare(rhs);
        public static bool operator !=(CompletionStreamResponse lhs, CompletionStreamResponse rhs) => !(lhs == rhs);

        private static int CompletionComparer(Completion lhs, Completion rhs)
        {
            if (lhs is null && rhs is null)
                return 0;
            if (lhs is null)
                return -1;
            if (rhs is null)
                return 1;
            return lhs.GetHashCode().CompareTo(rhs.GetHashCode());
        }
    }
}
