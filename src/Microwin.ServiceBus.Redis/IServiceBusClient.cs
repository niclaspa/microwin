using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwin.ServiceBus.Redis
{
    public interface IServiceBusClient : IDisposable
    {
        void PostRequest(string channel, string action, object viewModel);
    }
}
