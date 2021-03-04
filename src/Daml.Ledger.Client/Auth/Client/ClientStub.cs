// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using Grpc.Core;

namespace Daml.Ledger.Client.Auth.Client
{
    /// <summary>
    /// A stub around a gRPC client class, to ease management of access tokens. So named to mimic the Java use case somewhat.
    /// </summary>
    /// <typeparam name="TClient"></typeparam>
    public class ClientStub<TClient> where TClient : class
    {
        private readonly TClient _client;
        private readonly HashSet<string> _accessTokens;
        private readonly CallOptions _callOptions;

        /// <summary>
        /// Construct with an optional access token to be specified on every call to the client class
        /// </summary>
        /// <param name="client">The grpc client class</param>
        /// <param name="accessToken">An optional access token</param>
        public ClientStub(TClient client, string accessToken = null)
        {
            _client = client;
            _callOptions = new CallOptions();

            if (!string.IsNullOrEmpty(accessToken))
            {
                _accessTokens = new HashSet<string>(new[] { accessToken });
                _callOptions = _callOptions.WithCredentials(LedgerCallCredentials.MakeCallCredentials(accessToken));
            }
        }

        /// <summary>
        /// Return a stub modified to use the optional access token. If none is provided just return this - makes the calling sequence easier.
        /// Note that we cope if the access token has already been specified
        /// </summary>
        /// <param name="accessToken">An optional access token</param>
        /// <returns></returns>
        public ClientStub<TClient> WithAccess(string accessToken = null)
        {
            ClientStub<TClient> clientStub = this;

            if (!string.IsNullOrEmpty(accessToken))
            {
                HashSet<string> accessTokens = _accessTokens ?? new HashSet<string>();

                // Only return a new stub itf we haven't already seen this token    
                if (accessTokens.Add(accessToken))
                    clientStub = new ClientStub<TClient>(_client, accessTokens);
            }

            return clientStub;
        }

        /// <summary>
        /// Dispatch a request to the client.
        ///
        /// Each API method on the client has two forms:
        ///
        /// - one that accepts the request object and then a series of defaulted argument
        /// - another that accepts the request object and a CallOptions object.
        /// 
        /// The first form creates a CallOptions object from its (possibly defaulted) argument and calls the second form of the API method.
        ///
        /// This method the accepts the request object and a functor that accepts the request object and a CallOptions object that we create. If access tokens were specified, this CallOptions
        /// object will have its CallCredentials set, otherwise it will just be a default constructed CallOptions.
        ///
        /// If the caller wishes to set any of the the arguments that were available in the first form of the client API method, they should instead set them on the supplied CallOptions
        /// object through its Builder interface.
        ///
        /// Hopefully this should cover all use cases.
        /// </summary>
        /// <typeparam name="TRequest">The request object type</typeparam>
        /// <typeparam name="TResponse">The response object type, may be a Grpc.Core.AsyncUnaryCall when Async methods on the client object are specified in the functors</typeparam>
        /// <param name="request">The request object</param>
        /// <param name="func">A functor that accepts the request object and a CallOptions object</param>
        /// <returns></returns>
        public TResponse Dispatch<TRequest, TResponse>(TRequest request, Func<TClient, TRequest, CallOptions, TResponse> func)
        {
            return func(_client, request, _callOptions);
        }

        /// <summary>
        /// A copy constructor for building a new object with an extra access token
        /// </summary>
        /// <param name="client"></param>
        /// <param name="accessTokens"></param>
        private ClientStub(TClient client, HashSet<string> accessTokens)
        {
            _client = client;
            _accessTokens = accessTokens;
            _callOptions = _callOptions.WithCredentials(LedgerCallCredentials.MakeCallCredentials(_accessTokens));
        }
    }
}