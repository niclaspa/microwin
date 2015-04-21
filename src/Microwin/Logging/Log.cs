using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microwin.Logging
{
    public static class Log
    {
        static Log()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static ILog GetLogger()
        {
            return LogManager.GetLogger(string.Empty);
        }

        public static void Debug(string text)
        {
            GetLogger().Debug(text);
        }

        public static void Error(string text)
        {
            GetLogger().Error(text);
        }

        public static void Warn(string text)
        {
            GetLogger().Warn(text);
        }

        public static void Fatal(string text)
        {
            GetLogger().Fatal(text);
        }

        public static void Info(string text)
        {
            GetLogger().Info(text);
        }
    }
}
