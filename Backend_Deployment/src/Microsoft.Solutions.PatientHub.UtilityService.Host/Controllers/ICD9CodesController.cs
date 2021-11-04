// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.UtilityService.Host.Controllers
{
    public class ICD9CodesController : Controller
    {
        private ICD9CodeService _ICD9CodeService;

        public ICD9CodesController(ICD9CodeService iCD9CodeService)
        {
            _ICD9CodeService = iCD9CodeService;
        }

        [HttpGet]
        [Route("ICD9Codes")]
        public ActionResult<IEnumerable<string>> GetDescriptions()
        {
            return Ok(_ICD9CodeService.GetDescriptions());
        }

        [HttpGet]
        [Route("ICD9Codes/{Code}")]
        public ActionResult<IEnumerable<string>> GetDescription(string Code)
        {
            var result = _ICD9CodeService.GetDescription(Code);
            return string.IsNullOrEmpty(result) ? NotFound() : Ok(result);
         }

      
    }
}
