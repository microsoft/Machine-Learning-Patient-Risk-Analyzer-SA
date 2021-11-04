// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.PatientService.Models
{
    public class BasicPatientProfile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public  Gender Gender { get; set; }

        public Patient GenerateNewPatient()
        {
            return new Patient()
            {
                DMPRW30Days_Score = 0,
                Id = (new Random(BitConverter.ToInt32(getInitVal())).Next(17000, int.MaxValue)).ToString(),
                patient_nbr = (new Random(BitConverter.ToInt32(getInitVal())).Next(17000, int.MaxValue)),
                FirstName = this.FirstName,
                LastName = this.LastName,
                age = transformAge(this.Age),
                race = "?",
                gender = this.Gender.ToString(),
                weight = "?",
                admission_type_id = 8,
                discharge_disposition_id = 25,
                admission_source_id = 20,
                time_in_hospital = 0,
                payer_code = "?",
                medical_specialty = "?",
                num_lab_procedures = 0,
                num_procedures = 0,
                num_medications = 0,
                number_outpatient = 0,
                number_emergency = 0,
                number_inpatient = 0,
                diag_1 = "0",
                diag_2 = "0",
                diag_3 = "0",
                number_diagnoses = 0,
                max_glu_serum = "None",
                A1Cresult = "None",
                metformin = "No",
                repaglinide = "No",
                nateglinide = "No",
                chlorpropamide ="No",
                glimepiride = "No",
                acetohexamide = "No",
                glipizide = "No",
                glyburide = "No",
                tolbutamide = "No",
                pioglitazone = "No",
                rosiglitazone = "No",
                acarbose = "No",
                miglitol = "No",
                troglitazone = "No",
                tolazamide = "No",
                examide = "No",
                citoglipton = "No",
                insulin = "No",
                glyburide_metformin = "No",
                glipizide_metformin = "No",
                glimepiride_pioglitazone = "No",
                metformin_pioglitazone = "No",
                metformin_rosiglitazone = "No",
                change = "No",
                diabetesMed = "No",
                readmitted = "No"
            };
        }

        private byte[] getInitVal()
        {
            return System.Security.Cryptography.SHA1.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(System.DateTime.Now.Ticks.ToString()));
        }

        private string transformAge(int age)
        {
            int ages;
            ages = (age % 10) > 0 ? (age / 10) * 10 : ((age / 10) * 10) - 10;
            
            return $"[{ages}-{ages + 10})";
        }
    }

    public enum Gender { Male, Female, Others }
}
