// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Test
{
    using Daml.Ledger.Api.Data.Test.Factories;

    [TestFixture]
    public class GetTransactionsRequestTest
    {
        [Test]
        public void CanConvertBetweenProto()
        {
            ConvertThroughProto(new GetTransactionsRequest("ledger1", LedgerOffset.LedgerBegin.Instance, LedgerOffset.LedgerEnd.Instance, FiltersByPartyFactory.Filters1, false));
            ConvertThroughProto(new GetTransactionsRequest("ledger2", new LedgerOffset.Absolute("somewhere in the middle"), FiltersByPartyFactory.Filters2, true));
        }

        private void ConvertThroughProto(GetTransactionsRequest source)
        {
            Com.DigitalAsset.Ledger.Api.V1.GetTransactionsRequest protoValue = source.ToProto();
            GetTransactionsRequest target = GetTransactionsRequest.FromProto(protoValue);

            Assert.AreEqual(source.LedgerId, target.LedgerId);
            Assert.AreEqual(source.Begin, target.Begin);
            Assert.AreEqual(source.End, target.End);
            Assert.AreEqual(source.Filter, target.Filter);
            Assert.AreEqual(source.Verbose, target.Verbose);
        }
    }
}
