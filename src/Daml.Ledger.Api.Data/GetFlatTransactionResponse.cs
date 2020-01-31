// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Api.Data
{
    using Util;

    public class GetFlatTransactionResponse
    {
        public GetFlatTransactionResponse(Transaction transaction)
        {
            Transaction = transaction;
        }

        public static GetFlatTransactionResponse FromProto(Com.DigitalAsset.Ledger.Api.V1.GetFlatTransactionResponse response) => new GetFlatTransactionResponse(Transaction.FromProto(response.Transaction));

        public Com.DigitalAsset.Ledger.Api.V1.GetFlatTransactionResponse ToProto() => new Com.DigitalAsset.Ledger.Api.V1.GetFlatTransactionResponse { Transaction = Transaction.ToProto() };

        public Transaction Transaction { get; }

        public override string ToString() => $"GetFlatTransactionResponse{{transaction={Transaction}}}";

        public override bool Equals(object obj) => this.Compare(obj, rhs => Transaction == rhs.Transaction);

        public override int GetHashCode() => Transaction.GetHashCode();

        public static bool operator ==(GetFlatTransactionResponse lhs, GetFlatTransactionResponse rhs) => lhs.Compare(rhs);
        public static bool operator !=(GetFlatTransactionResponse lhs, GetFlatTransactionResponse rhs) => !(lhs == rhs);
    }
}
