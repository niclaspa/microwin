using Microwin.Extensions;
using Microwin.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Microwin.Hosting.Owin
{
    public class HttpLoggingAttribute : ActionFilterAttribute
    {
        private static string getSessionId(HttpRequestMessage msg)
        {
            return msg.GetHashCode().ToString();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception != null)
            {
                this.LogException(actionExecutedContext);
            }
            else if (actionExecutedContext.Response != null)
            {
                // for logging HTTP responses other than 500

                string response = actionExecutedContext.Response.ToString();
                Log.Info(string.Format("{0}: {1}", getSessionId(actionExecutedContext.Request), response));
            }
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // for logging HTTP incoming requests

            var owinContext = actionContext.Request.GetOwinContext();
            var owinRequest = owinContext.Request;

            string uri = actionContext.Request.RequestUri.AbsoluteUri;
            string method = actionContext.Request.Method.ToString().ToUpper();
            string headers = actionContext.Request.Headers.ToString();
            var args = actionContext.ActionArguments;

            string rawRequest = string.Format("{0}: {1} {2} {3} Parameters: {4}", getSessionId(actionContext.Request), method, uri, headers, JsonConvert.SerializeObject(args));
            Log.Info(rawRequest);
        }

        private void LogException(HttpActionExecutedContext actionExecutedContext)
        {
            Log.Error("{0}: Unhandled exception: {1}".InvariantFormat(
                        getSessionId(actionExecutedContext.Request), 
                        actionExecutedContext.Exception));
        }
    }
}

