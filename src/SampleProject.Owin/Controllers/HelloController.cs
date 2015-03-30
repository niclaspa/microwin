using SampleProject.Owin.ViewController;
using Microwin.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using SampleProject.Owin.Services;

namespace SampleProject.Owin.Controllers
{
    public class HelloController : ApiController
    {
        private IHelloService helloService;

        public HelloController(IHelloService helloService)
        {
            if (helloService == null) { throw new ArgumentNullException("helloService"); }

            this.helloService = helloService;
        }

        [HttpGet]
        [Route("hello")]
        public Message GetHello(string name)
        {
            return new Message { Text = "{0} {1}".InvariantFormat(this.helloService.GetGreeting(), name) };
        }
    }
}
