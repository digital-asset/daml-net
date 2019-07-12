// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1.Testing;
    using Google.Protobuf.WellKnownTypes;
    using Grpc.Core;

    public class TimeClient : ITimeClient
    {
        private readonly TimeService.TimeServiceClient timeClient;

        public TimeClient(Channel channel)
        {
            this.timeClient = new TimeService.TimeServiceClient(channel);
        }

        public IAsyncEnumerator<GetTimeResponse> GetTime(string ledgerId)
        {
            var request = new GetTimeRequest { LedgerId = ledgerId };
            var call = this.timeClient.GetTime(request);
            return call.ResponseStream;
        }

        public IEnumerable<GetTimeResponse> GetTimeSync(string ledgerId)
        {
            using (var stream = this.GetTime(ledgerId))
            {
                while (stream.MoveNext().Result)
                {
                    yield return stream.Current;
                }
            }
        }

        public void SetTime(string ledgerId, DateTime currentTime, DateTime newTime)
        {
            var request = new SetTimeRequest { LedgerId = ledgerId, CurrentTime = Timestamp.FromDateTime(currentTime), NewTime = Timestamp.FromDateTime(newTime) };
            this.timeClient.SetTime(request);
        }

        public async Task SetTimeAsync(string ledgerId, DateTime currentTime, DateTime newTime)
        {
            var request = new SetTimeRequest { LedgerId = ledgerId, CurrentTime = Timestamp.FromDateTime(currentTime), NewTime = Timestamp.FromDateTime(newTime) };
            await this.timeClient.SetTimeAsync(request);
        }
    }
}
