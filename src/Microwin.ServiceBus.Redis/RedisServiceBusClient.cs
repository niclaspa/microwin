using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwin.ServiceBus.Redis
{
    public class RedisServiceBusClient : IServiceBusClient
    {
        private ConnectionMultiplexer client;

        public RedisServiceBusClient(string baseAddress)
        {
            this.client = ConnectionMultiplexer.Connect(baseAddress);
        }

        public void PostRequest(string channel, string action, object viewModel)
        {
            var db = client.GetDatabase();
            
            // post the message on the queue
            db.ListRightPush(
                channel,
                JsonConvert.SerializeObject(new Request(action, viewModel)));

            // notify all workers subscribing to that queue
            db.Publish(channel, string.Empty);
        }

        public void Dispose()
        {
            this.client.Dispose();
        }
    }
}
