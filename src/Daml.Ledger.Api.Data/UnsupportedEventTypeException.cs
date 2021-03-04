// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Daml.Ledger.Api.Data 
{
    public class UnsupportedEventTypeException : Exception
    {
        public UnsupportedEventTypeException(string eventStr)
         : base($"Unsupported event {eventStr}")
        { }
    }
} 
