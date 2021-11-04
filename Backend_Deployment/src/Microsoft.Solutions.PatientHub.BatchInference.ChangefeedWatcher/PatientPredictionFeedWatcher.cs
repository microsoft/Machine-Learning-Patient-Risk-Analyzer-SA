// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Solutions.CosmosDB;
using Microsoft.Solutions.CosmosDB.SQL;
using Microsoft.Solutions.CosmosDB.SQL.ChangeFeed;
using Microsoft.Solutions.PatientHub.BatchInferenceService.Models;
using Microsoft.Solutions.PatientHub.PatientService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.BatchInference.ChangefeedWatcher
{
    public class PatientPredictionFeedWatcher : Watcher<Prediction>
    {
        private static PatientService.PatientService _patients;

        public PatientPredictionFeedWatcher(IConfiguration configuration) : base(configuration["Values:DBConnectionString"], configuration["Values:DatabaseName"], configuration["Values:ContainerName"])
        {
            ////Data Access for Patient Collection 
            PatientPredictionFeedWatcher._patients = new PatientService.PatientService(configuration["Values:DBConnectionString"], configuration["Values:DatabaseName"], "Patient");
        }

        /// <summary>
        /// Catch the Changes in Prediction Container
        /// </summary>
        /// <param name="changes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task OnChangedFeedDataSets(IReadOnlyCollection<Prediction> changes, CancellationToken cancellationToken)
        {
            foreach (var item in changes)
            {
                await PatientPredictionFeedWatcher._patients.UpdateScore(item.patientId.Trim(), (decimal)item.prediction);
#if DEBUG
                Console.WriteLine($"Updated changed item - {JsonConvert.SerializeObject(item)}");
#endif
            }
        }
       
    }
}
