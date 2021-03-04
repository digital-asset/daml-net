// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Daml.Ledger.Client.Testing
{
    using Com.DigitalAsset.Ledger.Api.V1.Testing;
    using Daml.Ledger.Client.Auth.Client;

    public class TimeClient : ITimeClient
    {
        private readonly string _ledgerId;
        private readonly ClientStub<TimeService.TimeServiceClient> _timeClient;

        public TimeClient(string ledgerId, Channel channel, string accessToken)
        {
            _ledgerId = ledgerId;
            _timeClient = new ClientStub<TimeService.TimeServiceClient>(new TimeService.TimeServiceClient(channel), accessToken);
        }

        public IAsyncEnumerator<GetTimeResponse> GetTime(string accessToken = null)
        {
            var response = _timeClient.WithAccess(accessToken).Dispatch(new GetTimeRequest { LedgerId = _ledgerId }, (c, r, co) => c.GetTime(r, co));
            return response.ResponseStream;
        }

        public IEnumerable<GetTimeResponse> GetTimeSync(string accessToken = null)
        {
            using (var stream = GetTime(accessToken))
            {
                while (stream.MoveNext().Result)
                {
                    yield return stream.Current;
                }
            }
        }

        public void SetTime(DateTime currentTime, DateTime newTime, string accessToken = null)
        {
            if (currentTime >= newTime)
                throw new SetTimeException(currentTime, newTime);

            var request = new SetTimeRequest { LedgerId = _ledgerId, CurrentTime = Timestamp.FromDateTime(currentTime), NewTime = Timestamp.FromDateTime(newTime) };
            _timeClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.SetTime(r, co));
        }

        public async Task SetTimeAsync(DateTime currentTime, DateTime newTime, string accessToken = null)
        {
            var request = new SetTimeRequest { LedgerId = _ledgerId, CurrentTime = Timestamp.FromDateTime(currentTime), NewTime = Timestamp.FromDateTime(newTime) };
            await _timeClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.SetTimeAsync(r, co));
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
