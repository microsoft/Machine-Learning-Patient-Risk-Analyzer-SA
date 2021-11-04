// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Solutions.PatientHub.PatientService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.PatientService.Host.Controllers
{
    public class PatientsController : Controller
    {
        readonly private PatientService _patientService;
        readonly private IMemoryCache _cache;

        public PatientsController(PatientService patientService, PatientHubMemoryCache cache)
        {
            _patientService = patientService;
            _cache = cache.Cache;
        }
        
        [HttpGet]
        [Route("Patients")]
        public async Task<ActionResult<IEnumerable<Patient>>> Get()
        {
            if (!_cache.TryGetValue("Patients", out IEnumerable<Patient> patientsInCache))
            {
                var results = await _patientService.GetAllPatient();

                patientsInCache = results.Take(5000);
                _cache.Set("Patients", patientsInCache, new MemoryCacheEntryOptions() { Size = 1 ,SlidingExpiration = TimeSpan.FromDays(1)});
            }

            //reducing result set for performance
            return Ok(patientsInCache);
        }

        [HttpGet]
        [Route("Patients/{PatientID}")]
        public async Task<ActionResult<Patient>> Get(string PatientID)
        {
            var result = await _patientService.GetPatient(PatientID);
            return (result is not null) ? Ok(result) : NotFound();
        }


        [HttpPut]
        [Route("Patients/{PatientID}/Score/{Score}")]
        public async Task<ActionResult<Patient>> Put(string PatientID, decimal Score)
        {
            var result = await _patientService.UpdateScore(PatientID, Score);
            return (result is not null) ? Ok(result) : NotFound();
        }

        /// <summary>
        /// </summary>
        /// <param name="patient">Basic Patient Information Object Parameter. The Gender should be Male, Female, Others</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Patients")]
        public async Task<ActionResult<Patient>> RegisterNewPatient([FromBody]BasicPatientProfile patient)
        {
            var result = await _patientService.RegisterPatient(patient);
            return (result is not null) ? Ok(result) : NotFound();
        }

        /// <summary>
        /// </summary>
        /// <param name="patientEntityId">Patient Entity's id</param>
        /// <param name="patientnumber">Patient Entity's patient_nbr</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Patients")]
        public async Task<ActionResult> Delete(string patientEntityId, string patientnumber)
        {
            await _patientService.DeletePatient(patientEntityId, patientnumber);
            return Ok();
        }
    }
}
