// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Solutions.CosmosDB.SQL;
using Microsoft.Solutions.PatientHub.UtilityService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Solutions.PatientHub.UtilityService
{
    public class DischargeDispositionService : SQLEntityCollectionBase<DischargeDisposition>
    {
        private static IEnumerable<DischargeDisposition> dischargeDispositions;

        public DischargeDispositionService(string DataConnectionString, string CollectionName, string ContainerName = "") : base(DataConnectionString, CollectionName, ContainerName)
        {
            putCache();
        }

        public string GetDescription(int Id)
        {
            var result = DischargeDispositionService.dischargeDispositions.Where(x => x.Id == Id).FirstOrDefault();
            return ((result is null) || (result.Description is null)) ? "" : result.Description;
        
        }

        public IEnumerable<string> GetDescriptions()
        {
            return DischargeDispositionService.dischargeDispositions.Select(x => x.Description).ToArray();
        }

        private void putCache()
        {
            if(DischargeDispositionService.dischargeDispositions is null)
            {
                DischargeDispositionService.dischargeDispositions =
                    this.EntityCollection.GetAllAsync().GetAwaiter().GetResult();
            }
        }
    }
}
