// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.AspNetCore.Mvc;
using Microsoft.Solutions.PatientHub.BatchInferenceService.Models;
using Microsoft.Solutions.PatientHub.UtilityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.BatchInferenceService.Host.Controllers
{
    public class ExplanationController : Controller
    {
        private ExplanationService _explanationService;
        private ColumnLookupValueService _columnLookupValueService;

        public ExplanationController(ColumnLookupValueService columnLookupValueService,ExplanationService explanationService)
        {
            _explanationService = explanationService;
            _columnLookupValueService = columnLookupValueService;
        }

        [HttpGet]
        [Route("Explanation/Patients/{PatientID}")]
        public async Task<ActionResult<IEnumerable<Explanation>>> GetTop5Explanations(string PatientID)
        {
            var result = await _explanationService.GetTop5Explanations(_columnLookupValueService ,PatientID);

            return result is null ? NotFound() : Ok(result) ;
        }
    }
}
