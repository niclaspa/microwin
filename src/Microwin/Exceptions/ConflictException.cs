using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
