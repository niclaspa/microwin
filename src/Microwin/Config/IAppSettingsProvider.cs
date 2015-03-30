using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwin.Config
{
    public interface IAppSettingsProvider
    {
        NameValueCollection AppSettings { get; }
    }
}
