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

namespace Microsoft.Solutions.PatientHub.PatientService.Tests
{
    [TestClass()]
    public class AdmissionSourceServiceTests : TestBase
    {
        private static AdmissionSourceService admissionSourceService;

        [TestInitialize()]
        public void InitTest()
        {
            admissionSourceService = new AdmissionSourceService(Config["Values:DBConnectionString"], "patienthubdb", "AdmissionSource");
        }

        [TestMethod()]
        public void Test_01_GetDescriptionTest()
        {
            for (int i = 1; i < 27; i++)
            {

                Console.WriteLine($" {i} - {admissionSourceService.GetAdmissionSource(i)}");
            }
        }

        [TestMethod()]
        public void Test_02_GetDescriptionsTest()
        {
            var descriptions = admissionSourceService.GetAdmissionSources();

            foreach (var item in descriptions)
            {
                Console.WriteLine(item);
            }


            Assert.IsTrue(descriptions.Count() > 0);
        }
    }
}