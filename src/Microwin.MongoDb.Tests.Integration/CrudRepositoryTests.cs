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

            coll.Indexes.CreateOneAsync(Builders<Book>.IndexKeys.Ascending(x => x.Isbn), new CreateIndexOptions { Unique = true });

            this.repository = new BookRepository(factory);
        }

        [Test]
        public void DocumentCanBeCreated()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void CreatingDocumentWithSameKeyIsNotAllowed()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void CreatingDocumentThatViolatesUniqueConstraintIsNotAllowed()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void DocumentCanBeReplaced()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void DocumentIsCreatedIfNotReplaced()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void MultipleDocumentsCanBeLoaded()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void AllDocumentsCanBeLoaded()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void DocumentCanBeUpdatedAndVersionIncremented()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void IfUpdateViolatesUniqueConstraintConflictIsReported()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void IfUpdateIdDoesntExistErrorIsReported()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void DocumentCanBeDeleted()
        {
            throw new NotImplementedException();
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
