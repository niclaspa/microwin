using Microwin.Extensions;
using Microwin.Hosting.Owin.Extensions;
using Newtonsoft.Json;
using Owin;
using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;

namespace Microwin.Hosting.Owin
{
    public static class WebPipeline
    {
        public static void Configure(IAppBuilder application, HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            ConfigureApiDocs(config);
            application.UseWebApi(config);
        }

        public static void ConfigureApiDocs(HttpConfiguration config)
        {
            if (LocalConfig.PublishApiDocs)
            {
                config.EnableSwagger(c =>
                    {
                        c.IncludeXmlComments(@"{0}\{1}".InvariantFormat(AppDomain.CurrentDomain.BaseDirectory, LocalConfig.XmlDocRelativePath));
                        c.RootUrl(x => OwinService.GetBaseUrl(x.GetOriginalUriScheme(), x.RequestUri.Host));
                        c.SingleApiVersion(LocalConfig.Version, LocalConfig.Version);
                    })
                    .EnableSwaggerUi();
            }
        }
    }
}
