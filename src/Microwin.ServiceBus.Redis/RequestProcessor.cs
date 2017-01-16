using Microwin.IoC;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace Microwin.ServiceBus.Redis
{
    public class RequestProcessor<TController, TViewModel> : IRequestProcessor
    {
        private Func<TController, TViewModel, Task> executeAsync;
        private Action<TController, TViewModel> execute;

        public string Endpoint { get; private set; }

        public RequestProcessor(string endpoint, Action<TController, TViewModel> execute)
        {
            this.execute = execute;
            this.Endpoint = endpoint;
        }

        public RequestProcessor(string endpoint, Func<TController, TViewModel, Task> executeAsync)
        {
            this.executeAsync = executeAsync;
            this.Endpoint = endpoint;
        }

        public async Task ProcessRequest(IDependencyScope scope, JToken viewModelToken)
        {
            var controller = (TController)scope.GetService(typeof(TController));
            var viewModel = viewModelToken.ToObject<TViewModel>();

            if (this.executeAsync != null)
            {
                await this.executeAsync(controller, viewModel);
            }
            else
            {
                this.execute(controller, viewModel);
            }
        }
    }
}
