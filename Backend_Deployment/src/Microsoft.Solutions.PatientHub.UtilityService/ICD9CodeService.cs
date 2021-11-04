// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Solutions.CosmosDB.SQL;
using Microsoft.Solutions.PatientHub.UtilityService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Solutions.PatientHub.UtilityService
{
    public class ICD9CodeService : SQLEntityCollectionBase<ICD9Code>
    {
        private static IEnumerable<ICD9Code> icd9Codes;

        public ICD9CodeService(string DataConnectionString, string CollectionName, string ContainerName = "") : base(DataConnectionString, CollectionName, ContainerName)
        {
            putCache();
        }

        public string GetDescription(string Code)
        {
            var result = ICD9CodeService.icd9Codes.Where(x => x.Code == Code).FirstOrDefault();
            return ((result is null) || (result.Description is null)) ? "" : result.Description;
        }

        public IEnumerable<string> GetDescriptions()
        {
            return ICD9CodeService.icd9Codes.Select(x => x.Description).ToArray();
        }

        private void putCache()
        {
            if (ICD9CodeService.icd9Codes is null)
            {
                ICD9CodeService.icd9Codes =
                    this.EntityCollection.GetAllAsync().GetAwaiter().GetResult();
            }
        }
    }
}
