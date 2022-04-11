using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
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
            var tokenSource = new CancellationTokenSource();
            //return;
            var raceDuration = TimeSpan.Parse(context.RequestHeaders.Single(h => h.Key == "race-duration").Value);
            Console.WriteLine("RacerService Start ReadySetGo");
            // Read incoming messages in a background task
            RaceMessage? lastMessageReceived = null;
            //var readTask = Task.Run(async () =>
            //{
            _ = Task.Factory.StartNew(() =>
              {
                  Console.WriteLine($"enter d to cancel read all");
                  var x = Console.ReadLine();
                  if (x == "d")
                  {
                      tokenSource.Cancel();
                  }
              });

            Console.WriteLine($"RequestStream ReadAllAsync");
            try
            {
                await foreach (var message in requestStream.ReadAllAsync(tokenSource.Token))
                {
                    Console.WriteLine($"RacerService requestStream.ReadAllAsync {message.Count}");
                    lastMessageReceived = message;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RacerService requestStream ex:{ex.Message}");
            }

            //try
            //{

            //    while (await requestStream.MoveNext())
            //    {
            //        Console.WriteLine($"RacerService requestStream.ReadAllAsync {requestStream.Current.Count}");
            //    }
            //    
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"RacerService requestStream ex:{ex.Message}");
            //}
            Console.WriteLine($"RacerService requestStream end");
            //});

            // Write outgoing messages until timer is complete
            //var sw = Stopwatch.StartNew();
            //var sent = 0;
            //while (true)
            //{
            //    try
            //    {
            //        var msg = Console.ReadLine();
            //        await responseStream.WriteAsync(new RaceMessage { Count = ++sent, Message = $"Server say:{msg}" });
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine($"responseStream ex:{ex.Message}");
            //        break;
            //    }

            //}

            //await readTask;
        }
    }
}
