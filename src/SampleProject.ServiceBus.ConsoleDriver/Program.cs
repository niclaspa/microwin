using Microwin.ServiceBus.Redis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SampleProject.Owin.ViewModels;
using ServiceStack.Redis;
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
            var client = new RedisClient("localhost");

            client.EnqueueItemOnList(
                "SampleProject", 
                JsonConvert.SerializeObject(new Request("post_message", new Message { Text = "hello there" })));

            client.EnqueueItemOnList(
                "SampleProject",
                JsonConvert.SerializeObject(new Request("post_message_async", new Message { Text = "hello there async" })));
        }
    }
}
