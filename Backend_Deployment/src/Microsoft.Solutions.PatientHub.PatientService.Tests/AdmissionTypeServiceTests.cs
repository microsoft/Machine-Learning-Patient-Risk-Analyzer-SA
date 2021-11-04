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

namespace Microsoft.Solutions.PatientHub.PatientService.Tests
{
    [TestClass()]
    public class AdmissionTypeServiceTests : TestBase
    {
        private static AdmissionService admissionTypeService;

        [TestInitialize()]
        public void InitTest()
        {
            admissionTypeService = new AdmissionService(Config["Values:DBConnectionString"], "patienthubdb", "AdmissionType");
        }

        [TestMethod()]
        public void Test_01_GetDescriptionTest()
        {
            for (int i = 1; i < 8; i++)
            {

                Console.WriteLine(JsonConvert.SerializeObject(admissionTypeService.GetAdmissionType(i)));
            }
        }

        [TestMethod()]
        public void Test_02_GetDescriptionsTest()
        {
            var descriptions = admissionTypeService.GetAdmissionTypes();

            foreach (var item in descriptions)
            {
                Console.WriteLine(JsonConvert.SerializeObject(item));
            }


            Assert.IsTrue(descriptions.Count() > 0);
        }
    }
}