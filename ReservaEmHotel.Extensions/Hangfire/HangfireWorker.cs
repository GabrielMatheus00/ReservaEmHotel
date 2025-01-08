using Hangfire;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaEmHotel.Extensions.Hangfire
{
    public class HangfireWorker : BackgroundService
    {   
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            RecurringJob.AddOrUpdate("ASDASDS", () => Console.WriteLine("teste"), Cron.Daily);
        }
    }
}
