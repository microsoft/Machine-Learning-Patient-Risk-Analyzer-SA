// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Solutions.PatientHub.PatientService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Solutions.Test.MSTestV2;
using Newtonsoft.Json;
using System.ComponentModel;
using Microsoft.Solutions.PatientHub.PatientService.Models;
using AutoMapper;

namespace Microsoft.Solutions.PatientHub.PatientService.Tests
{
    [TestClass()]
    public class PatientServiceTests : TestBase
    {
        private static PatientService patientService;
        private static AdmissionService admissionTypeService;

        [TestInitialize()]
        public void InitTest()
        {
            patientService = new PatientService(Config["Values:DBConnectionString"], "patienthubdb", "Patient");
            admissionTypeService = new AdmissionService(Config["Values:DBConnectionString"], "patienthubdb", "AdmissionType");
        }

        [TestMethod()]
        public async Task Test_01_GetAllPatients()
        {
            var result = await patientService.GetAllPatient();

            foreach (var item in result)
            {
                Console.WriteLine(JsonConvert.SerializeObject(item));
            }
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod()]
        public async Task Test_02_GetPatient()
        {
            var result = await patientService.GetPatient("65923");
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task Test_02_UpdateScore()
        {
            var result = await patientService.UpdateScore("65923", (decimal)0.5);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task Test_03_RegisterPatient()
        {
            BasicPatientProfile newPatient = new BasicPatientProfile() { LastName = "John", FirstName = "Doe", Age = 46, Gender = Gender.Male };

            var result = await patientService.RegisterPatient(newPatient);
            Console.WriteLine(JsonConvert.SerializeObject(result));

            await patientService.DeletePatient(result.id, result.Id);
            
            
            Assert.IsNotNull(result);


        }



    }
}