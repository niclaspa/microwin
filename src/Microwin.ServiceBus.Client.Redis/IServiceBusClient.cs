using System;
using System.Threading.Tasks;

namespace Microwin.ServiceBus.Client.Redis
{
    public interface IServiceBusClient : IDisposable
    {
        void PostRequest(string channel, string action, object viewModel);

        Task PostRequestAsync(string channel, string action, object viewModel);
    }
}
