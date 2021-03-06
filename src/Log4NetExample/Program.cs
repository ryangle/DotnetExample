﻿using log4net;
using log4net.Config;
using System;
using System.Reflection;

namespace Log4NetExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("log4net.config"));
            ILog log = LogManager.GetLogger(typeof(Program));
            //ILog log2 = LogManager.GetLogger("log2");

            log.Info("------- Hello World! -------------------");
            //log2.Info("------- Hello World! -------------------");

        }
    }
}
