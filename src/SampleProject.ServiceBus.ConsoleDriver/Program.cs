using Microwin.ServiceBus.Client.Redis;
using SampleProject.ServiceBus.ViewModels;

namespace SampleProject.ServiceBus.ConsoleDriver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var client = new RedisServiceBusClient("localhost"))
            {
                client.PostRequest("SampleProject", "post_message", new Message { Text = "hello there" });
                client.PostRequest("SampleProject", "post_message_async", new Message { Text = "hello there async" });
            }
        }
    }
}
