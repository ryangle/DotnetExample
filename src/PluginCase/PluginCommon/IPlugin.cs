using System;

namespace PluginCommon
{
    public interface IPlugin : IDisposable
    {
        string GetMessage();
    }
}
