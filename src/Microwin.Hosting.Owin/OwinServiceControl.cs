using Microwin.Extensions;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Topshelf;

namespace Microwin.Hosting.Owin
{
    public class OwinServiceControl : ServiceControl
    {
        private IDisposable webApplication;
        private HttpConfiguration config;

        public OwinServiceControl(HttpConfiguration config)
        {
            this.config = config;
        }

        public bool Start(HostControl hostControl)
        {
            var listenHosts = LocalConfig.HostNames;
            var options = new StartOptions();
            foreach (var url in listenHosts)
            {
                options.Urls.Add(OwinService.GetBaseUrl("http", url));
            }
            
            this.webApplication = WebApp.Start(options, (x) => WebPipeline.Configure(x, config));
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            this.webApplication.Dispose();
            return true;
        }
    }
}
