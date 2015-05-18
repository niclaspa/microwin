using Microwin.MongoDb;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwin.MongoDb
{
    public class MongoClientFactory : IMongoClientFactory
    {
        public MongoClient CreateMongoClient()
        {
            return new MongoClient(MongoConfig.MongoDbConnectionString);
        }
    }
}
