using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Race;

namespace Client
{
    class Program
    {
        private static readonly TimeSpan RaceDuration = TimeSpan.FromSeconds(30);

        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Racer.RacerClient(channel);

            Console.WriteLine($"Race duration: {RaceDuration.TotalSeconds} seconds");
            Console.WriteLine("Press any key to start race...");
            Console.ReadKey();
            var reconnect = true;
            while (reconnect)
            {
                await BidirectionalStreamingExample(client);
                Console.WriteLine("Press r key to reconnect...");
                reconnect = Console.ReadLine() == "r";

            }

            Console.WriteLine("Finished");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static async Task BidirectionalStreamingExample(Racer.RacerClient client)
        {
            var headers = new Metadata { new Metadata.Entry("race-duration", RaceDuration.ToString()) };

            Console.WriteLine("Ready, set, go!");
            using var call = client.ReadySetGo(new CallOptions(headers));
            var complete = false;

            // Read incoming messages in a background task
            RaceMessage? lastMessageReceived = null;
            var readTask = Task.Run(async () =>
            {
                try
                {
                    await foreach (var message in call.ResponseStream.ReadAllAsync())
                    {
                        lastMessageReceived = message;
                        Console.WriteLine($"{message.Message}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ResponseStream ex:{ex.Message}");
                }
            });

            // Write outgoing messages until timer is complete
            //var sw = Stopwatch.StartNew();
            var sent = 0;

            #region Reporting
            // Report requests in realtime
            //var reportTask = Task.Run(async () =>
            //{
            //    while (true)
            //    {
            //        Console.WriteLine($"Messages sent: {sent}");
            //        Console.WriteLine($"Messages received: {lastMessageReceived?.Count ?? 0}");

            //        if (!complete)
            //        {
            //            await Task.Delay(TimeSpan.FromSeconds(1));
            //            Console.SetCursorPosition(0, Console.CursorTop - 2);
            //        }
            //        else
            //        {
            //            break;
            //        }
            //    }
            //});
            #endregion

            while (true)
            {
                try
                {
                    var msg = Console.ReadLine();
                    await call.RequestStream.WriteAsync(new RaceMessage { Count = ++sent, Message = $"Client say:{msg}" });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"RequestStream ex:{ex.Message}");
                    break;
                }
            }

            // Finish call and report results
            await call.RequestStream.CompleteAsync();
            await readTask;

            complete = true;
            //await reportTask;
        }
    }
}
