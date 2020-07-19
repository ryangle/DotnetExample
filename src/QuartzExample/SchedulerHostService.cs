using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzExample
{
    public class SchedulerHostService : IHostedService
    {
        private DefaultSchedulerJobManager _jobManager;

        public SchedulerHostService(DefaultSchedulerJobManager jobManager)
        {
            _jobManager = jobManager;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return _jobManager.StartAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _jobManager.StopAsync();
        }

    }
}
