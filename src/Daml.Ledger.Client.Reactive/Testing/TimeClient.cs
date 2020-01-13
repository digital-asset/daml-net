// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Grpc.Core;

namespace Daml.Ledger.Client.Reactive.Testing
{
    using System;
    using System.Reactive.Concurrency;
    using System.Reactive.Linq;
    using Com.Daml.Ledger.Api.Util;
    using Util;
    using Google.Protobuf.WellKnownTypes;

    using Single = Com.Daml.Ledger.Api.Util.Single;

    public class TimeClient
    {
        private readonly Client.Testing.ITimeClient _timeClient;
        private readonly IScheduler _scheduler;

        public TimeClient(string ledgerId, Channel channel, Optional<string> accessToken, IScheduler scheduler = null)
        {
            _timeClient = new Client.Testing.TimeClient(ledgerId, channel, accessToken.Reduce((string) null));
            _scheduler = scheduler;
        }

        public Single<Empty> SetTime(DateTime currentTime, DateTime newTime, Optional<string> accessToken = null)
        {
            return Single.Just(_timeClient.SetTime(currentTime, newTime, accessToken?.Reduce((string) null)));
        }

        public IObservable<DateTimeOffset> GetTime(Optional<string> accessToken = null)
        {
            return _timeClient.GetTime(accessToken?.Reduce((string) null)).CreateAsyncObservable(_scheduler).Select(r => r.CurrentTime.ToDateTimeOffset());
        }
    }
}
