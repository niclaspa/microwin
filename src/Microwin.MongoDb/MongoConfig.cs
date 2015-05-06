using Microwin.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwin.MongoDb
{
    public static class MongoConfig
    {
        public static string MongoDbConnectionString { get { return AppSettings.ReadString("MongoDbConnectionString", true); } }
    }
}
