// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Reactive
{
    using System;
    using Com.DigitalAsset.Ledger.Api.V1;

    public class CommandClient
    {
        private readonly ICommandClient _commandClient;

        public CommandClient(ICommandClient commandClient)
        {
            _commandClient = commandClient;
        }

        public IDisposable Submit(IObservable<Commands> commands)
        {
            return commands.Subscribe(_commandClient.SubmitAndWait);
        }
    }
}
