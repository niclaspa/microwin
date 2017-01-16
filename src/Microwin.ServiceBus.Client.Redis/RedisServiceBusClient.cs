using Newtonsoft.Json;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace Microwin.ServiceBus.Client.Redis
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
                JsonConvert.SerializeObject(new ServiceBusRequest(action, viewModel)));

            // notify all workers subscribing to that queue
            db.Publish(channel, string.Empty);
        }

        public async Task PostRequestAsync(string channel, string action, object viewModel)
        {
            var db = client.GetDatabase();

            // post the message on the queue
            await db.ListRightPushAsync(
                channel,
                JsonConvert.SerializeObject(new ServiceBusRequest(action, viewModel)));

            // notify all workers subscribing to that queue
            await db.PublishAsync(channel, string.Empty);
        }

        public void Dispose()
        {
            this.client.Dispose();
        }
    }
}
