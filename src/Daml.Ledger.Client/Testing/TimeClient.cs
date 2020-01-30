// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1.Testing;
    using Google.Protobuf.WellKnownTypes;
    using Grpc.Core;

    public class TimeClient : ITimeClient
    {
        private readonly string _ledgerId;
        private readonly TimeService.TimeServiceClient _timeClient;

        public TimeClient(string ledgerId, Channel channel)
        {
          _ledgerId = ledgerId;
           _timeClient = new TimeService.TimeServiceClient(channel);
        }

        public IAsyncEnumerator<GetTimeResponse> GetTime()
        {
            var response = _timeClient.GetTime(new GetTimeRequest { LedgerId = _ledgerId });
            return response.ResponseStream;
        }

        public IEnumerable<GetTimeResponse> GetTimeSync()
        {
            using (var stream = GetTime())
            {
                while (stream.MoveNext().Result)
                {
                    yield return stream.Current;
                }
            }
        }

        public void SetTime(DateTime currentTime, DateTime newTime)
        {
              if (currentTime >= newTime)
                throw new SetTimeException(currentTime, newTime);

            var request = new SetTimeRequest { LedgerId = _ledgerId, CurrentTime = Timestamp.FromDateTime(currentTime), NewTime = Timestamp.FromDateTime(newTime) };
             _timeClient.SetTime(request);
        }

        public async Task SetTimeAsync(DateTime currentTime, DateTime newTime)
        {
            var request = new SetTimeRequest { LedgerId = _ledgerId, CurrentTime = Timestamp.FromDateTime(currentTime), NewTime = Timestamp.FromDateTime(newTime) };
            await _timeClient.SetTimeAsync(request);
        }
        
        private class SetTimeException : Exception
        {
            public SetTimeException(DateTimeOffset currentTime, DateTimeOffset newTime)
                : base($"Cannot set a new time smaller or equal to the current one. That new time tried is {newTime.ToString()} but the current one is {currentTime.ToString()}")
            {
            }
        }
    }
}
