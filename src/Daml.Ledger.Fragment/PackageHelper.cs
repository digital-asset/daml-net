// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Com.DigitalAsset.Daml_Lf_1_7.DamlLf1
{
    public static class PackageHelper
    {
        /// <summary>
        /// Resolve the list of dotted names in this module from the list of interned stings
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public static List<string> GetDottedNames(this Package package)
        {
            return package.InternedDottedNames.Aggregate(new List<string>(), (list, idn) => {
                list.Add(idn.SegmentsInternedStr.Aggregate(string.Empty, (s, i) => s + package.InternedStrings[i]));
                return list;
            });
        }

        /// <summary>
        /// Resolve the list of module name_dnames
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public static List<string> GetModuleNameDNames(this Package package)
        {
            return package.GetModuleNameDNames(package.GetDottedNames());
        }

        public static List<string> GetModuleNameDNames(this Package package, List<string> dottedNames)
        {
            return dottedNames.Count == 0 ? new List<string>() : package.Modules.Select(m => dottedNames[m.NameInternedDname]).ToList();
        }
    }
}
