using Microwin.IoC;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Microwin.ServiceBus.Redis
{
    public interface IRequestProcessor
    {
        string Endpoint { get; }

        Task ProcessRequest(IDependencyScope resolver, JToken viewModel);
    }
}
