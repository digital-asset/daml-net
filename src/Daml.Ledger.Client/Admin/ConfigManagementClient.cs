// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Admin
{
    using System;
    using System.Threading.Tasks;
    using Grpc.Core;
    using Com.DigitalAsset.Ledger.Api.V1.Admin;
    using Google.Protobuf.WellKnownTypes;

    public class ConfigManagementClient : IConfigManagementClient
    {
        private readonly ConfigManagementService.ConfigManagementServiceClient _configManagementClient;

        public ConfigManagementClient(Channel channel)
        {
            _configManagementClient = new ConfigManagementService.ConfigManagementServiceClient(channel);
        }

        public (TimeModel, long) GetTimeModel()
        {
            var response = _configManagementClient.GetTimeModel(new GetTimeModelRequest());
            return (response.TimeModel, response.ConfigurationGeneration);
        }

        public async Task<(TimeModel, long)> GetTimeModelAsync()
        {
            var response = await _configManagementClient.GetTimeModelAsync(new GetTimeModelRequest());
            return (response.TimeModel, response.ConfigurationGeneration);
        }

        public long SetTimeModel(string submissionId, long configurationGeneration, DateTime maximumRecordTime, TimeModel newTimeModel)
        {
            var request = new SetTimeModelRequest { SubmissionId = submissionId, ConfigurationGeneration = configurationGeneration, MaximumRecordTime = Timestamp.FromDateTime(maximumRecordTime), NewTimeModel = newTimeModel };

            return _configManagementClient.SetTimeModel(request).ConfigurationGeneration;
        }

        public async Task<long> SetTimeModelAsync(string submissionId, long configurationGeneration, DateTime maximumRecordTime, TimeModel newTimeModel)
        {
            var request = new SetTimeModelRequest { SubmissionId = submissionId, ConfigurationGeneration = configurationGeneration, MaximumRecordTime = Timestamp.FromDateTime(maximumRecordTime), NewTimeModel = newTimeModel };

            var response = await _configManagementClient.SetTimeModelAsync(request);
                
            return response.ConfigurationGeneration;
        }
    }
}
