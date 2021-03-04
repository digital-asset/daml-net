// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Client.Test.Auth.Client
{
    using Daml.Ledger.Client.Auth.Client;

    public class LedgerCallCredentialsTest
    {
        [Fact]
        public void CanCreateAuthInterceptorForAccessToken()
        {
            AsyncAuthInterceptor asyncAuthInterceptor = LedgerCallCredentials.MakeAsyncAuthInterceptor("myAccessToken");

            Metadata metaData = new Metadata();

            Task task = asyncAuthInterceptor(new AuthInterceptorContext("url", "method"), metaData);

            task.IsCompleted.Should().BeTrue();
            metaData.Should().ContainSingle();
            var entry = metaData.First();
            entry.Key.Should().Be("authorization");
            entry.Value.Should().Be("myAccessToken");
        }

        [Fact]
        public void InterceptorDoesNothingForEmptyAccessToken()
        {
            AsyncAuthInterceptor asyncAuthInterceptor = LedgerCallCredentials.MakeAsyncAuthInterceptor((string) null);

            Metadata metaData = new Metadata();

            Task task = asyncAuthInterceptor(new AuthInterceptorContext("url", "method"), metaData);

            task.IsCompleted.Should().BeTrue();
            metaData.Should().BeEmpty();
        }

        [Fact]
        public void CanCreateAuthInterceptorForAccessTokens()
        {
            AsyncAuthInterceptor asyncAuthInterceptor = LedgerCallCredentials.MakeAsyncAuthInterceptor(new [] { "accessToken1", "accessToken2", "accessToken3" });

            Metadata metaData = new Metadata();

            Task task = asyncAuthInterceptor(new AuthInterceptorContext("url", "method"), metaData);

            task.IsCompleted.Should().BeTrue();
            metaData.Should().HaveCount(3);
            int i = 1;
            foreach (var entry in metaData)
            {
                entry.Key.Should().Be("authorization");
                entry.Value.Should().Be($"accessToken{i++}");
            }
        }

        [Fact]
        public void InterceptorDoesNothingForEmptyAccessTokens()
        {
            AsyncAuthInterceptor asyncAuthInterceptor = LedgerCallCredentials.MakeAsyncAuthInterceptor(new List<string>());

            Metadata metaData = new Metadata();

            Task task = asyncAuthInterceptor(new AuthInterceptorContext("url", "method"), metaData);

            task.IsCompleted.Should().BeTrue();
            metaData.Should().BeEmpty();
        }

        [Fact]
        public void InterceptorCopesWithSparseAccessTokens()
        {
            AsyncAuthInterceptor asyncAuthInterceptor = LedgerCallCredentials.MakeAsyncAuthInterceptor(new[] { "accessToken1", null, "accessToken2", string.Empty, "accessToken3" });

            Metadata metaData = new Metadata();

            Task task = asyncAuthInterceptor(new AuthInterceptorContext("url", "method"), metaData);

            task.IsCompleted.Should().BeTrue();
            int i = 1;
            foreach (var entry in metaData)
            {
                entry.Key.Should().Be("authorization");
                entry.Value.Should().Be($"accessToken{i++}");
            }
        }
    }
}