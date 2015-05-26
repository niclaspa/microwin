using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwin.ServiceBus
{
    public class ServiceBusRequest
    {
        public ServiceBusRequest() { }

        public ServiceBusRequest(string action, JToken viewModel)
        {
            this.Action = action;
            this.ViewModel = viewModel;
        }

        public ServiceBusRequest(string action, object viewModel)
            : this(action, JToken.FromObject(viewModel))
        {
        }

        public string Action { get; set; }

        public JToken ViewModel { get; set; }
    }
}
