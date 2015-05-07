using Microwin;
using Microwin.Config;
using Microwin.Extensions;
using Microwin.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Topshelf;

namespace Microwin.Hosting.Owin
{
    public class OwinService
    {
        private static readonly string name = AppSettings.ReadString("ServiceName", true);

        public static void Start()
        {
            Start(new HttpConfiguration());
        }

        public static void Start(HttpConfiguration config, JsonSerializerSettings jsonSettings = null)
        {
            Log.Info("Starting {0} ...".InvariantFormat(name));

            HostFactory.Run(x =>
            {
                x.Service(() => new OwinServiceControl(config, jsonSettings));
                x.RunAsLocalSystem();
                x.EnableShutdown();

                x.SetDescription(name);
                x.SetDisplayName(name);
                x.SetServiceName(name);
            });
        }

        public static string GetBaseUrl(string scheme, string hostName)
        {
            string port = string.Empty;
            if (scheme == "http")
            {
                port = ":{0}".InvariantFormat(LocalConfig.Port);
            }

            hostName.TrimEnd('/');

            return "{0}://{1}{2}/{3}/{4}".InvariantFormat(scheme, hostName, port, LocalConfig.ServiceId, LocalConfig.Version);
        }
    }
}
