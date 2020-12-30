using System;
using System.Diagnostics;
using System.IO;

namespace PrintArguments
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainexe = "D:\\PrintArgs\\PrintArguments.exe";
            var processInfo = new ProcessStartInfo(mainexe)
            {
                WorkingDirectory = Path.GetDirectoryName(mainexe)
            };
            processInfo.Arguments = "-t xxx";
            Process.Start(processInfo);

            //Console.WriteLine("Print args");
            //if (args != null)
            //{
            //    for (int i = 0; i < args.Length; i++)
            //    {
            //        Console.WriteLine($"arg {i + 1} : {args[i]}");
            //    }
            //}
            //Console.ReadLine();
        }
    }
}
