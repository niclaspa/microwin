using System.Collections.Specialized;

namespace Microwin.Config
{
    public interface IAppSettingsProvider
    {
        NameValueCollection AppSettings { get; }
    }
}
