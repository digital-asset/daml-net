// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Daml.Ledger.Api.Data 
{
    using Util;

    public class GetTransactionsResponse
    {
        public GetTransactionsResponse(IEnumerable<Transaction> transactions)
        {
            Transactions = new List<Transaction>(transactions);

            _hashCode = new HashCodeHelper().AddRange(Transactions).ToHashCode();
        }

        public static GetTransactionsResponse FromProto(Com.DigitalAsset.Ledger.Api.V1.GetTransactionsResponse response) => new GetTransactionsResponse(from t in response.Transactions select Transaction.FromProto(t));

        public Com.DigitalAsset.Ledger.Api.V1.GetTransactionsResponse ToProto()
        {
            var response = new Com.DigitalAsset.Ledger.Api.V1.GetTransactionsResponse();
            response.Transactions.AddRange(from t in Transactions select t.ToProto());
            return response;
        }

        public List<Transaction> Transactions { get; }

        public override string ToString() => $"GetTransactionsResponse{{transactions={Transactions}}}";

        public override bool Equals(object obj) => this.Compare(obj, rhs => _hashCode == rhs._hashCode && !Transactions.Except(rhs.Transactions).Any());

        public override int GetHashCode() => _hashCode;

        public static bool operator ==(GetTransactionsResponse lhs, GetTransactionsResponse rhs) => lhs.Compare(rhs);
        public static bool operator !=(GetTransactionsResponse lhs, GetTransactionsResponse rhs) => !(lhs == rhs);

        private readonly int _hashCode;
    }
}
