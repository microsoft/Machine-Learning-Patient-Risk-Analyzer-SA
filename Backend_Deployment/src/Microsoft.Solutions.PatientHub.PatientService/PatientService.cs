// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Solutions.CosmosDB;
using Microsoft.Solutions.CosmosDB.SQL;
using Microsoft.Solutions.PatientHub.PatientService.Models;
using Microsoft.Solutions.PatientHub.UtilityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.PatientService
{
    public class PatientService : SQLEntityCollectionBase<Patient>
    {
        private AdmissionSourceService admissionSourceService;
        private AdmissionService admissionTypeService;
        private DischargeDispositionService dischargeDispositionService;
        private ICD9CodeService IDC9CodeService;

        public PatientService(string DataConnectionString, string CollectionName, string ContainerName = "") : base(DataConnectionString, CollectionName, ContainerName)
        {
            admissionTypeService = new AdmissionService(DataConnectionString, CollectionName, "AdmissionType");
            admissionSourceService = new AdmissionSourceService(DataConnectionString, CollectionName, "AdmissionSource");
            dischargeDispositionService = new DischargeDispositionService(DataConnectionString, CollectionName, "DischargeDisposition");
            IDC9CodeService = new ICD9CodeService(DataConnectionString, CollectionName, "ICD9Code");
        }

        async public Task<IEnumerable<Patient>> GetAllPatient()
        {
            return await this.EntityCollection.GetAllAsync();
        }

        async public Task<Patient> GetPatient(string PatientID)
        {
           
            var patient = await this.EntityCollection.FindAsync(new GenericSpecification<Patient>(x => x.Id.Equals(PatientID)));
            
            if (patient is null)
            {
                Console.WriteLine($"=============>>  Not found patient - {PatientID}");
                return null;
            }

            patient.admissionSource = admissionSourceService.GetAdmissionSource(patient.admission_source_id);
            patient.admission_type = admissionTypeService.GetAdmissionType(patient.admission_type_id);
            //for UI         
            var admissionType = admissionTypeService.GetAdmissionType(patient.discharge_disposition_id);
            patient.dischargeDisposition = new DischargeDisposition() {Id = patient.discharge_disposition_id };
            patient.dischargeDisposition.Description =((admissionType is null) || (admissionType.Description is null)) ? "-" : admissionType.Description ;

            patient.diag_1_Description = IDC9CodeService.GetDescription(patient.diag_1);
            patient.diag_2_Description = IDC9CodeService.GetDescription(patient.diag_2);
            patient.diag_3_Description = IDC9CodeService.GetDescription(patient.diag_3);
          
            return patient;
        }


        async public Task<Patient> UpdateScore(string PatientID, decimal Score)
        {
            var paitnet = await GetPatient(PatientID);
            if (paitnet is null) return null;

            
            paitnet.DMPRW30Days_Score = Score;
            Console.WriteLine($"Patient {PatientID} has been updated.");
            return await this.EntityCollection.SaveAsync(paitnet);
        }

        async public Task<Patient> AddNewPatient(Patient Patient)
        {
            return await this.EntityCollection.AddAsync(Patient);
        }

        async public Task<Patient> RegisterPatient(BasicPatientProfile Patient)
        {
            return await this.AddNewPatient(Patient.GenerateNewPatient());
        }

        async public Task DeletePatient(string EntityId, string PartitonKeyValue)
        {
            await this.EntityCollection.DeleteAsync(EntityId, PartitonKeyValue);
        }

        async public Task<Patient> UpdatePatient(Patient Patient)
        {
            return await this.EntityCollection.SaveAsync(Patient);
        }
    }
}
