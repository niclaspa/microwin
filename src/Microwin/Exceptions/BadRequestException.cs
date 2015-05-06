using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
