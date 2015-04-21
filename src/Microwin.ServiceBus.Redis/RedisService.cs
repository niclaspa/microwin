using Microwin.Config;
using Microwin.Extensions;
using Microwin.IoC;
using Microwin.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Microwin.ServiceBus.Redis
{
    public class RedisService
    {
        private static readonly string name = AppSettings.ReadString("ServiceName", true);

        public static void Start(string channel, IDependencyResolver resolver, params IRequestProcessor[] processors)
        {
            Log.Info("Starting {0} ...".InvariantFormat(name));

            HostFactory.Run(x =>
            {
                x.Service(() => new MessageHandler(channel, resolver, processors));
                x.RunAsLocalSystem();
                x.EnableShutdown();

                x.SetDescription(name);
                x.SetDisplayName(name);
                x.SetServiceName(name);
            });
        }
    }
}
