using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
