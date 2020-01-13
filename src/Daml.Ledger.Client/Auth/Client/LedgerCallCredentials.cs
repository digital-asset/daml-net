// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;
using Grpc.Core;
using Grpc.Core.Utils;

namespace Daml.Ledger.Client.Auth.Client
{
    /// <summary>
    ///  See https://github.com/grpc/grpc/blob/master/src/csharp/Grpc.IntegrationTesting/MetadataCredentialsTest.cs and https://docs.microsoft.com/en-us/aspnet/core/grpc/authn-and-authz?view=aspnetcore-3.1 for guidance/inspiration
    /// </summary>
    public class LedgerCallCredentials
    {
        public static CallCredentials MakeCallCredentials(string accessToken)
        {
            return MakeCallCredentials(new[] { accessToken });
        }

        public static CallCredentials MakeCallCredentials(IEnumerable<string> accessTokens)
        {
            var asyncAuthInterceptor = new AsyncAuthInterceptor((context, metadata) =>
            {
                accessTokens.ToList().ForEach(t => metadata.Add("Authorization", t));       // Should be merged so only one header key
                return TaskUtils.CompletedTask;
            });

            return CallCredentials.FromInterceptor(asyncAuthInterceptor);
        }

        public static CallOptions? MakeCallOptions(string accessToken)
        {
            return MakeCallOptions(new[] { accessToken });
        }

        public static CallOptions? MakeCallOptions(IEnumerable<string> accessTokens)
        {
            CallOptions? callOptions = null;

            var tokens = accessTokens.Where(t => !string.IsNullOrEmpty(t)).Distinct().Select(t => t).ToList();

            if (tokens.Count > 0)
                callOptions = new CallOptions().WithCredentials(LedgerCallCredentials.MakeCallCredentials(tokens));

            return callOptions;
        }
    }
}
