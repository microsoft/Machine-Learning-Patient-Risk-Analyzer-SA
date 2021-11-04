// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.RealtimeInferenceService.Model
{
    public class PatientData
    {
        public string race { get; set; }
        public string gender { get; set; }
        public string age { get; set; }
        public string weight { get; set; }
        public int admission_type_id { get; set; }
        public int discharge_disposition_id { get; set; }
        public int admission_source_id { get; set; }
        public int time_in_hospital { get; set; }
        public string payer_code { get; set; }
        public string medical_specialty { get; set; }
        public int num_lab_procedures { get; set; }
        public int num_procedures { get; set; }
        public int num_medications { get; set; }
        public int number_outpatient { get; set; }
        public int number_emergency { get; set; }
        public int number_inpatient { get; set; }
        public string diag_1 { get; set; }
        public string diag_2 { get; set; }
        public string diag_3 { get; set; }
        public int number_diagnoses { get; set; }
        public string max_glu_serum { get; set; }
        public string A1Cresult { get; set; }
        public string metformin { get; set; }
        public string repaglinide { get; set; }
        public string nateglinide { get; set; }
        public string chlorpropamide { get; set; }
        public string glimepiride { get; set; }
        public string acetohexamide { get; set; }
        public string glipizide { get; set; }
        public string glyburide { get; set; }
        public string tolbutamide { get; set; }
        public string pioglitazone { get; set; }
        public string rosiglitazone { get; set; }
        public string acarbose { get; set; }
        public string miglitol { get; set; }
        public string troglitazone { get; set; }
        public string tolazamide { get; set; }
        public string examide { get; set; }
        public string citoglipton { get; set; }
        public string insulin { get; set; }
        [JsonProperty("glyburide-metformin")] public string glyburide_metformin { get; set; }
        [JsonProperty("glipizide-metformin")] public string glipizide_metformin { get; set; }
        [JsonProperty("glimepiride-pioglitazone")] public string glimepiride_pioglitazone { get; set; }
        [JsonProperty("metformin-rosiglitazone")] public string metformin_rosiglitazone { get; set; }
        [JsonProperty("metformin-pioglitazone")] public string metformin_pioglitazone { get; set; }
        public string change { get; set; }
        public string diabetesMed { get; set; }
    }
}
