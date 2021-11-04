// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Solutions.PatientHub.RealtimeInferenceService.Model;
using Microsoft.Solutions.PatientHub.UtilityService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.RealtimeInferenceService.Host.Controllers
{
    public class RealtimeInferenceController : Controller
    {
        private ColumnLookupValueService _columnLookupValueService;
        private RealtimeInference _realtimeInference;
        private ColumnNameMapService _columnNameMapService;

        public RealtimeInferenceController(ColumnNameMapService columnNameMapService ,ColumnLookupValueService columnLookupValueService, RealtimeInference realtimeInference)
        {
            _columnNameMapService = columnNameMapService;
            _columnLookupValueService = columnLookupValueService;
            _realtimeInference = realtimeInference;

        }

        [HttpPost("/RealtimeInference/Patient")]
        public async Task<ActionResult<Prediction>> GetTop5RealtimeInference([FromBody]PatientData patientData)
        {
            Console.WriteLine(JsonConvert.SerializeObject(patientData));

            var result = 
                await _realtimeInference.GetTop5RealtimeInference(_columnNameMapService,_columnLookupValueService, patientData);

            return result is null ? NotFound() : Ok(result);
        }

    }
}
