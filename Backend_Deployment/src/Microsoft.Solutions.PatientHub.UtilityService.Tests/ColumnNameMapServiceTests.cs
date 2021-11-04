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
    public class ColumnNameMapServiceTests : TestBase
    {
        private static ColumnNameMapService columnNameMapService;

        [TestInitialize()]
        public void InitTest()
        {
            columnNameMapService = new ColumnNameMapService(Config["Values:DBConnectionString"], "patienthubdb", "ColumnNameMap");
        }

        [TestMethod()]
        public void Test_01_GetAllValuesTest()
        {
            var allValues = columnNameMapService.GetAllValues();
            foreach (var item in allValues)
            {
                Console.WriteLine(JsonConvert.SerializeObject(item));
            }

            Assert.IsTrue(allValues.Count() > 0);
        }

        [TestMethod()]
        public void Test_02_GetValueTest()
        {
            var value = columnNameMapService.GetValue(35);

            Assert.IsNotNull(value);
        }
    }
}