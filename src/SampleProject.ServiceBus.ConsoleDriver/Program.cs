using Microwin.ServiceBus.Redis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SampleProject.Owin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
