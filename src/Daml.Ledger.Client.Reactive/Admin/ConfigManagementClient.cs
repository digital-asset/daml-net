// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Daml.Ledger.Client.Reactive.Admin
{
    using Daml.Ledger.Client.Admin;
    using Daml.Ledger.Api.Data.Util;

    using TimeModel = Com.DigitalAsset.Ledger.Api.V1.Admin.TimeModel;

    public class ConfigManagementClient
    {
        private readonly IConfigManagementClient _configManagementClient;

        public ConfigManagementClient(IConfigManagementClient configManagementClient)
        {
            _configManagementClient = configManagementClient;
        }

        public (TimeModel, long) GetTimeModel(Optional<string> accessToken = null)
        {
            return _configManagementClient.GetTimeModel(accessToken?.Reduce((string) null));
        }

        public long SetTimeModel(string submissionId, long configurationGeneration, DateTime maximumRecordTime, TimeModel newTimeModel, Optional<string> accessToken = null)
        {
            return _configManagementClient.SetTimeModel(submissionId, configurationGeneration, maximumRecordTime, newTimeModel, accessToken?.Reduce((string) null));
        }
    }
}
