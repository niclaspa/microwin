using Microwin.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwin.Config
{
    public static class AppSettings
    {
        private static AppSettingsReader settings = new AppSettingsReader(new AppSettingsProvider());

        public static string ReadString(string key, bool throwIfNullOrWhiteSpace = false)
        {
            return settings.ReadString(key, throwIfNullOrWhiteSpace);
        }

        public static IEnumerable<string> ReadStringArray(
            string key,
            bool throwIfNullOrWhiteSpace = false,
            bool trimValues = true,
            bool includeEmptyValues = false)
        {
            return settings.ReadStringArray(key, throwIfNullOrWhiteSpace, trimValues, includeEmptyValues);
        }

        public static bool ReadBool(string key, bool throwOnError)
        {
            return settings.ReadBool(key, throwOnError);
        }
    }
}
