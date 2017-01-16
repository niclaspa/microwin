using Newtonsoft.Json.Linq;

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
