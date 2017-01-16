using System.Net;

namespace Microwin.Exceptions
{
    public class NotFoundException : RestException
    {
        public override HttpStatusCode StatusCode
        {
            get { return HttpStatusCode.NotFound; }
        }

        public NotFoundException()
            : base()
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}
