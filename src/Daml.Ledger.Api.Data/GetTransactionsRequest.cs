// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Com.Daml.Ledger.Api.Util;

namespace Com.Daml.Ledger.Api.Data 
{
    public class GetTransactionsRequest
    {
        public string LedgerId { get; }

        public LedgerOffset Begin { get; }

        public Optional<LedgerOffset> End { get; }

        public TransactionFilter Filter { get; }

        public bool Verbose { get; }

        public GetTransactionsRequest(string ledgerId, LedgerOffset begin, LedgerOffset end, TransactionFilter filter, bool verbose)
         : this(ledgerId, begin, filter, verbose)
        {
            End = Optional.Of(end);
        }

        public GetTransactionsRequest(string ledgerId, LedgerOffset begin, TransactionFilter filter, bool verbose)
        {
            LedgerId = ledgerId;
            Begin = begin;
            End = None.Value;
            Filter = filter;
            Verbose = verbose;
        }

        public static GetTransactionsRequest FromProto(DigitalAsset.Ledger.Api.V1.GetTransactionsRequest request)
        {
            LedgerOffset begin = LedgerOffset.FromProto(request.Begin);
            TransactionFilter filter = TransactionFilter.FromProto(request.Filter);

            if (request.End != null)
            {
                LedgerOffset end = LedgerOffset.FromProto(request.End);
                return new GetTransactionsRequest(request.LedgerId, begin, end, filter, request.Verbose);
            }

            return new GetTransactionsRequest(request.LedgerId, begin, filter, request.Verbose);
        }

        public DigitalAsset.Ledger.Api.V1.GetTransactionsRequest ToProto()
        {
            var request = new DigitalAsset.Ledger.Api.V1.GetTransactionsRequest { LedgerId = LedgerId, Begin = Begin.ToProto(), Filter = Filter.ToProto(), Verbose = Verbose };
            End.IfPresent(end => request.End = end.ToProto());
            return request;
        }
    }
}
