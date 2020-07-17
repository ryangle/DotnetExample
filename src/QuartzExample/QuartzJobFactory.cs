using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuartzExample
{
    public class QuartzJobFactory : IJobFactory
    {
        private IServiceProvider _services;
        public QuartzJobFactory(IServiceProvider services)
        {
            Console.Out.WriteLineAsync("QuartzJobFactory new!");
            _services = services;
        }
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _services.GetService(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            Console.Out.WriteLineAsync("QuartzJobFactory ReturnJob!");
        }
    }
}
