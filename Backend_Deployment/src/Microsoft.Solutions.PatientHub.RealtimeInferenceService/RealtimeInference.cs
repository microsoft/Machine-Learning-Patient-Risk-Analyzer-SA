// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Solutions.PatientHub.RealtimeInferenceService.Model;
using Microsoft.Solutions.PatientHub.UtilityService;
using Microsoft.Solutions.PatientHub.UtilityService.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.RealtimeInferenceService
{
    public class RealtimeInference
    {
        private string mlServiceURL = "";
        private string mlServiceBearerToken = "";

        public RealtimeInference(string MlServiceURL, string MlServicebearerToken)
        {
            mlServiceURL = MlServiceURL;
            mlServiceBearerToken = MlServicebearerToken;
        }
        public async Task<Prediction> GetTop5RealtimeInference(ColumnNameMapService columnNameMapService, ColumnLookupValueService columnLookupValueService,PatientData patientData)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(mlServiceURL);
            httpClient.DefaultRequestHeaders.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", mlServiceBearerToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var jsonRequestParam = JsonConvert.SerializeObject(new PatientData[] { patientData });
            var stringContent = new StringContent(jsonRequestParam, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(mlServiceURL, stringContent);
            response.EnsureSuccessStatusCode();

            var jString = await response.Content.ReadAsStringAsync();

            if (jString.Contains("RDD is empty")) return null;

            var result =  JsonConvert.DeserializeObject<RealtimeInferenceResult>(jString);

            Prediction retPrediction = new Prediction();
            retPrediction.Predictions = result.predictions;

            var _rowCount = 0;

            foreach (var item in result.raw_local_importance_values)
            {
                if (_rowCount > 4) break;

                ColumnLookupValue _columnLookupValue = columnLookupValueService.GetValue(result.raw_local_importance_values[_rowCount, 0]);
                if ((_columnLookupValue is null) || (result.raw_local_importance_values[_rowCount, 0] == "num_lab_procedures")) continue;

                var explanation = new Explanation()
                {
                    Feature = result.raw_local_importance_values[_rowCount, 0],
                    Value = result.raw_local_importance_values[_rowCount, 1],
                    Score = result.raw_local_importance_values[_rowCount, 2],
                    NamingMap = _columnLookupValue
                };

                retPrediction.Importance_Factors.Add(explanation);
                _rowCount++;

            }



            return retPrediction;
        }
    }
}
