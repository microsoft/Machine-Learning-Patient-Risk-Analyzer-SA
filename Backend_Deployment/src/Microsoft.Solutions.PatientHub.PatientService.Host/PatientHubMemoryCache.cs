// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.PatientService.Host
{
    public class PatientHubMemoryCache
    {
        public MemoryCache Cache { get; set; }
        
        public PatientHubMemoryCache()
        {
            Cache = new MemoryCache(new MemoryCacheOptions { SizeLimit = 1024 });
        }
    }
}
