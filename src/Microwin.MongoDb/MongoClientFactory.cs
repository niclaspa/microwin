using iNeed.MongoDb;
using Microwin.MongoDb;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iNeed.MongoDb
{
    public class MongoClientFactory : IMongoClientFactory
    {
        public MongoClient CreateMongoClient()
        {
            return new MongoClient(MongoConfig.MongoDbConnectionString);
        }
    }
}
