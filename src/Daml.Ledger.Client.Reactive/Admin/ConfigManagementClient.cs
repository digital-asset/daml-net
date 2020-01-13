// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Grpc.Core;

namespace Daml.Ledger.Client.Reactive.Admin
{
    using System;
    using Single = Com.Daml.Ledger.Api.Util.Single;
    using Com.Daml.Ledger.Api.Util;
    using Com.DigitalAsset.Ledger.Api.V1.Admin;

    public class ConfigManagementClient
    {
        private readonly Client.Admin.IConfigManagementClient _configManagementClient;

        public ConfigManagementClient(Channel channel, Optional<string> accessToken)
        {
            _configManagementClient = new Client.Admin.ConfigManagementClient(channel, accessToken.Reduce((string) null));
        }

        public Com.Daml.Ledger.Api.Util.Single<(TimeModel, long)> GetTimeModel(Optional<string> accessToken = null)
        {
            return Single.Just(_configManagementClient.GetTimeModel(accessToken?.Reduce((string) null)));
        }

        public Com.Daml.Ledger.Api.Util.Single<long> SetTimeModel(string submissionId, long configurationGeneration, DateTime maximumRecordTime, TimeModel newTimeModel, Optional<string> accessToken = null)
        {
            return Single.Just(_configManagementClient.SetTimeModel(submissionId, configurationGeneration, maximumRecordTime, newTimeModel, accessToken?.Reduce((string) null)));
        }
    }
}
