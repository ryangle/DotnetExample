using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Race;
using Microsoft.Win32;
using System.Threading;

namespace Client
{
    class Program
    {
        private static readonly TimeSpan RaceDuration = TimeSpan.FromSeconds(30);
        private static CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
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
            //var call1 = client.ReadySetGo(new CallOptions(headers));
            //await call1.RequestStream.WriteAsync(new RaceMessage { Count = 111, Message = $"Client say:dddd" });

            Console.WriteLine("Ready, set, go!");
            using (var call = client.ReadySetGo(new CallOptions(headers, cancellationToken: _cancellationTokenSource.Token)))
            {
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
                    Console.WriteLine($"ResponseStream Task end");
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

                while (sent < 10)
                {
                    try
                    {
                        var msg = Console.ReadLine();
                        if (msg == "c")
                        {
                            _cancellationTokenSource.Cancel();
                        }else if (msg == "d")
                        {
                            call.Dispose();
                        }
                        msg = "test";
                        await call.RequestStream.WriteAsync(new RaceMessage { Count = ++sent, Message = $"Client say:{msg}" });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"RequestStream ex:{ex.Message}");
                        break;
                    }
                }
                Console.WriteLine($"RequestStream CompleteAsync");
                // Finish call and report results
                //await call.RequestStream.CompleteAsync();
                //await readTask;

                complete = true;
                //await reportTask;
            }
        }


        /// <summary>
        /// 判断注册表项是否存在
        /// </summary>
        /// <param name="sKeyName"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        private bool IsRegistryKeyExist(string sKeyName)
        {
            var hklm = Registry.LocalMachine;
            var hkSoftWare = hklm.OpenSubKey(@"SOFTWARE");
            var sKeyNameColl = hkSoftWare.GetSubKeyNames(); //获取SOFTWARE下所有的子项
            foreach (string sName in sKeyNameColl)
            {
                if (sName == sKeyName)
                {
                    hklm.Close();
                    hkSoftWare.Close();
                    return true;
                }
            }
            hklm.Close();
            hkSoftWare.Close();
            return false;
        }


        /// <summary>
        /// 判断键值是否存在
        /// </summary>
        /// <param name="sValueName"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        private bool IsRegistryValueNameExist(string sValueName)
        {
            var hklm = Registry.LocalMachine;
            var hkTest = hklm.OpenSubKey(@"SOFTWARE\test");
            var sValueNameColl = hkTest.GetValueNames(); //获取test下所有键值的名称
            foreach (string sName in sValueNameColl)
            {
                if (sName == sValueName)
                {
                    hklm.Close();
                    hkTest.Close();
                    return true;
                }
            }
            hklm.Close();
            hkTest.Close();
            return false;
        }
    }
}
