using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentalStack.Entities;
using MentalStack.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MentalStack
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MStackService>();
            services.AddMemoryCache();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddDebug();
            }
            app.UseMvc();
            loggerFactory.AddAzureWebAppDiagnostics(
                new Microsoft.Extensions.Logging.AzureAppServices.AzureAppServicesDiagnosticsSettings
                {
                    OutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss zz [{Level}] {RequestId}-{SourceContext}: {Message}{ NewLine}{ Exception}"
                }
            );
        }
    }
}
