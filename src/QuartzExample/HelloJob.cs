using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuartzExample
{
    public class HelloJob : IJob
    {
        public HelloJob()
        {
            Console.Out.WriteLineAsync("Create new HelloJob!");
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Greetings from HelloJob!");
        }
    }
}
