// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Solutions.PatientHub.CognitiveService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Solutions.Test.MSTestV2;

namespace Microsoft.Solutions.PatientHub.CognitiveService.Tests
{
    [TestClass()]
    public class TTSServiceTests : TestBase
    {
        private static TTSService ttsService;

        [TestInitialize]
        public void InitTest()
        {
            ttsService = new TTSService(Config["Values:TTSSubscriptionKey"], Config["Values:TTSServiceRegion"]);
        }

        [TestMethod()]
        public async Task GetSpeechStreamTest()
        {
            var result = await ttsService.GetSpeechStreamAsync("Hello John Doe, How are you?");
            Assert.IsTrue(result.Length > 0);
        }
    }
}