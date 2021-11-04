// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using Microsoft.Solutions.CosmosDB.SQL;
using Microsoft.Solutions.PatientHub.UtilityService.Models;
using System.Linq;

namespace Microsoft.Solutions.PatientHub.UtilityService
{
    public class ColumnNameMapService : SQLEntityCollectionBase<ColumnNameMap>
    {
        private static IEnumerable<ColumnNameMap> columnNameMaps;

        public ColumnNameMapService(string DataConnectionString, string CollectionName, string ContainerName = "") : base(DataConnectionString, CollectionName, ContainerName)
        {
            putCache();
        }

        public IEnumerable<ColumnNameMap> GetAllValues()
        {
            return ColumnNameMapService.columnNameMaps;
        }

        public ColumnNameMap GetValue(string ColumnName)
        {
            return ColumnNameMapService.columnNameMaps.Where(x => x.SourceColumn == ColumnName).FirstOrDefault();
        }

        public ColumnNameMap GetValue(int Id)
        {
            return ColumnNameMapService.columnNameMaps.Where(x => x.Id == Id).FirstOrDefault();
        }

        private void putCache()
        {
            if (ColumnNameMapService.columnNameMaps is null)
            {
                ColumnNameMapService.columnNameMaps = this.EntityCollection.GetAllAsync().GetAwaiter().GetResult();
            }
        }
    }
}
