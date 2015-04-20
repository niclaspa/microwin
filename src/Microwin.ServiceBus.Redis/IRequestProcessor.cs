using Microwin.IoC;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwin.ServiceBus.Redis
{
    public interface IRequestProcessor
    {
        string Endpoint { get; }

        Task ProcessRequest(IDependencyScope resolver, JToken viewModel);
    }
}
