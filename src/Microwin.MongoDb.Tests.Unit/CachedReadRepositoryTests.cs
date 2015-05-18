using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwin.MongoDb.Tests.Unit
{
    [TestFixture]
    public class CachedReadRepositoryTests
    {
        private Mock<IReadRepository<string, Document<string>>> mockRepo;
        private CachedReadRepository<string, Document<string>> cachedRepo;

        private const string A = "a";
        private const string B = "b";

        [SetUp]
        public void Setup()
        {
            this.mockRepo = new Mock<IReadRepository<string, Document<string>>>();
            this.mockRepo.Setup(x => x.Load(It.IsAny<string>())).Returns<string>(x => Task.FromResult(new Document<string> { Id = x }));
            this.mockRepo.Setup(x => x.LoadAll(It.IsAny<IEnumerable<string>>())).Returns<IEnumerable<string>>(x => Task.FromResult(x.Select(y => new Document<string> { Id = y }).ToList()));
            this.mockRepo.Setup(x => x.LoadAll()).Returns(Task.FromResult(new[] { A, B }.Select(x => new Document<string> { Id = x }).ToList()));
            this.cachedRepo = new CachedReadRepository<string, Document<string>>(this.mockRepo.Object);
        }
        
        [Test]
        public async Task LoadIsOnlyCalledOncePerElement()
        {
            // act
            var a1 = await this.cachedRepo.Load(A);
            var a2 = await this.cachedRepo.Load(A);
            var b1 = await this.cachedRepo.Load(B);

            // assert
            Assert.AreEqual(A, a1.Id);
            Assert.AreEqual(A, a2.Id);
            Assert.AreEqual(B, b1.Id);
            this.mockRepo.Verify(x => x.Load(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public async Task LoadAllByIdIsOnlyCalledOncePerElement()
        {
            // act
            var a1 = await this.cachedRepo.LoadAll(new[] { A });
            var ab = await this.cachedRepo.LoadAll(new[] { A, B });
            var b1 = await this.cachedRepo.LoadAll(new[] { B });

            // assert
            Assert.AreEqual(A, a1.Single().Id);
            Assert.AreEqual(2, ab.Count);
            Assert.AreEqual(B, b1.Single().Id);
            this.mockRepo.Verify(x => x.LoadAll(It.IsAny<IEnumerable<string>>()), Times.Exactly(2));
        }

        [Test]
        public async Task LoadAllIsOnlyCalledOnce()
        {
            // act
            var a = await this.cachedRepo.LoadAll();
            var b = await this.cachedRepo.LoadAll();

            // assert
            Assert.AreEqual(2, a.Count);
            this.mockRepo.Verify(x => x.LoadAll(), Times.Exactly(1));
        }
    }
}
