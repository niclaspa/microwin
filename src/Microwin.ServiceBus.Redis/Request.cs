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
        public string Action { get; set; }

        public JToken Parameters { get; set; }
    }
}
