// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using NUnit.Framework;

namespace Daml.Ledger.Client.Test.Auth.Client
{
    using Daml.Ledger.Client.Auth.Client;

    [TestFixture]
    public class LedgerCallCredentialsTest
    {
        [Test]
        public void CanCreateAuthInterceptorForAccessToken()
        {
            AsyncAuthInterceptor asyncAuthInterceptor = LedgerCallCredentials.MakeAsyncAuthInterceptor("myAccessToken");

            Metadata metaData = new Metadata();

            Task task = asyncAuthInterceptor(new AuthInterceptorContext("url", "method"), metaData);

            Assert.IsTrue(task.IsCompleted);
            Assert.AreEqual(1, metaData.Count);
            var entry = metaData.First();
            Assert.AreEqual("authorization", entry.Key);
            Assert.AreEqual("myAccessToken", entry.Value);
        }

        [Test]
        public void InterceptorDoesNothingForEmptyAccessToken()
        {
            AsyncAuthInterceptor asyncAuthInterceptor = LedgerCallCredentials.MakeAsyncAuthInterceptor((string) null);

            Metadata metaData = new Metadata();

            Task task = asyncAuthInterceptor(new AuthInterceptorContext("url", "method"), metaData);

            Assert.IsTrue(task.IsCompleted);
            Assert.AreEqual(0, metaData.Count);
        }

        [Test]
        public void CanCreateAuthInterceptorForAccessTokens()
        {
            AsyncAuthInterceptor asyncAuthInterceptor = LedgerCallCredentials.MakeAsyncAuthInterceptor(new [] { "accessToken1", "accessToken2", "accessToken3" });

            Metadata metaData = new Metadata();

            Task task = asyncAuthInterceptor(new AuthInterceptorContext("url", "method"), metaData);

            Assert.IsTrue(task.IsCompleted);
            Assert.AreEqual(3, metaData.Count);
            int i = 1;
            foreach (var entry in metaData)
            {
                Assert.AreEqual("authorization", entry.Key);
                Assert.AreEqual($"accessToken{i++}", entry.Value);
            }
        }

        [Test]
        public void InterceptorDoesNothingForEmptyAccessTokens()
        {
            AsyncAuthInterceptor asyncAuthInterceptor = LedgerCallCredentials.MakeAsyncAuthInterceptor(new List<string>());

            Metadata metaData = new Metadata();

            Task task = asyncAuthInterceptor(new AuthInterceptorContext("url", "method"), metaData);

            Assert.IsTrue(task.IsCompleted);
            Assert.AreEqual(0, metaData.Count);
        }

        [Test]
        public void InterceptorCopesWithSparseAccessTokens()
        {
            AsyncAuthInterceptor asyncAuthInterceptor = LedgerCallCredentials.MakeAsyncAuthInterceptor(new[] { "accessToken1", null, "accessToken2", string.Empty, "accessToken3" });

            Metadata metaData = new Metadata();

            Task task = asyncAuthInterceptor(new AuthInterceptorContext("url", "method"), metaData);

            Assert.IsTrue(task.IsCompleted);
            int i = 1;
            foreach (var entry in metaData)
            {
                Assert.AreEqual("authorization", entry.Key);
                Assert.AreEqual($"accessToken{i++}", entry.Value);
            }
        }
    }
}