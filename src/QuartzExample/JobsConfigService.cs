using Microsoft.Extensions.Hosting;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzExample
{
    public class JobsConfigService : IHostedService
    {
        private JobManager _jobManager;
        public JobsConfigService(JobManager jobManager)
        {
            _jobManager = jobManager;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return _jobManager.ScheduleAsync<HelloJob>(
                 jobBuilder =>
                 {
                     jobBuilder.WithIdentity("job1", "group1");
                 },
                 triggerBuilder =>
                 {
                     triggerBuilder
                     .WithIdentity("trigger1", "group1")
                     .StartNow()
                     .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(10)
                        .RepeatForever());
                 });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
