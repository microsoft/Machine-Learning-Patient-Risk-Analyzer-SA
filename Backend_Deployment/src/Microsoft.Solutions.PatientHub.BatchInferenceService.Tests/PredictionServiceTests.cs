// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Solutions.PatientHub.BatchInferenceService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Solutions.Test.MSTestV2;
using Newtonsoft.Json;

namespace Microsoft.Solutions.PatientHub.BatchInferenceService.Models.Tests
{
    [TestClass()]
    public class PredictionServiceTests : TestBase
    {
        private PredictionService predictionService;

        [TestInitialize]
        public void Testinit()
        {
            predictionService = new PredictionService(Config["Values:DBConnectionString"], "patienthubdb", "Predictions");
        }

        [TestMethod()]
        public async Task GetAllPredictionsTest()
        {
            var results = await predictionService.GetAllPredictions();
            Assert.IsTrue(results.Count() > 0);
        }

        [TestMethod()]
        public async Task GetPredictionTest()
        {
            var result = await predictionService.GetPrediction("41660");
            Console.WriteLine(JsonConvert.SerializeObject(result));
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task GetPredictionsByPatientIdsTest()
        {
            var result = predictionService.GetPredictionsByPatientIds(new string[] { "41660", "14759" });
            await foreach (var item in result)
            {
                Console.WriteLine(JsonConvert.SerializeObject(item));
            }

            Assert.IsNotNull(result);
        }
    }
}