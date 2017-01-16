using Microwin.Exceptions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Microwin.Hosting.Owin
{
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var restException = context.Exception as RestException;

            if (restException != null)
            {
                HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { Error = context.Exception.Message })),
                    ReasonPhrase = restException.StatusCode.ToString(),
                    StatusCode = restException.StatusCode
                };

                context.Response = msg;
            }
        }
    }
}
