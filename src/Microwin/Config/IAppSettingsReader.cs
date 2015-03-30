using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwin.Config
{
    public interface IAppSettingsReader
    {
        string ReadString(string key, bool throwIfNullOrWhiteSpace = false);

        string[] ReadStringArray(
            string key, 
            bool throwIfNullOrWhiteSpace = false, 
            bool trimValues = true, 
            bool includeEmptyValues = false);

        bool ReadBool(string key, bool throwOnError = false);
    }
}
