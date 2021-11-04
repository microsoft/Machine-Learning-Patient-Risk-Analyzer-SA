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
    public class ICD9CodeServiceTests : TestBase
    {
        private static ICD9CodeService iCD9CodeService;

        [TestInitialize()]
        public void InitTest()
        {
            iCD9CodeService = new ICD9CodeService(Config["Values:DBConnectionString"], "patienthubdb", "ICD9Code");
        }

        [TestMethod()]
        public void Test_01_GetDescriptionTest()
        {
            var value = iCD9CodeService.GetDescription("V86");

            Assert.IsNotNull(value);
        }

        [TestMethod()]
        public void Test_02_GetDescriptionsTest()
        {
            var allValues = iCD9CodeService.GetDescriptions();

            foreach (var item in allValues)
            {
                Console.WriteLine(item);
            }

            Assert.IsTrue(allValues.Count() > 0);

        }
    }
}