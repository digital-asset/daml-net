// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Admin
{
    using System;
    using System.Threading.Tasks;
    using Grpc.Core;
    using Com.DigitalAsset.Ledger.Api.V1.Admin;
    using Daml.Ledger.Client.Auth.Client;
    using Google.Protobuf.WellKnownTypes;

    public class ConfigManagementClient : IConfigManagementClient
    {
        private readonly ClientStub<ConfigManagementService.ConfigManagementServiceClient> _configManagementClient;

        public ConfigManagementClient(Channel channel, string accessToken)
        {
            _configManagementClient = new ClientStub<ConfigManagementService.ConfigManagementServiceClient>(new Com.DigitalAsset.Ledger.Api.V1.Admin.ConfigManagementService.ConfigManagementServiceClient(channel), accessToken);
        }

        public (TimeModel, long) GetTimeModel(string accessToken = null)
        {
            var response = _configManagementClient.WithAccess(accessToken).Dispatch(new GetTimeModelRequest(), (c, r, co) => c.GetTimeModel(r, co));
            return (response.TimeModel, response.ConfigurationGeneration);
        }

        public async Task<(TimeModel, long)> GetTimeModelAsync(string accessToken = null)
        {
            var response = await _configManagementClient.WithAccess(accessToken).Dispatch(new GetTimeModelRequest(), (c, r, co) => c.GetTimeModelAsync(r, co));
            return (response.TimeModel, response.ConfigurationGeneration);
        }

        public long SetTimeModel(string submissionId, long configurationGeneration, DateTime maximumRecordTime, TimeModel newTimeModel, string accessToken = null)
        {
            var request = new SetTimeModelRequest { SubmissionId = submissionId, ConfigurationGeneration = configurationGeneration, MaximumRecordTime = Timestamp.FromDateTime(maximumRecordTime), NewTimeModel = newTimeModel };
            return _configManagementClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.SetTimeModel(r, co)).ConfigurationGeneration;
        }

        public async Task<long> SetTimeModelAsync(string submissionId, long configurationGeneration, DateTime maximumRecordTime, TimeModel newTimeModel, string accessToken = null)
        {
            var request = new SetTimeModelRequest { SubmissionId = submissionId, ConfigurationGeneration = configurationGeneration, MaximumRecordTime = Timestamp.FromDateTime(maximumRecordTime), NewTimeModel = newTimeModel };
            var response = await _configManagementClient.WithAccess(accessToken).Dispatch(request, (c, r, co) => c.SetTimeModelAsync(r, co));
                
            return response.ConfigurationGeneration;
        }
    }
}
