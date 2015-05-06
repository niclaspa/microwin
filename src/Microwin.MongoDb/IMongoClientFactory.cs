using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iNeed.MongoDb
{
    public interface IMongoClientFactory
    {
        MongoClient CreateMongoClient();
    }
}
