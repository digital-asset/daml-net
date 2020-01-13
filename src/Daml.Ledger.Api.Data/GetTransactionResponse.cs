// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Com.Daml.Ledger.Api.Util;

namespace Com.Daml.Ledger.Api.Data 
{
    public class GetTransactionResponse
    {
        public GetTransactionResponse(TransactionTree transaction)
        {
            Transaction = transaction;
        }

        public static GetTransactionResponse FromProto(DigitalAsset.Ledger.Api.V1.GetTransactionResponse response) => new GetTransactionResponse(TransactionTree.FromProto(response.Transaction));

        public DigitalAsset.Ledger.Api.V1.GetTransactionResponse ToProto() => new DigitalAsset.Ledger.Api.V1.GetTransactionResponse { Transaction = Transaction.ToProto() };

        public TransactionTree Transaction {  get; }

        public override string ToString() => $"GetTransactionResponse{{transaction={Transaction}}}";

        public override bool Equals(object obj) => this.Compare(obj, rhs => Transaction == rhs.Transaction);

        public override int GetHashCode() => Transaction.GetHashCode();

        public static bool operator ==(GetTransactionResponse lhs, GetTransactionResponse rhs) => lhs.Compare(rhs);
        public static bool operator !=(GetTransactionResponse lhs, GetTransactionResponse rhs) => !(lhs == rhs);
    }
}
