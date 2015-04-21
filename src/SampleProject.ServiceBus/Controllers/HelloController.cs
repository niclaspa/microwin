using SampleProject.Owin.Services;
using SampleProject.Owin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.ServiceBus.Controllers
{
    public class HelloController
    {
        private IHelloService helloService;

        public HelloController(IHelloService helloService)
        {
            if (helloService == null) { throw new ArgumentNullException("helloService"); }

            this.helloService = helloService;
        }

        public void PostMessage(Message message)
        {
            this.helloService.PostMessage(message.Text);
        }

        public async Task PostMessageAsync(Message message)
        {
            await this.helloService.PostMessageAsync(message.Text);
        }
    }
}
