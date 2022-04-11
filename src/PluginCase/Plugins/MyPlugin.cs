using PluginCommon;
using System;

namespace Plugins
{
    public class MyPlugin : IPlugin
    {
        public MyPlugin()
        {
            Console.WriteLine("MyPlugin loaded");
        }

        public string GetMessage()
        {
            return "Hello 1";
        }

        public void Dispose()
        {
            Console.WriteLine("MyPlugin unloaded");
        }
    }
}
