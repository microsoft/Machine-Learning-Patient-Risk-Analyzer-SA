// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Solutions.PatientHub.RealtimeInferenceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Solutions.Test.MSTestV2;
using Newtonsoft.Json;
using Microsoft.Solutions.PatientHub.RealtimeInferenceService.Model;
using Microsoft.Solutions.PatientHub.UtilityService;

namespace Microsoft.Solutions.PatientHub.RealtimeInferenceService.Tests
{
    [TestClass()]
    public class RealtimeInferenceTests : TestBase
    {
        static RealtimeInference realtimeInferenceService;
        static ColumnNameMapService columnNameMapService;
        static ColumnLookupValueService columnLookupValueService;

        [TestInitialize]
        public void TestInit()
        {
            RealtimeInferenceTests.realtimeInferenceService =
                new RealtimeInference(Config["Values:MLserviceUrl"], Config["Values:MLServiceBearerToken"]);
            RealtimeInferenceTests.columnNameMapService =
                new ColumnNameMapService(Config["Values:DBConnectionString"], "patienthubdb", "ColumnNameMap");
            RealtimeInferenceTests.columnLookupValueService =
          new ColumnLookupValueService(Config["Values:DBConnectionString"], "patienthubdb", "ColumnLookupValues");
        }

        [TestMethod()]
        public async Task GetTop5RealtimeInferenceTest()
        {
            string jsonString = @"{
    'race': 'Caucasian',
    'gender': 'Male',
    'age': '[70-80)',
    'weight': '?',
    'admission_type_id': 1,
    'discharge_disposition_id': 24,
    'admission_source_id': 7,
    'time_in_hospital': 1,
    'payer_code': 'MD',
    'medical_specialty': '?',
    'num_lab_procedures': 51,
    'num_procedures': 0,
    'num_medications': 19,
    'number_outpatient': 0,
    'number_emergency': 0,
    'number_inpatient': 3,
    'diag_1': '491',
    'diag_2': '428',
    'diag_3': '428',
    'number_diagnoses': 9,
    'max_glu_serum': 'None',
    'A1Cresult': 'None',
    'metformin': 'No',
    'repaglinide': 'No',
    'nateglinide': 'No',
    'chlorpropamide': 'No',
    'glimepiride': 'No',
    'acetohexamide': 'No',
    'glipizide': 'Steady',
    'glyburide': 'No',
    'tolbutamide': 'No',
    'pioglitazone': 'Steady',
    'rosiglitazone': 'No',
    'acarbose': 'No',
    'miglitol': 'No',
    'troglitazone': 'No',
    'tolazamide': 'No',
    'examide': 'No',
    'citoglipton': 'No',
    'insulin': 2,
    'glyburide-metformin': 'No',
    'glipizide-metformin': 'No',
    'glimepiride-pioglitazone': 'No',
    'metformin-rosiglitazone': 'No',
    'metformin-pioglitazone': 'No',
    'change': 'Ch',
    'diabetesMed': 'Yes'
}
";
            var patientData = JsonConvert.DeserializeObject<PatientData>(jsonString);

            var result = await RealtimeInferenceTests.realtimeInferenceService.GetTop5RealtimeInference(columnNameMapService, columnLookupValueService, patientData);
            Console.WriteLine(JsonConvert.SerializeObject(result));
            Assert.IsNotNull(result);
        }
    }
}