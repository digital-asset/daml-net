// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Daml.Ledger.Client.Reactive
{
    using Daml.Ledger.Api.Data.Util;

    using Commands = Com.DigitalAsset.Ledger.Api.V1.Commands;

    public class CommandSubmissionClient
    {
        private readonly ICommandSubmissionClient _commandSubmissionClient;

        public CommandSubmissionClient(ICommandSubmissionClient commandSubmissionClient)
        {
            _commandSubmissionClient = commandSubmissionClient;
        }

        public IDisposable Submit(IObservable<Commands> commands, Optional<string> accessToken = null)
        {
            return commands.Subscribe(c => _commandSubmissionClient.Submit(c, accessToken?.Reduce((string) null)));
        }
    }
}
