// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Solutions.PatientHub.UtilityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Solutions.Test.MSTestV2;

namespace Microsoft.Solutions.PatientHub.UtilityService.Tests
{
    [TestClass()]
    public class DischargeDispositionServiceTests : TestBase
    {
        private static DischargeDispositionService dischargeDispositionService;
        [TestInitialize()]
        public void InitTest()
        {
            dischargeDispositionService = new DischargeDispositionService(Config["Values:DBConnectionString"], "patienthubdb", "DischargeDisposition");
        }


        [TestMethod()]
        public void GetDescriptionTest()
        {
            var value = dischargeDispositionService.GetDescription(30);

            Assert.IsNotNull(value);
        }

        [TestMethod()]
        public void GetDescriptionsTest()
        {
            var allValues = dischargeDispositionService.GetDescriptions();

            foreach (var item in allValues)
            {
                Console.WriteLine(item);
            }

            Assert.IsTrue(allValues.Count() > 0);
        }
    }
}