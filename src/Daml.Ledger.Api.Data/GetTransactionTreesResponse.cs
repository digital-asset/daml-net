// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Linq;
using System.Collections.Generic;

namespace Daml.Ledger.Api.Data 
{
    using Util;

    public class GetTransactionTreesResponse
    {
        private readonly int _hashCode;

        public GetTransactionTreesResponse(IEnumerable<TransactionTree> transactions)
        {
            Transactions = new List<TransactionTree>(transactions).AsReadOnly();

            _hashCode = new HashCodeHelper().AddRange(Transactions).ToHashCode();
        }

        public static GetTransactionTreesResponse FromProto(Com.DigitalAsset.Ledger.Api.V1.GetTransactionTreesResponse response) => new GetTransactionTreesResponse(from t in response.Transactions select TransactionTree.FromProto(t));

        public Com.DigitalAsset.Ledger.Api.V1.GetTransactionTreesResponse ToProto()
        {
            var response = new Com.DigitalAsset.Ledger.Api.V1.GetTransactionTreesResponse();
            response.Transactions.AddRange(from t in Transactions select t.ToProto());
            return response;
        }
        
        public IReadOnlyList<TransactionTree> Transactions { get; }

        public override string ToString() => $"GetTransactionTreesResponse{{transactions={Transactions}}}";

        public override bool Equals(object obj) => this.Compare(obj, rhs => _hashCode == rhs._hashCode && !Transactions.Except(rhs.Transactions).Any());

        public override int GetHashCode() => _hashCode;

        public static bool operator ==(GetTransactionTreesResponse lhs, GetTransactionTreesResponse rhs) => lhs.Compare(rhs);
        public static bool operator !=(GetTransactionTreesResponse lhs, GetTransactionTreesResponse rhs) => !(lhs == rhs);
    }
}
