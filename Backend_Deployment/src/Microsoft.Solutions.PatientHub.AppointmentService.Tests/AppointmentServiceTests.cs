// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Solutions.PatientHub.AppointmentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Solutions.Test.MSTestV2;
using Newtonsoft.Json;

namespace Microsoft.Solutions.PatientHub.AppointmentService.Tests
{


    [TestClass()]
    public class AppointmentServiceTests : TestBase
    {
        private static AppointmentService appointmentService;
        static Models.Appointment appointment;

        [TestInitialize()]
        public void InitTest()
        {
            appointmentService = new AppointmentService(Config["Values:DBConnectionString"], "patienthubdb");
        }


        [TestMethod()]
        public async Task Test_01_CreateTest()
        {
            appointment = new Models.Appointment()
            {
                patient_id = 1940,
                doctor_name = "John Doe",
                purpose = "Follow up",
                start_time = DateTime.Now.AddDays(10),
                end_time = DateTime.Now.AddMinutes(30)
            };

            await appointmentService.CreateAsync(new Models.Appointment()
            {
                patient_id = 1940,
                doctor_name = "John Doe",
                start_time = DateTime.Now.AddDays(-10),
                end_time = (DateTime.Now.AddDays(-10)).AddMinutes(30)
            });

            var result = await appointmentService.CreateAsync(appointment);

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task Test_02_GetAppointments()
        {
            var results = await appointmentService.GetAllAppointmentsAsync(appointment.patient_id);
            foreach (var appointment in results)
            {
                Console.WriteLine(JsonConvert.SerializeObject(appointment));
            }

            Assert.IsTrue(results.Count() > 0);
        }

        [TestMethod()]
        public async Task Test_03_GetNextAppointmentTest()
        {
            var result = await appointmentService.GetNextAppointmentAsync(appointment.patient_id);
            Console.WriteLine(JsonConvert.SerializeObject(result));

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task Test_04_GetLastAppointmentTest()
        {
            var result = await appointmentService.GetLastAppointmentAsync(appointment.patient_id);
            Console.WriteLine(JsonConvert.SerializeObject(result));

            Assert.IsNotNull(result);
        }

        //[TestMethod()]
        //public async Task Test_05_CleanupAllAppointmentsTest()
        //{
        //    await appointmentService.CleanupAppointments(appointment.patient_id);
        //    var result = await appointmentService.GetLastAppointment(appointment.patient_id);
        //    Assert.IsNull(result);
        //}

        [TestMethod()]
        public async Task Test_06_GetNextAppointmentsTest()
        {
            var result = await appointmentService.GetNextAppointmentsAsync(appointment.patient_id);
            Console.WriteLine(JsonConvert.SerializeObject(result));

            Assert.IsNotNull(result);
        }

    }
}