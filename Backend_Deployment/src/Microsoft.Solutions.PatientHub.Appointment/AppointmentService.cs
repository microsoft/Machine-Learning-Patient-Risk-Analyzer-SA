// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Solutions.CosmosDB.SQL;
using System;
using Microsoft.Solutions.PatientHub.AppointmentService.Models;
using System.Threading.Tasks;
using Microsoft.Solutions.CosmosDB;
using System.Collections.Generic;

namespace Microsoft.Solutions.PatientHub.AppointmentService
{
    public class AppointmentService : SQLEntityCollectionBase<Appointment>
    {
        public AppointmentService(string DataConnectionString, string CollectionName, string ContainerName = "") : base(DataConnectionString, CollectionName, ContainerName)
        {
        }

        async public Task<Appointment> CreateAsync(Appointment appointment)
        {
            return await this.EntityCollection.AddAsync(appointment);
        }

        async public Task<IEnumerable<Appointment>> GetAllAppointmentsAsync(int patientID)
        {
            return await this.EntityCollection.FindAllAsync(new GenericSpecification<Appointment>((x => x.patient_id == patientID), (x => x.start_time), Order.Desc));
        }

        async public Task<Appointment> GetNextAppointmentAsync(int patientID)
        {
            return await this.EntityCollection.FindAsync(new GenericSpecification<Appointment>(x => (x.patient_id == patientID) && (x.start_time > DateTime.Now), (x => x.start_time), Order.Asc));
        }

        async public Task<IEnumerable<Appointment>> GetNextAppointmentsAsync(int patientID)
        {
            return await this.EntityCollection.FindAllAsync(new GenericSpecification<Appointment>(x => (x.patient_id == patientID) && (x.start_time > DateTime.Now), (x => x.start_time), Order.Asc));
        }

        async public Task<Appointment> GetLastAppointmentAsync(int patientID)
        {
            return await this.EntityCollection.FindAsync(new GenericSpecification<Appointment>(x => (x.patient_id == patientID) && (x.start_time < DateTime.Now), (x => x.start_time), Order.Desc));
        }

        async public Task CleanupAppointmentsAsync(int patientID)
        {
            foreach (var appointment in await this.GetAllAppointmentsAsync(patientID))
            {
                await this.EntityCollection.DeleteAsync(appointment);
            }
        }
    }
}
