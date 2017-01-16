using System.Collections.Specialized;
using System.Configuration;

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
