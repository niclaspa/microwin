using System.Net;

namespace Microwin.Exceptions
{
    public class ConflictException : RestException
    {
        public override HttpStatusCode StatusCode
        {
            get { return HttpStatusCode.Conflict; }
        }

    }
}
