// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.RealtimeInferenceService.Model
{
    public class Prediction
    {
        public double Predictions { get; set; }
        public List<Explanation> Importance_Factors { get; set; }

        public Prediction()
        {
            Importance_Factors = new List<Explanation>();
        }
    }
}
