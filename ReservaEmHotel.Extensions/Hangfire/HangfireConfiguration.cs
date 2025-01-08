using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaEmHotel.Extensions.Hangfire
{
    public class HangfireConfiguration
    {
  
        public static void DashBoard(IApplicationBuilder app)
        {
            app.UseHangfireDashboard("/hangfire");
        }
        public static void Configuration(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180);
                config.UseSimpleAssemblyNameTypeSerializer();
                config.UseRecommendedSerializerSettings();
                config.UseSqlServerStorage(configuration.GetConnectionString("DatabaseConnection"));
                new BackgroundJobServerOptions
                {
                    WorkerCount = Environment.ProcessorCount * 5
                };
            });

        }
    }
}
