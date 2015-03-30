using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Microwin.Hosting.Owin.Extensions
{
    public static class HttpRequestMessageExtensions
    {
        public static string GetOriginalUriScheme(this HttpRequestMessage request)
        {
            string scheme = request.RequestUri.Scheme;
            IEnumerable<string> values;
            if (request.Headers.TryGetValues("X-Forwarded-Proto", out values) && values.Count() > 0)
            {
                var val = values.First();
                if (!string.IsNullOrWhiteSpace(val))
                {
                    scheme = val;
                }
            }

            return scheme;
        }
    }
}
