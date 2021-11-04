// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Solutions.CosmosDB;
using Microsoft.Solutions.PatientHub.UtilityService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.BatchInferenceService.Models
{
    public class Explanation : CosmosDBEntityBase
    {
        public double Score { get; set; }
        public string patientId { get; set; }
        public string Feature { get; set; }
        public ColumnLookupValue NamingMap { get; set;}
    }
}
