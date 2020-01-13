// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Com.Daml.Ledger.Api.Util;
    
namespace Com.Daml.Ledger.Api.Data 
{
    public class GetActiveContractsRequest
    {
        public GetActiveContractsRequest(string ledgerId, TransactionFilter transactionFilter, bool verbose)
        {
            LedgerId = ledgerId;
            TransactionFilter = transactionFilter;
            Verbose = verbose;
        }

        public static GetActiveContractsRequest FromProto(DigitalAsset.Ledger.Api.V1.GetActiveContractsRequest request) => new GetActiveContractsRequest(request.LedgerId, TransactionFilter.FromProto(request.Filter), request.Verbose);

        public DigitalAsset.Ledger.Api.V1.GetActiveContractsRequest ToProto() => new DigitalAsset.Ledger.Api.V1.GetActiveContractsRequest { LedgerId = LedgerId, Filter = TransactionFilter.ToProto(), Verbose = Verbose };

        public string LedgerId { get; }

        public TransactionFilter TransactionFilter {  get; }

        public bool Verbose {  get; }

        public override bool Equals(object obj) => this.Compare(obj, rhs => Verbose == rhs.Verbose && LedgerId == rhs.LedgerId && TransactionFilter == rhs.TransactionFilter);

        public override int GetHashCode() => (LedgerId, TransactionFilter, Verbose).GetHashCode();

        public static bool operator ==(GetActiveContractsRequest lhs, GetActiveContractsRequest rhs) => lhs.Compare(rhs);
        public static bool operator !=(GetActiveContractsRequest lhs, GetActiveContractsRequest rhs) => !(lhs == rhs);

        public override string ToString() => $"GetActiveContractsRequest{{ledgerId='{LedgerId}', transactionFilter={TransactionFilter}, verbose={Verbose}}}";
    }
}
