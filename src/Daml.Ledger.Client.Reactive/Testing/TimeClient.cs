// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace Daml.Ledger.Client.Reactive.Testing
{
    using Daml.Ledger.Client.Testing;
    using Daml.Ledger.Api.Data.Util;
    using Util;

    public class TimeClient
    {
        private readonly ITimeClient _timeClient;
        private readonly IScheduler _scheduler;

        public TimeClient(ITimeClient timeClient, IScheduler scheduler = null)
        {
            _timeClient = timeClient;
            _scheduler = scheduler;
        }

        public void SetTime(DateTime currentTime, DateTime newTime, Optional<string> accessToken = null)
        {
            _timeClient.SetTime(currentTime, newTime, accessToken?.Reduce((string) null));
        }

        public IObservable<DateTimeOffset> GetTime(Optional<string> accessToken = null)
        {
            return _timeClient.GetTime(accessToken?.Reduce((string) null)).CreateAsyncObservable(_scheduler).Select(r => r.CurrentTime.ToDateTimeOffset());
        }
    }
}
