// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Solutions.PatientHub.BatchInferenceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Solutions.Test.MSTestV2;
using Newtonsoft.Json;
using Microsoft.Solutions.PatientHub.UtilityService;

namespace Microsoft.Solutions.PatientHub.BatchInferenceService.Tests
{
    [TestClass()]
    public class ExplanationServiceTests : TestBase
    {
        private static ExplanationService explanationService;
        private static ColumnLookupValueService columnLookupValueService;
        
        [TestInitialize()]
        public void InitTest()
        {
            columnLookupValueService = new ColumnLookupValueService(Config["Values:DBConnectionString"], "patienthubdb", "ColumnLookupValues");
            explanationService = new ExplanationService(Config["Values:DBConnectionString"], "patienthubdb", "Explanations");
        }

        [TestMethod()]
        public async Task Test_01_GetTop5ExplanationsTest()
        {
            var result = await explanationService.GetTop5Explanations(columnLookupValueService ,"66822");
            foreach (var item in result)
            {
                Console.WriteLine(JsonConvert.SerializeObject(item));
            }

            Assert.IsTrue(result.Count() == 5);
        }
    }
}