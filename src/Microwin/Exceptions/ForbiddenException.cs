using System.Net;

namespace Microwin.Exceptions
{
    public class ForbiddenException : RestException
    {
        public override HttpStatusCode StatusCode
        {
            get { return HttpStatusCode.Forbidden; }
        }

    }
}
