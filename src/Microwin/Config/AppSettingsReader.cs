using Microwin.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microwin.Config
{
    public class AppSettingsReader : IAppSettingsReader
    {
        private IAppSettingsProvider provider;

        public AppSettingsReader(IAppSettingsProvider provider)
        {
            if (provider == null) { throw new ArgumentNullException("provider"); }

            this.provider = provider;
        }

        public string ReadString(string key, bool throwIfNullOrWhiteSpace = false)
        {
            string val = this.provider.AppSettings[key];
            if (string.IsNullOrWhiteSpace(val) && throwIfNullOrWhiteSpace)
            {
                throw new KeyNotFoundException("Could not find key '{0}' in app settings".InvariantFormat(key));
            }

            return val ?? string.Empty;
        }

        public string[] ReadStringArray(
            string key, 
            bool throwIfNullOrWhiteSpace = false, 
            bool trimValues = true, 
            bool includeEmptyValues = false)
        {
            IEnumerable<string> values = this.ReadString(key, throwIfNullOrWhiteSpace).Split(',');

            if (trimValues)
            {
                values = values.Select(s => s.Trim());
            }

            if (!includeEmptyValues)
            {
                values = values.Where(s => !string.IsNullOrWhiteSpace(s));
            }

            return values.ToArray();
        }

        public bool ReadBool(string key, bool throwOnError = false)
        {
            bool b = false;
            if (throwOnError)
            {
                b = Boolean.Parse(this.ReadString(key, true));
            }
            else
            {
                Boolean.TryParse(this.ReadString(key, false), out b);
            }

            return b;
        }
    }
}
