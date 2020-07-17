using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System.Threading.Tasks;

namespace QuartzExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddTransient<HelloJob>();

                   services.AddTransient<IJobFactory, QuartzJobFactory>();
                   services.AddSingleton<JobManager>();
                   services.AddSingleton<IHostedService, SchedulerHostService>();

                   services.AddSingleton<IHostedService, JobsConfigService>();
               });

            await builder.RunConsoleAsync();
        }
    }
}
