// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Solutions.CosmosDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.AppointmentService.Models
{
    public class Appointment : CosmosDBEntityBase
    {
        public Appointment()
        {
            this.appointment_id = Guid.NewGuid();
        }

        public Guid appointment_id { get; set; }
        public int patient_id { get; set; }
        public string purpose { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public string doctor_name { get; set; }
    }
}
