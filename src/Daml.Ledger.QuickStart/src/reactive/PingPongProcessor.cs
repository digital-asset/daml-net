// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using Daml.Ledger.Client.Reactive;
using log4net;

using Identifier = Com.Daml.Ledger.Api.Data.Identifier;
using Transaction = Com.Daml.Ledger.Api.Data.Transaction;
using Event = Com.Daml.Ledger.Api.Data.IEvent;
using Command = Com.Daml.Ledger.Api.Data.Command;
using CreatedEvent = Com.Daml.Ledger.Api.Data.CreatedEvent;

namespace Daml.Ledger.QuickStart.Reactive
{
    public class PingPongProcessor
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(PingPongReactiveMain));

        private static readonly string[] PINGPONG_PARAMETERS = {"sender", "receiver", "count"};

        private readonly string _party;
        private readonly string _ledgerId;
        private ILedgerClient _client;

        private readonly Identifier _pingIdentifier;
        private readonly Identifier _pongIdentifier;

        public PingPongProcessor(string party, ILedgerClient client, Identifier pingIdentifier, Identifier pongIdentifier)
        {
            _party = party;
            _ledgerId = client.LedgerId;
            _client = client;
            _pingIdentifier = pingIdentifier;
            _pongIdentifier = pongIdentifier;
        }

        public void RunIndefinitely()
        {
            //// assemble the request for the transaction stream
            //_logger.Info($"{_party} starts reading transactions.");

            //var filter = new TransactionFilter();
            //filter.FiltersByParty.Add(_party, new Filters());

            //IObservable<Transaction> transactions = _client.TransactionClient.GetTransactions(new LedgerOffset {Boundary = LedgerOffset.Types.LedgerBoundary.LedgerEnd},
            //    filter, true);
            //transactions.ForEach(ProcessTransaction);
        }

        /// <summary>
        /// Processes a transaction and sends the resulting commands to the Command Submission Service
        /// </summary>
        /// <param name="tx">the Transaction to process</param>
        private void ProcessTransaction(Transaction tx)
        {
//            var exerciseCommands = tx.Events.AsEnumerable().Where(e => e.EventCase == Event.EventOneofCase.Created).Select(e => e.Created).ToDictionary(e => tx.WorkflowId);
        }

        /// <summary>
        /// For each CreatedEvent where the receiver is
        /// the current party, exercise the Pong choice of Ping contracts, or the Ping
        /// choice of Pong contracts.
        /// </summary>
        /// <param name="workflowId">the workflow the event is part of</param>
        /// <param name="createdEvent">the CreatedEvent to process</param>
        /// <returns>null if this event doesn't trigger any action for this PingPongProcessor's party</returns>
        private IEnumerable<Command> ProcessEvent(string workflowId, CreatedEvent createdEvent)
        {
            Identifier template = createdEvent.TemplateId;

            bool isPing = template.Equals(_pingIdentifier);
            bool isPong = template.Equals(_pongIdentifier);

            if (!isPing && !isPong)
                return new List<Command>();

            return null;
        }
    }
}
