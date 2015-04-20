using Microwin.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using System.Threading;
using Microwin.ServiceBus.Redis;
using Microwin.Config;
using Microwin.IoC;
using ServiceStack.Redis;

namespace Microwin.ServiceBus.Redis
{
    public class MessageHandler : ServiceControl, IDisposable
    {
        //private ILog log = LogHelper.GetLogger();

        private Dictionary<string, IRequestProcessor> requestProcessors;
        private IDependencyResolver resolver;
        private string channel;
        private string baseAddress;

        private readonly int maxWorkerThreads = Convert.ToInt32(AppSettings.ReadString("MaxWorkerThreads", true));
        
        public MessageHandler(string channel, IDependencyResolver resolver, IEnumerable<IRequestProcessor> processors)
        {
            this.channel = channel;
            this.requestProcessors = processors.ToDictionary(x => x.Endpoint);
            this.baseAddress = AppSettings.ReadString("RedisBaseAddress", true);
        }

        public bool Start(HostControl hostControl)
        {
            using (var client = new RedisClient(this.baseAddress))
            {
                Task.Run(
                    () =>
                    {
                        int count = 0;
                        var evt = new ManualResetEvent(true);
                        while (true)
                        {
                            if (count >= maxWorkerThreads)
                            {
                                evt.Reset();
                            }
                            evt.WaitOne();
                            var msg = client.BlockingDequeueItemFromList(channel, null);
                            Interlocked.Increment(ref count);
                            Task.Run(async () => await this.HandleRequest(msg))
                                .ContinueWith(
                                    t => 
                                    {
                                        Interlocked.Decrement(ref count);
                                        evt.Set();
                                    });
                        }

                    });
            }

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            return true;
        }

        private async Task HandleRequest(string json)
        {
            try
            {
                using (var container = this.resolver.BeginScope())
                {
                    //log.Info("Message received: " + json);

                    var request = this.ParseRequest(json);
                    var processor = this.ResolveProcessor(request.Action);
                    await processor.ProcessRequest(container, request.Parameters);
                }
            }
            catch (Exception e)
            {
                //log.Error(e.ToString());
            }

            //log.Info("Finished processing message");
        }

        private Request ParseRequest(string json)
        {
            JObject obj = JObject.Parse(json);
            var request = obj.ToObject<Request>();

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

        public void Dispose()
        {
            this.resolver.Dispose();
        }
    }
}
