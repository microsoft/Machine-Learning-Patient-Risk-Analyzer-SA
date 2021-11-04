// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.AspNetCore.Mvc;
using Microsoft.Solutions.PatientHub.UtilityService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.UtilityService.Host.Controllers
{
    public class ColumnNameMapsController : Controller
    {
        readonly private ColumnNameMapService _columnNameMapService;

        public ColumnNameMapsController(ColumnNameMapService columnNameMapService)
        {
            _columnNameMapService = columnNameMapService;
        }

        [HttpGet]
        [Route("ColumnNameMaps")]
        public ActionResult<IEnumerable<ColumnNameMap>> Index()
        {
            return Ok(_columnNameMapService.GetAllValues());
        }

        [HttpGet]
        [Route("ColumnNameMaps/{Id}")]
        public ActionResult<ColumnNameMap> GetValue(int Id)
        {
            var result = _columnNameMapService.GetValue(Id);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
