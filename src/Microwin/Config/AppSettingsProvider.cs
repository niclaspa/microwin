using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwin.Config
{
    public class AppSettingsProvider : IAppSettingsProvider
    {
        public NameValueCollection AppSettings
        {
            get 
            { 
                return ConfigurationManager.AppSettings; 
            }
        }
    }
}
