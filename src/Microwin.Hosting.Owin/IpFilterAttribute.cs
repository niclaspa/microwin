using Microwin.Hosting.Owin;
using Microwin.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Microwin.Hosting.Owin
{
    public class IpFilterAttribute : AuthorizeAttribute
    {
        private HashSet<string> allowedIps = new HashSet<string>();

        public IpFilterAttribute()
        {
            string allowedIpsString = LocalConfig.AllowedIps;
            if (string.IsNullOrWhiteSpace(allowedIpsString))
            {
                throw new ArgumentException("allowedIpsString null or empty");
            }

            var ips = allowedIpsString.Split(',');
            foreach (var ip in ips)
            {
                if (!string.IsNullOrWhiteSpace(ip))
                {
                    this.allowedIps.Add(ip.Trim());
                }
            }
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var owinContext = actionContext.Request.GetOwinContext();
            string userIp = owinContext.Request.RemoteIpAddress;

            if (!LocalConfig.EnableIpFiltering || this.IsAuthorized(userIp))
            {
                return;
            }

            Log.Debug("Denying access from: " + userIp);

            base.HandleUnauthorizedRequest(actionContext);
        }

        private bool IsAuthorized(string userHostAddress)
        {
            if (!string.IsNullOrWhiteSpace(userHostAddress))
            {
                return this.allowedIps.Contains(userHostAddress);
            }

            return false;
        }
    }
}
