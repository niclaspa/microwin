using SampleProject.Owin.ViewModels;
using System;
using System.Threading.Tasks;
using SampleProject.Services;

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
