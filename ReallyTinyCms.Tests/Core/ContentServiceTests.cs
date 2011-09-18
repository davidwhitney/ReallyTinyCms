using System;
using System.Collections.Generic;
using NUnit.Framework;
using ReallyTinyCms.Core;
using ReallyTinyCms.Core.Model;
using ReallyTinyCms.Core.Storage;

namespace ReallyTinyCms.Tests.Core
{
    [TestFixture]
    public class ContentServiceTests
    {
        private ContentService _contentService;
        private ContentSourceRegistration _contentRegistration;
        private Func<ICmsContentRepository> _contentRepositoryFunction;

        [SetUp]
        public void SetUp()
        {
            _contentRepositoryFunction = () => new CmsContentRepositoryFake();
            _contentRegistration = new ContentSourceRegistration(_contentRepositoryFunction, null);
            _contentService = new ContentService(_contentRegistration);
        }

        [Test]
        public void Ctor_ContentRegistrationIsNull_ThrowArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new ContentService(null));

            Assert.That(ex.ParamName, Is.StringMatching("contentRegistration"));
            Assert.That(ex.Message, Is.StringMatching("ContentService requires a valid ContentRegistration to function."));
        }

        private class CmsContentRepositoryFake: ICmsContentRepository
        {
            public IList<CmsContentItem> RetrieveAll()
            {
                throw new NotImplementedException();
            }

            public CmsContentItem Retrieve(string contentItemName)
            {
                throw new NotImplementedException();
            }

            public void SaveOrUpdate(CmsContentItem item)
            {
                throw new NotImplementedException();
            }

            public void Delete(string contentItemName)
            {
                throw new NotImplementedException();
            }

            public bool StorageExists()
            {
                throw new NotImplementedException();
            }

            public void CreateStorage()
            {
                throw new NotImplementedException();
            }
        }
    }
}
