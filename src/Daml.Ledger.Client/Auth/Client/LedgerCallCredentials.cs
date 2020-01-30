// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Auth.Client
{
    using System.Collections.Generic;
    using System.Linq;
    using Grpc.Core;
    using Grpc.Core.Utils;

    /// <summary>
    ///  See https://github.com/grpc/grpc/blob/master/src/csharp/Grpc.IntegrationTesting/MetadataCredentialsTest.cs and https://docs.microsoft.com/en-us/aspnet/core/grpc/authn-and-authz?view=aspnetcore-3.1 for guidance/inspiration
    /// </summary>
    public class LedgerCallCredentials
    {
        /// <summary>
        /// Return a CallCredentials that will set the specified access token in the Authorization metadata, if it is not null or empty.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns>A CallCredentials object</returns>
        public static CallCredentials MakeCallCredentials(string accessToken)
        {
            return CallCredentials.FromInterceptor(MakeAsyncAuthInterceptor(accessToken));
        }

        /// <summary>
        /// Return a CallCredentials that will set the specified non-empty access tokens in the Authorization metadata.
        /// </summary>
        /// <param name="accessTokens"></param>
        /// <returns>A CallCredentials object</returns>
        public static CallCredentials MakeCallCredentials(IEnumerable<string> accessTokens)
        {
            return CallCredentials.FromInterceptor(MakeAsyncAuthInterceptor(accessTokens));
        }

        internal static AsyncAuthInterceptor MakeAsyncAuthInterceptor(string accessToken)
        {
            return (context, metadata) =>
            {
                if (!string.IsNullOrEmpty(accessToken))
                    metadata.Add(AuthorizationKeyName, accessToken);
                return TaskUtils.CompletedTask;
            };
        }

        internal static AsyncAuthInterceptor MakeAsyncAuthInterceptor(IEnumerable<string> accessTokens)
        {
            // If no valid tokens then we will return an async auth interceptor but it won't do anything... 
            return (context, metadata) =>
            {
                accessTokens.Where(t => !string.IsNullOrEmpty(t)).Distinct().Select(t => t).ToList().ForEach(t => metadata.Add(AuthorizationKeyName, t)); // Should be merged so only one header key
                return TaskUtils.CompletedTask;
            };
        }

        private const string AuthorizationKeyName = "authorization";  // Seems like the Grpc code converts this to lowercase anyway
    }
}
