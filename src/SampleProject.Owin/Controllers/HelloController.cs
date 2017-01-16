using System;
using System.Web.Http;
using SampleProject.Owin.ViewModels;
using Microwin.Hosting.Owin;
using SampleProject.Services;

namespace SampleProject.Owin.Controllers
{
    [HttpLogging]
    public class HelloController : ApiController
    {
        private readonly IHelloService helloService;

        public HelloController(IHelloService helloService)
        {
            if (helloService == null) { throw new ArgumentNullException(nameof(helloService)); }

            this.helloService = helloService;
        }

        [HttpGet]
        [Route("hello")]
        public Message GetHello(string name)
        {
            return new Message { Text = $"{this.helloService.GetGreeting()} {name}" };
        }
    }
}
