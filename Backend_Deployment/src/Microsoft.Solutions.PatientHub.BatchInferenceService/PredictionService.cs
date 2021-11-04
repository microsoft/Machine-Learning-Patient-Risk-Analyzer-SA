// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Solutions.CosmosDB;
using Microsoft.Solutions.CosmosDB.SQL;
using Microsoft.Solutions.PatientHub.BatchInferenceService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.BatchInferenceService
{
    public class PredictionService : SQLEntityCollectionBase<Prediction>
    {
        public PredictionService(string DataConnectionString, string CollectionName, string ContainerName = "") : base(DataConnectionString, CollectionName, ContainerName)
        {
        }

        public async Task<IEnumerable<Prediction>> GetAllPredictions()
        {
            return await this.EntityCollection.GetAllAsync();
        }

        public async Task<Prediction> GetPrediction(string patientId)
        {
            return await this.EntityCollection.FindAsync(new GenericSpecification<Prediction>(x => x.patientId == patientId));
        }

        public async IAsyncEnumerable<Prediction> GetPredictionsByPatientIds(string[] patientIds)
        {
            foreach (var patientId in patientIds)
            {
                yield return await this.EntityCollection.FindAsync(new GenericSpecification<Prediction>(x => x.patientId == patientId));
            }
        }
    }
}
