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
    public class AdmissionSourceService : SQLEntityCollectionBase<AdmissionSource>
    {
        private static IEnumerable<AdmissionSource> admissionSources;

        public AdmissionSourceService(string DataConnectionString, string CollectionName, string ContainerName = "") : base(DataConnectionString, CollectionName, ContainerName)
        {
            putCache();
        }

        public IEnumerable<AdmissionSource> GetAdmissionSources()
        {
            return AdmissionSourceService.admissionSources;
        }

        public AdmissionSource GetAdmissionSource(int Id)
        {
            return admissionSources.Where(x => x.Id == Id).SingleOrDefault();
        }

        private void putCache()
        {
            if (AdmissionSourceService.admissionSources == null)
                AdmissionSourceService.admissionSources = this.EntityCollection.GetAllAsync().GetAwaiter().GetResult();
        }
    }
}
