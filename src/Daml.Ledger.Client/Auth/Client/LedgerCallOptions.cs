// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Auth.Client
{
    using System.Collections.Generic;
    using System.Linq;
    using Grpc.Core;

    /// <summary>
    ///  See https://github.com/grpc/grpc/blob/master/src/csharp/Grpc.IntegrationTesting/MetadataCredentialsTest.cs and https://docs.microsoft.com/en-us/aspnet/core/grpc/authn-and-authz?view=aspnetcore-3.1 for guidance/inspiration
    /// </summary>
    public class LedgerCallOptions
    {
        /// <summary>
        /// Make a CallOptions object with the specified access token for passing to the Ledger API for authentication.
        /// If the access token is null or empty then a null(able) is returned
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns>A CallOptions object or null</returns>
        public static CallOptions? MakeCallOptions(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
                return null;

            return new CallOptions().WithCredentials(LedgerCallCredentials.MakeCallCredentials(accessToken));
        }

        /// <summary>
        /// Make a CallOptions object with the specified access tokens for passing to the Ledger API for authentication.
        /// If there are no non-null/non-empty access tokens then a null(able) is returned
        /// </summary>
        /// <param name="accessTokens"></param>
        /// <returns>A CallOptions object or null</returns>
        public static CallOptions? MakeCallOptions(IEnumerable<string> accessTokens)
        {
            var tokens = accessTokens.Where(t => !string.IsNullOrEmpty(t)).Distinct().Select(t => t).ToList();

            if (tokens.Count == 0)
                return null;

            return new CallOptions().WithCredentials(LedgerCallCredentials.MakeCallCredentials(tokens));
        }
    }
}
