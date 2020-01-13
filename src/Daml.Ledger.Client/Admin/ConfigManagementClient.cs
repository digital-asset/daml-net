// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Daml.Ledger.Client.Auth.Client;

namespace Daml.Ledger.Client.Admin
{
    using System;
    using System.Threading.Tasks;
    using Grpc.Core;
    using Com.DigitalAsset.Ledger.Api.V1.Admin;
    using Google.Protobuf.WellKnownTypes;

    public class ConfigManagementClient : IConfigManagementClient
    {
        private readonly ClientStub<ConfigManagementService.ConfigManagementServiceClient> _configManagementService;

        public ConfigManagementClient(Channel channel, string accessToken)
        {
            _configManagementService = new ClientStub<ConfigManagementService.ConfigManagementServiceClient>(new Com.DigitalAsset.Ledger.Api.V1.Admin.ConfigManagementService.ConfigManagementServiceClient(channel), accessToken);
        }

        public (TimeModel, long) GetTimeModel(string accessToken = null)
        {
            var response = _configManagementService.WithAccess(accessToken).DispatchRequest(new GetTimeModelRequest(), (c, r, co) => c.GetTimeModel(r, co), (c, r) => c.GetTimeModel(r));

            return (response.TimeModel, response.ConfigurationGeneration);
        }

        public async Task<(TimeModel, long)> GetTimeModelAsync(string accessToken = null)
        {
            var response = await _configManagementService.WithAccess(accessToken).DispatchRequest(new GetTimeModelRequest(), (c, r, co) => c.GetTimeModelAsync(r, co), (c, r) => c.GetTimeModelAsync(r));

            return (response.TimeModel, response.ConfigurationGeneration);
        }

        public long SetTimeModel(string submissionId, long configurationGeneration, DateTime maximumRecordTime, TimeModel newTimeModel, string accessToken = null)
        {
            var request = new SetTimeModelRequest { SubmissionId = submissionId, ConfigurationGeneration = configurationGeneration, MaximumRecordTime = Timestamp.FromDateTime(maximumRecordTime), NewTimeModel = newTimeModel };

            return _configManagementService.WithAccess(accessToken).DispatchRequest(request, (c, r, co) => c.SetTimeModel(r, co), (c, r) => c.SetTimeModel(r)).ConfigurationGeneration;
        }

        public async Task<long> SetTimeModelAsync(string submissionId, long configurationGeneration, DateTime maximumRecordTime, TimeModel newTimeModel, string accessToken = null)
        {
            var request = new SetTimeModelRequest { SubmissionId = submissionId, ConfigurationGeneration = configurationGeneration, MaximumRecordTime = Timestamp.FromDateTime(maximumRecordTime), NewTimeModel = newTimeModel };

            var response = await _configManagementService.WithAccess(accessToken).DispatchRequest(request, (c, r, co) => c.SetTimeModelAsync(r, co), (c, r) => c.SetTimeModelAsync(r));
                
            return response.ConfigurationGeneration;
        }
    }
}
