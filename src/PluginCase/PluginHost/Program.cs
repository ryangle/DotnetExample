using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;

namespace PluginHost
{
   
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var controller = new PluginController("MyPlugin", "../Plugins"))
            {
                bool keepRunning = true;
                Console.CancelKeyPress += (sender, e) =>
                {
                    e.Cancel = true;
                    keepRunning = false;
                };
                while (keepRunning)
                {
                    try
                    {
                        Console.WriteLine(controller.GetMessage());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex.GetType()}: {ex.Message}");
                    }
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
