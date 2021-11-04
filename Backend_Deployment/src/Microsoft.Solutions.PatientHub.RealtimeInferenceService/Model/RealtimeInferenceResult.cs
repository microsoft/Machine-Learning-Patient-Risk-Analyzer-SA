// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.RealtimeInferenceService.Model
{
    public class RealtimeInferenceResult
    {
        public double predictions { get; set; }
        public dynamic[,] raw_local_importance_values { get; set; }
    }
}
