using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzExample
{
    public class QuartzJobListener : IJobListener
    {
        public string Name { get; } = "QuartzJobListener";

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            Console.Out.WriteLineAsync($"Job {context.JobDetail.JobType.Name} executing...");
            return Task.CompletedTask;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            Console.Out.WriteLineAsync($"Job {context.JobDetail.JobType.Name} executing operation vetoed...");
            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            if (jobException == null)
            {
                Console.Out.WriteLineAsync($"Job {context.JobDetail.JobType.Name} successfully executed.");
            }
            else
            {
                Console.Out.WriteLineAsync($"Job {context.JobDetail.JobType.Name} failed with exception: {jobException}");
            }
            return Task.CompletedTask;
        }
    }
}
