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
    public class AdmissionTypesController : Controller
    {
        readonly private AdmissionService _admissionTypeService;

        public AdmissionTypesController(AdmissionService admissionTypeService)
        {
            _admissionTypeService = admissionTypeService;
        }

        [HttpGet]
        [Route("AdmissionTypes/{TypeID}")]
        public ActionResult<Admission> Get(int TypeID)
        {
            var result = _admissionTypeService.GetAdmissionType(TypeID);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
