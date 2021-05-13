using System;
using System.Diagnostics;
using System.IO;
using System.Timers;

namespace PrintArguments
{
    class Program
    {
        static Timer t = new Timer();
        static void Main(string[] args)
        {
            
            t.Interval = 1000;
            t.AutoReset = true;
            //设置是否执行System.Timers.Timer.Elapsed事件
            t.Enabled = true;
            //绑定Elapsed事件
            t.Elapsed += new System.Timers.ElapsedEventHandler(TimerUp);

            t.Start();
            Console.WriteLine($"Timer start {t.Enabled}");
            Console.ReadLine();
            t.Stop();
            Console.WriteLine($"Timer stop {t.Enabled}");
            Console.ReadLine();
            t.Start();

            Console.WriteLine($"Timer start {t.Enabled}");
            Console.ReadLine();
            //var mainexe = "D:\\PrintArgs\\PrintArguments.exe";
            //var processInfo = new ProcessStartInfo(mainexe)
            //{
            //    WorkingDirectory = Path.GetDirectoryName(mainexe)
            //};
            //processInfo.Arguments = "-t xxx";
            //Process.Start(processInfo);

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
        /// <summary>
        /// Timer类执行定时到点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void TimerUp(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine($"TimerUp");
            t.Stop();
        }
    }
}
