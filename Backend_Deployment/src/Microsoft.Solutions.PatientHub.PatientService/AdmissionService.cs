// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Solutions.PatientHub.PatientService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Solutions.CosmosDB.SQL;


namespace Microsoft.Solutions.PatientHub.PatientService
{
    public class AdmissionService : SQLEntityCollectionBase<Admission>
    {
        private static IEnumerable<Admission> admissionTypes;

        public AdmissionService(string DataConnectionString, string CollectionName, string ContainerName = "") : base(DataConnectionString, CollectionName, ContainerName)
        {
            putCache();
        }

        public IEnumerable<Admission> GetAdmissionTypes()
        {
            return AdmissionService.admissionTypes;
        }

        public Admission GetAdmissionType(int Id)
        {
            return AdmissionService.admissionTypes.Where(x => x.Id == Id).FirstOrDefault();
        }


        private void putCache()
        {
            if (AdmissionService.admissionTypes is null)
                admissionTypes = this.EntityCollection.GetAllAsync().GetAwaiter().GetResult();
        }

    }

    
}
