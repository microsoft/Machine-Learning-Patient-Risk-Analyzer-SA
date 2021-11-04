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
    public class ColumnNameMap : CosmosDBEntityBase
    {
        public int Id { get; set; }
        public string SourceSchema { get; set; }
        public string SourceTable { get; set; }
        public string SourceColumn { get; set; }
        public string DestinationSchema { get; set; }
        public string DestinationTable { get; set; }
        public string DestinationColumn { get; set; }
        public string Description { get; set; }
    }
}
