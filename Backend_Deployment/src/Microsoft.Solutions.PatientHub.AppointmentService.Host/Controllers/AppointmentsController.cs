// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Solutions.PatientHub.AppointmentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.AppointmentService.Host.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppointmentsController : Controller
    {
        readonly private AppointmentService _appointmentService;
        readonly private IMemoryCache _cache;

        public AppointmentsController(AppointmentService appointmentService, PatientHubMemoryCache cache)
        {
            _appointmentService = appointmentService;
            _cache = cache.Cache;
        }

        [HttpGet]
        [Route("Patients/{PatientID}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> Get(int PatientID)
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync(PatientID);
            return appointments.Count() > 0 ? Ok(appointments) : NotFound();
        }

        [HttpGet]
        [Route("Patients/{PatientID}/NextAppointment")]
        public async Task<ActionResult<Appointment>> GetNextAppointment(int PatientID)
        {
            var appointment = await _appointmentService.GetNextAppointmentAsync(PatientID);
            return appointment is not null ? Ok(appointment) : NotFound();
        }

        [HttpGet]
        [Route("Patients/{PatientID}/NextAppointments")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetNextAppointments(int PatientID)
        {
            var appointment = await _appointmentService.GetNextAppointmentsAsync(PatientID);

            //Demo purpose.
            if (appointment.Count() == 0)
            {
                if (!_cache.TryGetValue("MockAppointment", out IEnumerable<Appointment> mockAppointmentInCache))
                {
                    var lstappointment = new List<Appointment>();
                    lstappointment.Add(new Appointment()
                    {
                        appointment_id = Guid.NewGuid(),
                        start_time = DateTime.Now.AddDays(7),
                        end_time = DateTime.Now.AddDays(7).AddMinutes(30),
                        doctor_name = "John Doe",
                        patient_id = PatientID,
                        purpose = "Follow up"
                    });
                    lstappointment.Add(new Appointment()
                    {
                        appointment_id = Guid.NewGuid(),
                        start_time = DateTime.Now.AddDays(14),
                        end_time = DateTime.Now.AddDays(14).AddMinutes(30),
                        doctor_name = "John Doe",
                        patient_id = PatientID,
                        purpose = "Follow up"
                    });
                    lstappointment.Add(new Appointment()
                    {
                        appointment_id = Guid.NewGuid(),
                        start_time = DateTime.Now.AddDays(21),
                        end_time = DateTime.Now.AddDays(21).AddMinutes(30),
                        doctor_name = "John Doe",
                        patient_id = PatientID,
                        purpose = "Follow up"
                    });

                    mockAppointmentInCache = lstappointment.ToArray();
                    _cache.Set("MockAppointment", mockAppointmentInCache, new MemoryCacheEntryOptions() { Size = 1 });
                }

                appointment = mockAppointmentInCache;
            }

            return appointment is not null ? Ok(appointment) : NotFound();
        }

        [HttpGet]
        [Route("Patients/{PatientID}/LastAppointment")]
        public async Task<ActionResult<Appointment>> GetLastAppointment(int PatientID)
        {
            var appointment = await _appointmentService.GetLastAppointmentAsync(PatientID);
            return appointment is not null ? Ok(appointment) : NotFound();
        }


        [HttpPost]
        public async Task<ActionResult<Appointment>> Create(Appointment appointment)
        {
            var newAppointment = await _appointmentService.CreateAsync(appointment);
            return new CreatedResult(@"/Appointments/PatientID", newAppointment);
        }

        [HttpDelete]
        [Route("Patients/{PatientID}")]
        public async Task<ActionResult> Delete(int PatientID)
        {
            await _appointmentService.CleanupAppointmentsAsync(PatientID);
            return NoContent();
        }



    }
}
