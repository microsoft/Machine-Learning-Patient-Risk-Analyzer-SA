// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Solutions.CosmosDB.SQL;
using Microsoft.Solutions.PatientHub.PatientService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.PatientService
{
    public class DischargeDispositionService : SQLEntityCollectionBase<DischargeDisposition>
    {
        private static IEnumerable<DischargeDisposition> dischargeDispositions;
        public DischargeDispositionService(string DataConnectionString, string CollectionName, string ContainerName = "") : base(DataConnectionString, CollectionName, ContainerName)
        {
            putCache();
        }

        public IEnumerable<DischargeDisposition> GetDischargeDisposition()
        {
            return DischargeDispositionService.dischargeDispositions;
        }

        public DischargeDisposition GetDischargeDisposition(int Id)
        {
            return DischargeDispositionService.dischargeDispositions.Where(x => x.Id == Id).SingleOrDefault();
        }

        private void putCache()
        {
            if (DischargeDispositionService.dischargeDispositions == null)
                DischargeDispositionService.dischargeDispositions = this.EntityCollection.GetAllAsync().GetAwaiter().GetResult();
        }
    }
}
