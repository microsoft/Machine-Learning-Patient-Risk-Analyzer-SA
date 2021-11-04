// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Solutions.CosmosDB;
using Microsoft.Solutions.CosmosDB.SQL;
using Microsoft.Solutions.PatientHub.BatchInferenceService.Models;
using Microsoft.Solutions.PatientHub.UtilityService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.BatchInferenceService
{
    public class ExplanationService : SQLEntityCollectionBase<Explanation>
    {
        public ExplanationService(string DataConnectionString, string CollectionName, string ContainerName = "") : base(DataConnectionString, CollectionName, ContainerName)
        {
        }

        public async Task<IEnumerable<Explanation>> GetTop5Explanations(ColumnLookupValueService columnLookupValueService, string patientId)
        {
            var result = (await this.EntityCollection.FindAllAsync(new GenericSpecification<Explanation>((x => x.patientId == patientId), (x => x.Score), Order.Desc)));

            var lstExplanation = new List<Explanation>();

            //Get Disticted Resultset
            foreach (var item in result)
            {
                var existingItem = lstExplanation.Find(x => x.Feature == item.Feature);
                //unusual value need to removed..
                if((existingItem is null) && (item.Feature != "num_lab_procedures")) lstExplanation.Add(item);
                if (lstExplanation.Count == 5) break;
            }
            
            
            lstExplanation.ForEach(x => x.NamingMap = columnLookupValueService.GetValue(x.Feature));
          


            return lstExplanation;
        }
    }
}
