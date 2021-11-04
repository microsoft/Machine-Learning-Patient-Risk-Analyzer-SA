// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Solutions.CosmosDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.UtilityService.Models
{
    public class ICD9Code : CosmosDBEntityBase
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
