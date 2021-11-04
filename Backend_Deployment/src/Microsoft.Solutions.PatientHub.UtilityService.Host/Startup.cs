// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.UtilityService.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Microsoft.Solutions.PatientHub.UtilityService.Host", Version = "v1" });
            });

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddTransient<ColumnLookupValueService>(x => { return new ColumnLookupValueService(Configuration["Values:DBConnectionString"], Configuration["Values:DatabaseName"], "ColumnLookupValues"); });
            services.AddTransient<ColumnNameMapService>(x => { return new ColumnNameMapService(Configuration["Values:DBConnectionString"], Configuration["Values:DatabaseName"], "ColumnNameMap"); });
            services.AddTransient<DischargeDispositionService>(x => { return new DischargeDispositionService(Configuration["Values:DBConnectionString"], Configuration["Values:DatabaseName"], "DischargeDisposition"); });
            services.AddTransient<ICD9CodeService>(x => { return new ICD9CodeService(Configuration["Values:DBConnectionString"], Configuration["Values:DatabaseName"], "ICD9Code"); });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microsoft.Solutions.PatientHub.UtilityService.Host v1"));
            //}

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
