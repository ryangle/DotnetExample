using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Akka;
using Akka.Actor;
using Akka.IO;
using Akka.Streams;
using Akka.Streams.Dsl;
using Akka.Streams.IO;
using Serilog;

namespace StreamDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}[{Level:u3}]({SourceContext}){Message:lj}{NewLine}{Exception}")
            .MinimumLevel.Debug()
            .CreateLogger();

            using (var system = ActorSystem.Create("system"))
            {
                var worker = system.ActorOf<WorkActor>();
                worker.Tell("hello");
                worker.Tell(111);

                //using (var materializer = system.Materializer())
                //{
                //    Source<int, NotUsed> source = Source.From(Enumerable.Range(1, 100));
                //    //source.RunForeach(i => Console.WriteLine(i.ToString()), materializer);

                //    var factorials = source.Scan(new BigInteger(1), (acc, next) => acc * next);
                //    var result =
                //        factorials
                //            .Select(num => ByteString.FromString($"{num}\n"))
                //            .RunWith(FileIO.ToFile(new FileInfo("factorials.txt")), materializer);
                //}
                Console.WriteLine("Finished!");
                Console.ReadLine();
            }
        }
        public static Sink<string, Task<IOResult>> LineSink(string filename)
        {
            return Flow.Create<string>()
              .Select(s => ByteString.FromString($"{s}\n"))
              .ToMaterialized(FileIO.ToFile(new FileInfo(filename)), Keep.Right);
        }
    }
}
