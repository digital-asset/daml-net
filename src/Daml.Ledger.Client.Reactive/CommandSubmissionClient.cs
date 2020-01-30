// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Reactive
{
    using System;
    using Com.DigitalAsset.Ledger.Api.V1;

    public class CommandSubmissionClient
    {
        private readonly ICommandSubmissionClient commandSubmissionClient;

        public CommandSubmissionClient(ICommandSubmissionClient commandSubmissionClient)
        {
            this.commandSubmissionClient = commandSubmissionClient;
        }

        public IDisposable Submit(IObservable<Commands> commands)
        {
            return commands.Subscribe(this.commandSubmissionClient.Submit);
        }
    }
}
