using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Race;

namespace Server
{
    public class RacerService : Racer.RacerBase
    {
        public RacerService()
        {
            Console.WriteLine("RacerService Create");
        }
        ~RacerService()
        {
            Console.WriteLine("RacerService Finished");
        }
        public override async Task ReadySetGo(IAsyncStreamReader<RaceMessage> requestStream, IServerStreamWriter<RaceMessage> responseStream, ServerCallContext context)
        {
            var raceDuration = TimeSpan.Parse(context.RequestHeaders.Single(h => h.Key == "race-duration").Value);
            
            // Read incoming messages in a background task
            RaceMessage? lastMessageReceived = null;
            var readTask = Task.Run(async () =>
            {
                try
                {
                    await foreach (var message in requestStream.ReadAllAsync())
                    {
                        Console.WriteLine(message);
                        lastMessageReceived = message;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"requestStream ex:{ex.Message}");
                }

            });

            // Write outgoing messages until timer is complete
            //var sw = Stopwatch.StartNew();
            var sent = 0;
            while (true)
            {
                try
                {
                    var msg = Console.ReadLine();
                    await responseStream.WriteAsync(new RaceMessage { Count = ++sent, Message = $"Server say:{msg}" });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"responseStream ex:{ex.Message}");
                    break;
                }

            }

            await readTask;
        }
    }
}
