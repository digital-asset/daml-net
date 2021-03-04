// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Daml.Ledger.Client.Reactive
{
    using Daml.Ledger.Api.Data.Util;

    using Commands = Com.Daml.Ledger.Api.V1.Commands;

    public class CommandClient
    {
        private readonly ICommandClient _commandClient;

        public CommandClient(ICommandClient commandClient)
        {
            _commandClient = commandClient;
        }

        public IDisposable Submit(IObservable<Commands> commands, Optional<string> accessToken = null)
        {
            return commands.Subscribe(c => _commandClient.SubmitAndWait(c , accessToken?.Reduce((string) null)));
        }
    }
}
