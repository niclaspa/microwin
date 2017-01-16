using System.Net;

namespace Microwin.Exceptions
{
    public class UnauthorizedException : RestException
    {
        public override HttpStatusCode StatusCode
        {
            get { return HttpStatusCode.Unauthorized; }
        }

        public UnauthorizedException()
            : base()
        {
        }

        public UnauthorizedException(string message)
            : base(message)
        {
        }
    }
}
