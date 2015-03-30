using Microwin.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwin.Hosting.Owin
{
    internal static class LocalConfig
    {
        public static IEnumerable<string> HostNames { get { return AppSettings.ReadStringArray("ListenHostNames", true); } }

        public static string AllowedIps { get { return AppSettings.ReadString("AllowedIps", true); } }

        public static bool EnableIpFiltering { get { return AppSettings.ReadBool("EnableIpFiltering", false); } }

        public static bool PublishApiDocs { get { return AppSettings.ReadBool("PublishApiDocs", false); } }
        
        public static string Port { get { return AppSettings.ReadString("Port", true); } }
        
        public static string ServiceId { get { return AppSettings.ReadString("ServiceId", true); } }
        
        public static string Version { get { return AppSettings.ReadString("Version", true); } }

        public static string XmlDocRelativePath { get { return AppSettings.ReadString("XmlDocRelativePath", false); } }
    }
}
