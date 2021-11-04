// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Solutions.PatientHub.PatientService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.PatientService.Host.Controllers
{
    public class AdmissionSourcesController : Controller
    {
        readonly private AdmissionSourceService _admissionSourceService;

        public AdmissionSourcesController(AdmissionSourceService admissionSourceService)
        {
            _admissionSourceService = admissionSourceService;
        }

        [HttpGet]
        [Route("AdmissionSources/{AdmissionSourceID}")]
        public ActionResult<AdmissionSource> Get(int AdmissionSourceID)
        {
            var result = _admissionSourceService.GetAdmissionSource(AdmissionSourceID);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
