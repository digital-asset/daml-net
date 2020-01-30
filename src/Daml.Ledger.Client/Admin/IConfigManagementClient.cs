// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Daml.Ledger.Client.Admin
{
    using System;
    using System.Threading.Tasks;
    using Com.DigitalAsset.Ledger.Api.V1.Admin;

    public interface IConfigManagementClient
    {
        (TimeModel, long) GetTimeModel();

        Task<(TimeModel, long)> GetTimeModelAsync();

        long SetTimeModel(string submissionId, long configurationGeneration, DateTime maximumRecordTime, TimeModel newTimeModel);

        Task<long> SetTimeModelAsync(string submissionId, long configurationGeneration, DateTime maximumRecordTime, TimeModel newTimeModel);
    }
}
