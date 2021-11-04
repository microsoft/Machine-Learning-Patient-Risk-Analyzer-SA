// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Solutions.PatientHub.UtilityService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.UtilityService.Host.Controllers
{
    public class ColumnLookupValuesController : Controller
    {
        private ColumnLookupValueService _columnLookupValueService;

        public ColumnLookupValuesController(ColumnLookupValueService columnLookupValueService)
        {
            _columnLookupValueService = columnLookupValueService;
        }

        [HttpGet]
        [Route("ColumnLookupValues")]
        public ActionResult Index()
        {
            return Ok(_columnLookupValueService.GetAllValues());
        }

        [HttpGet]
        [Route("ColumnLookupValues/{Id}")]
        public ActionResult<ColumnLookupValue> GetValue(int Id)
        {
            var result = _columnLookupValueService.GetValue(Id);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
