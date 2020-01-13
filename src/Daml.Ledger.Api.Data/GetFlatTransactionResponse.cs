// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Com.Daml.Ledger.Api.Util;

namespace Com.Daml.Ledger.Api.Data
{
    public class GetFlatTransactionResponse
    {
        public GetFlatTransactionResponse(Transaction transaction)
        {
            Transaction = transaction;
        }

        public static GetFlatTransactionResponse FromProto(DigitalAsset.Ledger.Api.V1.GetFlatTransactionResponse response) => new GetFlatTransactionResponse(Transaction.FromProto(response.Transaction));

        public DigitalAsset.Ledger.Api.V1.GetFlatTransactionResponse ToProto() => new DigitalAsset.Ledger.Api.V1.GetFlatTransactionResponse { Transaction = Transaction.ToProto() };

        public Transaction Transaction { get; }

        public override string ToString() => $"GetFlatTransactionResponse{{transaction={Transaction}}}";

        public override bool Equals(object obj) => this.Compare(obj, rhs => Transaction == rhs.Transaction);

        public override int GetHashCode() => Transaction.GetHashCode();

        public static bool operator ==(GetFlatTransactionResponse lhs, GetFlatTransactionResponse rhs) => lhs.Compare(rhs);
        public static bool operator !=(GetFlatTransactionResponse lhs, GetFlatTransactionResponse rhs) => !(lhs == rhs);
    }
}
