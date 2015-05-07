using Microwin.Extensions;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Topshelf;
using Microwin.Logging;
using Newtonsoft.Json;

namespace Microwin.Hosting.Owin
{
    public class OwinServiceControl : ServiceControl
    {
        private IDisposable webApplication;
        private HttpConfiguration config;
        private JsonSerializerSettings jsonSettings;

        public OwinServiceControl(HttpConfiguration config, JsonSerializerSettings jsonSettings)
        {
            this.config = config;
            this.jsonSettings = jsonSettings;
        }

        public bool Start(HostControl hostControl)
        {
            var listenHosts = LocalConfig.HostNames;
            var options = new StartOptions();
            foreach (var url in listenHosts)
            {
                options.Urls.Add(OwinService.GetBaseUrl("http", url));
            }

            Log.Info("Listening on {0}".InvariantFormat(string.Join(",", options.Urls)));

            this.webApplication = WebApp.Start(options, (x) => WebPipeline.Configure(x, config, jsonSettings));
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            this.webApplication.Dispose();
            return true;
        }
    }
}
