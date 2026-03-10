using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ReservaHotel.Services.Authorizators;

namespace ReservaHotel.Extensions.Extensions.Hangfire
{
    public static class HangfireConfig
    {

        public static void ConfigHangfireServer(this IServiceCollection services, string connectionString)
        {
            services.AddHangfire(config =>
            {
                config.UsePostgreSqlStorage(connectionString)
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings();
            }).AddHangfireServer(config =>
            {
                config.WorkerCount = 5;
                config.ServerName = "Hoteis Hangfire";
            }
             );
        }
        public static void StartDashboard(this WebApplication app)
        {
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorization() }
            });
        }
        private static void AddTasks()
        {
            
        }
    }
}
