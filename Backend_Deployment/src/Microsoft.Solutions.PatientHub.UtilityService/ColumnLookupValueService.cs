// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Solutions.CosmosDB.SQL;
using Microsoft.Solutions.PatientHub.UtilityService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Microsoft.Solutions.PatientHub.UtilityService
{
    public class ColumnLookupValueService : SQLEntityCollectionBase<ColumnLookupValue>
    {
        private static IEnumerable<ColumnLookupValue> columnLookupValues;
       
        public ColumnLookupValueService(string DataConnectionString, string CollectionName, string ContainerName = "") : base(DataConnectionString, CollectionName, ContainerName)
        {
            putCache();
        }

        public IEnumerable<ColumnLookupValue> GetAllValues()
        {
            return ColumnLookupValueService.columnLookupValues;
        }

        public ColumnLookupValue GetValue(int Id)
        {
            return ColumnLookupValueService.columnLookupValues.Where(x => x.Id == Id).FirstOrDefault();
        }

        public ColumnLookupValue GetValue(string DestinationColumn)
        {
            return ColumnLookupValueService.columnLookupValues.Where(x => x.DestinationColumn == DestinationColumn).FirstOrDefault();
        }

        private void putCache()
        {
            if (ColumnLookupValueService.columnLookupValues is null)
            {
                ColumnLookupValueService.columnLookupValues = 
                    this.EntityCollection.GetAllAsync().GetAwaiter().GetResult();
            }
        }

    }
}
