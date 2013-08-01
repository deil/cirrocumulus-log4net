using System;
using log4net.Config;
using log4net;
using System.IO;
using System.ServiceModel;
using Cirrocumulus;

namespace test
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo("log4net.config"));
            LogManager.GetLogger(typeof(MainClass)).Info("Hello World!");

            return;
        }
    }
}
