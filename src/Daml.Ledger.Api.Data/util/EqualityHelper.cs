// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Daml.Ledger.Api.Data.Util
{
    public static class EqualityHelper
    {
        public static bool Compare<T, TBase>(this T lhs, TBase obj, Func<T, bool> comparer) where T : class, TBase
        {
            if (ReferenceEquals(lhs, obj))
                return true;

            var rhs = obj as T;
            if (rhs is null)
                return false;

            return comparer(rhs);
        }

        public static bool Compare<T>(this T lhs, T rhs) where T : class
        {
            return ReferenceEquals(lhs, rhs) || (!(lhs is null) && lhs.Equals(rhs));
        }
    }
}
