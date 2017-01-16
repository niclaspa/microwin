using Microwin.Config;

namespace Microwin.MongoDb
{
    public static class MongoConfig
    {
        public static string MongoDbConnectionString { get { return AppSettings.ReadString("MongoDbConnectionString", true); } }
    }
}
