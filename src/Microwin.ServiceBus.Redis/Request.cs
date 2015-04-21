using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwin.ServiceBus.Redis
{
    public class Request
    {
        public Request() { }

        public Request(string action, JToken viewModel)
        {
            this.Action = action;
            this.ViewModel = viewModel;
        }

        public Request(string action, object viewModel)
            : this(action, JToken.FromObject(viewModel))
        {
        }

        public string Action { get; set; }

        public JToken ViewModel { get; set; }
    }
}
