using System.Net;

namespace Microwin.Exceptions
{
    public class BadRequestException : RestException
    {
        public override HttpStatusCode StatusCode
        {
            get { return HttpStatusCode.BadRequest; }
        }

        public BadRequestException()
            : base()
        {
        }

        public BadRequestException(string message)
            : base(message)
        {
        }
    }
}
