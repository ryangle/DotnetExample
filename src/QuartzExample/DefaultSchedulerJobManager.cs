using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuartzExample
{
    public class DefaultSchedulerJobManager
    {
        private IScheduler _scheduler;
        private IJobFactory _jobFactory;

        public DefaultSchedulerJobManager(IJobFactory jobFactory)
        {
            _jobFactory = jobFactory;
        }
        public Task StartAsync()
        {
            try
            {
                _scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
                _scheduler.JobFactory = _jobFactory;
                _scheduler.ListenerManager.AddJobListener(new QuartzJobListener());
                _scheduler.Start();
            }
            catch (SchedulerException se)
            {
                Console.Error.WriteLineAsync(se.ToString());
            }
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            _scheduler.Shutdown();
            return Console.Out.WriteLineAsync("SchedulerHostService Stop!");
        }
        public async Task ScheduleAsync<TJob>(Action<JobBuilder> configureJob, Action<TriggerBuilder> configureTrigger)
           where TJob : IJob
        {
            var jobToBuild = JobBuilder.Create<TJob>();
            configureJob(jobToBuild);
            var job = jobToBuild.Build();

            var triggerToBuild = TriggerBuilder.Create();
            configureTrigger(triggerToBuild);
            var trigger = triggerToBuild.Build();

            await _scheduler.ScheduleJob(job, trigger);
        }

        public async Task RescheduleAsync(TriggerKey triggerKey, Action<TriggerBuilder> configureTrigger)
        {
            var triggerToBuild = TriggerBuilder.Create();
            configureTrigger(triggerToBuild);
            var trigger = triggerToBuild.Build();

            await _scheduler.RescheduleJob(triggerKey, trigger);
        }

        public async Task UnscheduleAsync(TriggerKey triggerKey)
        {
            await _scheduler.UnscheduleJob(triggerKey);
        }
    }
}
