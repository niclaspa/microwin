using SampleProject.Owin.ViewController;
using Microwin.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SampleProject.Owin.Controllers
{
    public class HelloController : ApiController
    {
        [HttpGet]
        [Route("hello")]
        public Message GetHello(string name)
        {
            return new Message { Text = "Hello {0}".InvariantFormat(name) };
        }
    }
}
