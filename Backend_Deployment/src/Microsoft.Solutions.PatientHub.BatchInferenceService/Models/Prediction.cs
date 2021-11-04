// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Solutions.CosmosDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.BatchInferenceService.Models
{
    public class Prediction : CosmosDBEntityBase
    {
        public string patientId { get; set; }
        [JsonProperty("Prediction")]public double prediction { get; set; }
    }
}
