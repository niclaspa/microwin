using Microwin.Config;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwin.Tests.Unit.Config
{
    [TestFixture]
    public class AppSettingsReaderTests
    {
        private AppSettingsReader settings;
        private NameValueCollection configValues;

        [SetUp]
        public void Setup()
        {
            this.configValues = new NameValueCollection();
            this.configValues.Add("string", "val1");
            this.configValues.Add("stringarray", " val1, ,val2 ");
            this.configValues.Add("bool", "true");
            this.configValues.Add("invalid_bool", "123");
            this.configValues.Add("null", null);

            var mockProvider = new Mock<IAppSettingsProvider>();
            mockProvider.SetupGet(x => x.AppSettings).Returns(this.configValues);

            this.settings = new AppSettingsReader(mockProvider.Object);
        }

        [Test]
        public void StringCanBeRead()
        {
            Assert.AreEqual("val1", this.settings.ReadString("string", true));
        }

        [Test]
        public void StringCanNotBeReadIfEmptyAndEmptyIsNotAllowed()
        {
            Assert.Catch<KeyNotFoundException>(() => this.settings.ReadString("none", true));
        }

        [Test]
        public void StringCanBeReadIfEmptyAndEmptyIsAllowed()
        {
            Assert.AreEqual(string.Empty, this.settings.ReadString("none", false));
        }

        [Test]
        public void NullStringIsHandled()
        {
            Assert.AreEqual(string.Empty, this.settings.ReadString("null", false));
        }

        [Test]
        public void StringArrayCanNotBeReadIfEmptyAndEmptyIsNotAllowed()
        {
            Assert.Catch<KeyNotFoundException>(() => { this.settings.ReadStringArray("none", true); });
        }

        [Test]
        public void StringArrayCanBeReadIfEmptyAndEmptyIsAllowed()
        {
            var val = this.settings.ReadStringArray("none", false);
            Assert.AreEqual(0, val.Length);
        }

        [Test]
        public void StringArrayCanTrimValues()
        {
            var val = this.settings.ReadStringArray("stringarray", false, true, true);
            Assert.AreEqual(3, val.Length);
            Assert.AreEqual("val1", val[0]);
            Assert.AreEqual(string.Empty, val[1]);
            Assert.AreEqual("val2", val[2]);
        }

        [Test]
        public void StringArrayCanFilterOutEmptyValues()
        {
            var val = this.settings.ReadStringArray("stringarray", false, false, false);
            Assert.AreEqual(2, val.Length);
            Assert.AreEqual(" val1", val[0]);
            Assert.AreEqual("val2 ", val[1]);
        }

        [Test]
        public void BoolCanBeRead()
        {
            Assert.IsTrue(this.settings.ReadBool("bool"));
        }

        [Test]
        public void BoolDefaultsToFalseForInvalidFormatIfNotThrowingOnError()
        {
            Assert.IsFalse(this.settings.ReadBool("invalid_bool", false));
        }

        [Test]
        public void BoolDefaultsToFalseForEmptyIfNotThrowingOnError()
        {
            Assert.IsFalse(this.settings.ReadBool("none", false));
        }

        [Test]
        public void BoolReturnsErrorIfEmptyAndThrowingOnError()
        {
            Assert.Catch<KeyNotFoundException>(() => this.settings.ReadBool("none", true));
        }

        [Test]
        public void BoolReturnsErrorIfInvalidFormatAndThrowingOnError()
        {
            Assert.Catch<FormatException>(() => this.settings.ReadBool("invalid_bool", true));
        }
    }
}
