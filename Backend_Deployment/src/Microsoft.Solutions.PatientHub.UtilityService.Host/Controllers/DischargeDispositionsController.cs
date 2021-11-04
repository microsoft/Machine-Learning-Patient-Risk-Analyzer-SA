// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.UtilityService.Host.Controllers
{
    public class DischargeDispositionsController : Controller
    {
        readonly private DischargeDispositionService _dischargeDispositionService;

        public DischargeDispositionsController(DischargeDispositionService dischargeDispositionService)
        {
            _dischargeDispositionService = dischargeDispositionService;
        }


        [HttpGet]
        [Route("DischargeDispositions")]
        public ActionResult<IEnumerable<string>> Index()
        {
            return Ok(_dischargeDispositionService.GetDescriptions());
        }

        [HttpGet]
        [Route("DischargeDispositions/{Id}")]
        public ActionResult<string> GetValue(int Id)
        {
            var result = _dischargeDispositionService.GetDescription(Id);
            return string.IsNullOrEmpty(result) ? NotFound() : Ok(result);
        }
    }
}
