// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using System;
    using Com.DigitalAsset.Ledger.Api.V1;

    public class CommandClient
    {
        private readonly ICommandClient commandClient;

        public CommandClient(ICommandClient commandClient)
        {
            this.commandClient = commandClient;
        }

        public IDisposable Submit(IObservable<Commands> commands)
        {
            return commands.Subscribe(this.commandClient.SubmitAndWait);
        }
    }
}
