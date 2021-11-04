// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Solutions.PatientHub.UtilityService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.RealtimeInferenceService.Model
{
    public class Explanation
    {
        public double Score { get; set; }
        public dynamic Value { get; set; }
        public string Feature { get; set; }
        public ColumnLookupValue NamingMap { get; set; }
    }
}
