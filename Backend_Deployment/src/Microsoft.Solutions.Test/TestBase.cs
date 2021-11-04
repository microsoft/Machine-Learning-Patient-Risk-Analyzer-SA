// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Microsoft.Solutions.Test.MSTestV2
{
    public class TestBase
    {
        public readonly IConfigurationRoot Config;

        public TestBase()
        {
            this.Config = GetSettings();
        }

        protected IConfigurationRoot GetSettings()
        {
            var relativePath = $@"..\..\..\..\TestConfigurations\.";

            var filePath = Path.Combine(Path.GetFullPath(relativePath), "appsettings.json");
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("there is no appsettings.json file");
            }

            return new ConfigurationBuilder()
                                               .SetBasePath(Path.GetDirectoryName(filePath))
                                               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                               .Build();
        }

    }
}
