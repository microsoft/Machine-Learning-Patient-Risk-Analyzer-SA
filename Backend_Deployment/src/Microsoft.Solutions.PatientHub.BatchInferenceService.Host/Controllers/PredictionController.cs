// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.AspNetCore.Mvc;
using Microsoft.Solutions.PatientHub.BatchInferenceService;
using Microsoft.Solutions.PatientHub.BatchInferenceService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.BatchInferenceService.Host.Controllers
{
    public class PredictionController : Controller
    {
        private PredictionService _predictionService;

        public PredictionController(PredictionService predictionService)
        {
            _predictionService = predictionService;
        }

        [HttpGet("Prediction/")]
        public async Task<ActionResult<IEnumerable<Prediction>>> GetAllPredictions()
        {
            var result = await _predictionService.GetAllPredictions();
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("Prediction/Patients/{PatientId}")]
        //[Route(/*"Patients/{PatientId}"*/)]
        public async Task<ActionResult<IEnumerable<Prediction>>> GetPrediction(string PatientId)
        {
            var result = await _predictionService.GetPrediction(PatientId);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost("Prediction/Patients/")]
        //[Route("Patients/")]
        public ActionResult<IEnumerable<Prediction>> GetPredictions([FromBody] string[] PatientIds)
        {
            var result = _predictionService.GetPredictionsByPatientIds(PatientIds);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
