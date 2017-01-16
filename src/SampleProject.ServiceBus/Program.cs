using Microwin.Config;
using Microwin.IoC.Unity;
using Microwin.ServiceBus.Redis;
using SampleProject.IoC;
using SampleProject.ServiceBus.Controllers;
using SampleProject.ServiceBus.ViewModels;

namespace SampleProject.ServiceBus
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RedisService.Start(
                AppSettings.ReadString("ServiceName", true), 
                new UnityDependencyResolver(UnityConfig.RegisterGlobalComponents()), 
                new RequestProcessor<HelloController, Message>("post_message", (c, m) => c.PostMessage(m)), 
                new RequestProcessor<HelloController, Message>("post_message_async", async (c, m) => await c.PostMessageAsync(m)));
        }
    }
}
