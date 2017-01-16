using Microwin.Extensions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Topshelf;
using Microwin.Config;
using Microwin.IoC;
using StackExchange.Redis;
using Microwin.Logging;

namespace Microwin.ServiceBus.Redis
{
    public class MessageHandler : ServiceControl
    {
        private readonly Dictionary<string, IRequestProcessor> requestProcessors;
        private readonly IDependencyResolver resolver;
        private readonly string channel;
        private readonly string baseAddress;
        private ConnectionMultiplexer redisClient;
        private readonly Action onStart;
        private readonly Action onStop;

        private readonly int maxWorkerThreads = Convert.ToInt32(AppSettings.ReadString("MaxWorkerThreads", true));

        public MessageHandler(string channel, IDependencyResolver resolver, IEnumerable<IRequestProcessor> processors, Action onStart, Action onStop)
        {
            this.channel = channel;
            this.resolver = resolver;
            this.requestProcessors = processors.ToDictionary(x => x.Endpoint);
            this.baseAddress = AppSettings.ReadString("RedisBaseAddress", true);
            this.onStart = onStart;
            this.onStop = onStop;
        }

        public bool Start(HostControl hostControl)
        {
            if (onStart != null)
            {
                this.onStart();
            }

            if (this.redisClient == null)
            {
                this.redisClient = ConnectionMultiplexer.Connect(this.baseAddress);
            }

            for (int i = 0; i < maxWorkerThreads; i++)
            {
                this.redisClient.GetSubscriber().Subscribe(
                    this.channel,
                    (c, v) =>
                    {
                        Task.Run(
                          async () =>
                          {
                              string work;
                              do
                              {
                                  work = this.redisClient.GetDatabase().ListLeftPop(this.channel);
                                  if (work != null)
                                  {
                                      await this.HandleRequest(work);
                                  }
                              } while (work != null);
                          });
                    });
            }

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            if (this.onStop != null)
            {
                this.onStop();
            }

            return true;
        }

        private async Task HandleRequest(string json)
        {
            try
            {
                using (var container = this.resolver.BeginScope())
                {
                    Log.Info("Message received: {0}".InvariantFormat(json));

                    var request = this.ParseRequest(json);
                    var processor = this.ResolveProcessor(request.Action);
                    await processor.ProcessRequest(container, request.ViewModel);
                }
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }

            Log.Info("Finished processing message");
        }

        private ServiceBusRequest ParseRequest(string json)
        {
            JObject obj = JObject.Parse(json);
            var request = obj.ToObject<ServiceBusRequest>();

            if (string.IsNullOrWhiteSpace(request.Action))
            {
                throw new ArgumentException("No action in message");
            }

            return request;
        }

        private IRequestProcessor ResolveProcessor(string action)
        {
            IRequestProcessor processor;
            if (!this.requestProcessors.TryGetValue(action, out processor))
            {
                throw new ArgumentException("No request processor registered for action {0}".InvariantFormat(action));
            }

            return processor;
        }
    }
}
