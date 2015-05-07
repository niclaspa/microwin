using iNeed.MongoDb;
using iNeed.MongoDb.Models;
using iNeed.MongoDb.Repositories;
using MongoDB.Driver;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microwin.Exceptions;
using MongoDB.Bson;

namespace Microwin.MongoDb.Tests.Integration
{
    [TestFixture]
    public class CrudRepositoryTests
    {
        private BookRepository repository;

        private const string DatabaseName = "CrudRepositoryTests";
        private const string CollectionName = "books";

        [SetUp]
        public void Setup()
        {
            var factory = new MongoClientFactory();

            var server = factory.CreateMongoClient();
            var db = server.GetDatabase(DatabaseName);
            var coll = db.GetCollection<Book>(CollectionName);
            coll.DeleteManyAsync(new BsonDocument());

            coll.Indexes.CreateOneAsync(Builders<Book>.IndexKeys.Ascending(x => x.Isbn), new CreateIndexOptions { Unique = true });

            this.repository = new BookRepository(factory);
        }

        [Test]
        public async Task DocumentCanBeCreated()
        {
            // arrange
            var model = new Book { Isbn = "isbn" };

            // act
            var id = await this.repository.Create(model);
            var loadedModel = await this.repository.Load(id);

            // assert
            Assert.AreEqual(model.Id, id);
            Assert.AreNotEqual(model.Id, default(Guid));
            Assert.AreEqual(model.Isbn, loadedModel.Isbn);
        }

        [Test]
        public async Task CreatingDocumentWithSameKeyIsNotAllowed()
        {
            // arrange
            var model = new Book { Isbn = "isbn" };
            var id = await this.repository.Create(model);

            // act
            // assert
            Assert.Catch(typeof(ConflictException), async () => await this.repository.Create(new Book { Id = id, Isbn = "isbn2" }));
        }

        [Test]
        public async Task CreatingDocumentThatViolatesUniqueConstraintIsNotAllowed()
        {
            // arrange
            var model = new Book { Isbn = "isbn" };
            var id = await this.repository.Create(model);

            // act
            // assert
            Assert.Catch(typeof(ConflictException), async () => await this.repository.Create(new Book { Isbn = "isbn" }));
        }

        [Test]
        public async Task DocumentCanBeReplaced()
        {
            // arrange
            var model = new Book { Isbn = "isbn" };
            var id = await this.repository.Create(model);

            // act
            model.Isbn = "new";
            await this.repository.CreateOrReplace(model);

            // assert
            var loadedModel = await this.repository.Load(id);
            Assert.AreEqual("new", loadedModel.Isbn);
        }

        [Test]
        public async Task DocumentIsCreatedIfNotReplaced()
        {
            // arrange
            var model = new Book { Isbn = "isbn" };

            // act
            await this.repository.CreateOrReplace(model);

            // assert
            var loadedModel = await this.repository.Load(model.Id);
            Assert.IsNotNull(loadedModel);
        }

        [Test]
        public async Task MultipleDocumentsCanBeLoaded()
        {
            // arrange
            var model = new Book { Isbn = "isbn" };
            var model2 = new Book { Isbn = "isbn2" };
            var id = await this.repository.Create(model);
            var id2 = await this.repository.Create(model2);

            // act
            var res = await this.repository.LoadAll(new[] { model.Id, model2.Id });

            // assert
            Assert.AreEqual(2, res.Count);
        }

        [Test]
        public async Task AllDocumentsCanBeLoaded()
        {
            // arrange
            var model = new Book { Isbn = "isbn" };
            var model2 = new Book { Isbn = "isbn2" };
            var id = await this.repository.Create(model);
            var id2 = await this.repository.Create(model2);

            // act
            var res = await this.repository.LoadAll();

            // assert
            Assert.AreEqual(2, res.Count);
        }

        [Test]
        public async Task DocumentCanBeUpdatedAndVersionIncremented()
        {
            // arrange
            var model = new Book { Isbn = "isbn" };
            var id = await this.repository.Create(model);

            // act
            model.Isbn = "new";
            await this.repository.Update(model);

            // assert
            var loadedModel = await this.repository.Load(model.Id);
            Assert.AreEqual(2, loadedModel.Version);
            Assert.AreEqual("new", loadedModel.Isbn);
        }

        [Test]
        public async Task IfUpdateViolatesUniqueConstraintConflictIsReported()
        {
            // arrange
            var model = new Book { Isbn = "isbn" };
            var model2 = new Book { Isbn = "isbn2" };
            var id = await this.repository.Create(model);
            var id2 = await this.repository.Create(model2);

            // act
            // assert
            model.Isbn = model2.Isbn;
            Assert.Catch(typeof(ConflictException), async () => await this.repository.Update(model));
        }

        [Test]
        public void IfUpdateIdDoesntExistErrorIsReported()
        {
            Assert.Catch(typeof(NotFoundException), async () => await this.repository.Update(new Book { Id = Guid.NewGuid(), Version = 1 }));
        }

        [Test]
        public async Task DocumentCanBeDeleted()
        {
            // arrange
            var model = new Book { Isbn = "isbn" };
            var id = await this.repository.Create(model);

            // act
            await this.repository.Delete(id);

            // assert
            Assert.IsNull(await this.repository.Load(id));
        }

        private class Book : IDocument<Guid>
        {
            public Guid Id { get; set; }

            public int Version { get; set; }

            public string Isbn { get; set; } // unique constraint
        }

        private class BookRepository : CrudRepository<Guid, Book>
        {
            public BookRepository(IMongoClientFactory clientFactory)
                : base(clientFactory, DatabaseName, CollectionName)
            {
            }
        }
    }
}
