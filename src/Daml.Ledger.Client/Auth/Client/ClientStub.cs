// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using Grpc.Core;

namespace Daml.Ledger.Client.Auth.Client
{
    /// <summary>
    /// A stub around a Grpc client class, to ease management of access tokens. So named to mimic the Java usa case somewhat.
    /// </summary>
    /// <typeparam name="TClient"></typeparam>
    public class ClientStub<TClient> where TClient : class
    {
        private readonly TClient _client;
        private readonly HashSet<string> _accessTokens;

        /// <summary>
        /// Construct with an optional access token to be specified on every call to the client class
        /// </summary>
        /// <param name="client">The grpc client class</param>
        /// <param name="accessToken">An optonal access token</param>
        public ClientStub(TClient client, string accessToken = null)
        {
            _client = client;
            if (!string.IsNullOrEmpty(accessToken))
                _accessTokens = new HashSet<string>(new[] { accessToken });
        }

        /// <summary>
        /// Return a stub modified to use the optional access token. If none is provided just return this - makes the calling sequence easier.
        /// Note that we cope if the access token has already been specified
        /// </summary>
        /// <param name="accessToken">An optional access token</param>
        /// <returns></returns>
        public ClientStub<TClient> WithAccess(string accessToken = null)
        {
            if (string.IsNullOrEmpty(accessToken))
                return this;

            HashSet<string> accessTokens = _accessTokens ?? new HashSet<string>();
            accessTokens.Add(accessToken);

            return new ClientStub<TClient>(_client, accessTokens);
        }

        /// <summary>
        /// Dispatch a request to the client depending on whether there are access tokens to attach to the call. Two functors must be provided, one to take the call options specifying the access tokens, and one with no access control
        /// </summary>
        /// <typeparam name="TRequest">The request object type</typeparam>
        /// <typeparam name="TResponse">The response object type, may be a Grpc.Core.AsyncUnaryCall when Async methods on the client object are specified in the functors</typeparam>
        /// <param name="request">The request object</param>
        /// <param name="funcWithAccessControl">The functor that uses access control</param>
        /// <param name="funcWithoutAccessControl">The functor with no access control</param>
        /// <returns></returns>
        public TResponse DispatchRequest<TRequest, TResponse>(TRequest request, Func<TClient, TRequest, CallOptions, TResponse> funcWithAccessControl, Func<TClient, TRequest, TResponse> funcWithoutAccessControl)
        {
            CallOptions? callOptions = _accessTokens.Count > 0 ? new CallOptions().WithCredentials(LedgerCallCredentials.MakeCallCredentials(_accessTokens)) : (CallOptions?)null;

            return callOptions.HasValue ? funcWithAccessControl(_client, request, callOptions.Value) : funcWithoutAccessControl(_client, request);
        }

        /// <summary>
        /// A copy constructor used with adding access tokens to the call
        /// </summary>
        /// <param name="client"></param>
        /// <param name="accessTokens"></param>
        private ClientStub(TClient client, HashSet<string> accessTokens)
        {
            _client = client;
            _accessTokens = accessTokens;
        }
    }
}