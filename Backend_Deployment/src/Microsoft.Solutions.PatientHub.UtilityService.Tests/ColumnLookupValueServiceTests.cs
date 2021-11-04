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
using Newtonsoft.Json;

namespace Microsoft.Solutions.PatientHub.UtilityService.Tests
{
    [TestClass()]
    public class ColumnLookupValueServiceTests : TestBase
    {
        private static ColumnLookupValueService columnLookupValueService;
        [TestInitialize()]
        public void InitTest()
        {
            columnLookupValueService = new ColumnLookupValueService(Config["Values:DBConnectionString"], "patienthubdb", "ColumnLookupValues");
        }


        [TestMethod()]
        public void Test_01_GetAllValuesTest()
        {
            var allvalues = columnLookupValueService.GetAllValues();

            foreach (var item in allvalues)
            {
                Console.WriteLine(JsonConvert.SerializeObject(item));
            }

            Assert.IsTrue(allvalues.Count() > 0);
        }

        [TestMethod()]
        public void Test_02_GetValueTest()
        {
            var value = columnLookupValueService.GetValue(38);

            Assert.IsNotNull(value);
        }
    }
}