// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Threading.Tasks;

namespace Daml.Ledger.Client.Admin
{
    using Com.DigitalAsset.Ledger.Api.V1.Admin;

    public interface IConfigManagementClient
    {
        (TimeModel, long) GetTimeModel(string accessToken = null);

        Task<(TimeModel, long)> GetTimeModelAsync(string accessToken = null);

        long SetTimeModel(string submissionId, long configurationGeneration, DateTime maximumRecordTime, TimeModel newTimeModel, string accessToken = null);

        Task<long> SetTimeModelAsync(string submissionId, long configurationGeneration, DateTime maximumRecordTime, TimeModel newTimeModel, string accessToken = null);
    }
}
