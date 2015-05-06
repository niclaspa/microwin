using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Microwin.Exceptions
{
    public class RestException : Exception
    {
        public virtual HttpStatusCode StatusCode { get; private set; }

        public RestException()
            : base()
        {

        }

        public RestException(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
        }

        public RestException(string message)
            : base(message)
        {

        }

        public RestException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
