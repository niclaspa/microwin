using iNeed.MongoDb;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iNeed.MongoDb.Models;
using Microwin.Exceptions;
using Microwin.Extensions;
using iNeed.Exceptions;

namespace iNeed.MongoDb.Repositories
{
    public class CrudRepository<TKey, TVal> : ICrudRepository<TKey, TVal>
        where TVal : class, IDocument<TKey>
    {
        protected IMongoCollection<TVal> collection;

        public CrudRepository(IMongoClientFactory clientFactory, string dbName, string collectionName)
        {
            if (clientFactory == null) { throw new ArgumentNullException("clientFactory"); }

            var db = clientFactory.CreateMongoClient().GetDatabase(dbName);
            this.collection = db.GetCollection<TVal>(collectionName);
        }

        public async Task<TKey> Create(TVal model)
        {
            this.PreCreate(model);

            model.Version = 1;
            try
            {
                await this.collection.InsertOneAsync(model);
            }
            catch (MongoWriteException e)
            {
                if (e.WriteError.Category == ServerErrorCategory.DuplicateKey)
                {
                    throw new ConflictException();
                }

                throw;
            }

            return model.Id;
        }

        public async Task CreateOrReplace(TVal model)
        {
            this.PreCreate(model);

            model.Version = 1;
            await this.collection.ReplaceOneAsync(
                Builders<TVal>.Filter.Eq(x => x.Id, model.Id),
                model,
                new UpdateOptions { IsUpsert = true });
        }

        public async Task CreateOrReplace(IEnumerable<TVal> models)
        {
            foreach (var model in models)
            {
                await this.CreateOrReplace(model);
            }
        }

        public async Task<TVal> Load(TKey id)
        {
            return await this.collection.Find(Builders<TVal>.Filter.Eq(x => x.Id, id)).FirstOrDefaultAsync();
        }

        public async Task<List<TVal>> LoadAll(IEnumerable<TKey> ids)
        {
            return await this.collection.Find(Builders<TVal>.Filter.In(x => x.Id, ids)).ToListAsync();
        }

        public async Task Update(TVal model)
        {
            if (object.Equals(model.Id, default(TKey)))
            {
                throw new ArgumentException("Can't insert/update with default id");
            }
            else if (model.Version <= 0)
            {
                throw new ArgumentException("Document version needs to be a positive integer");
            }

            this.PreUpdate(model);

            var preUpdateModelVersion = model.Version;
            model.Version++;

            TVal replacementValue = null;
            try
            {
                var b = Builders<TVal>.Filter;
                replacementValue = await this.collection.FindOneAndReplaceAsync<TVal>(
                    b.Eq(x => x.Id, model.Id) & 
                    b.Eq(x => x.Version, preUpdateModelVersion), model);
            }
            catch (MongoCommandException e)
            {
                if (e.Message.Contains("duplicate key"))
                {
                    throw new ConflictException();
                }

                throw;
            }

            if (replacementValue == null)
            {
                var savedModel = await this.Load(model.Id);
                if (savedModel != null && savedModel.Version != preUpdateModelVersion)
                {
                    // document was modified in the database so we didn't write our update
                    model.Version = preUpdateModelVersion;
                    throw new ConcurrencyException();
                }
                else
                {
                    throw new NotFoundException();
                }
            }
        }

        public async Task Delete(TKey id)
        {
            await this.collection.FindOneAndDeleteAsync(Builders<TVal>.Filter.Eq(x => x.Id, id));
        }

        public async Task<List<TVal>> LoadAll()
        {
            return await this.collection.Find(new BsonDocument()).ToListAsync();
        }

        public virtual void PreCreate(TVal model)
        {
        }

        public virtual void PreUpdate(TVal model)
        {
        }
    }
}
