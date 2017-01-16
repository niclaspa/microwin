using System;
using System.Threading.Tasks;
using SampleProject.ServiceBus.ViewModels;
using SampleProject.Services;

namespace SampleProject.ServiceBus.Controllers
{
    public class HelloController
    {
        private readonly IHelloService helloService;

        public HelloController(IHelloService helloService)
        {
            if (helloService == null) { throw new ArgumentNullException(nameof(helloService)); }

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
