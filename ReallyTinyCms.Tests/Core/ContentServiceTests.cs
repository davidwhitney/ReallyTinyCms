using System;
using System.Collections.Generic;
using System.Linq;
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
        private CmsContentRepositoryFake _contentRepository;
        
        const string ItemName = "item";
        const string ItemValue = "value";

        [SetUp]
        public void SetUp()
        {
            _contentRepository = new CmsContentRepositoryFake();
            _contentRepositoryFunction = () => _contentRepository;
            _contentRegistration = new ContentSourceRegistration(_contentRepositoryFunction, null);
            _contentService = new ContentService(_contentRegistration);
        }

        [Test]
        public void Ctor_ContentRegistrationIsNotNull_ConstructsService()
        {
            Assert.That(_contentService, Is.Not.Null);
        }

        [Test]
        public void Ctor_ContentRegistrationIsNull_ThrowArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new ContentService(null));

            Assert.That(ex.ParamName, Is.StringMatching("contentRegistration"));
            Assert.That(ex.Message, Is.StringMatching("ContentService requires a valid ContentRegistration to function."));
        }

        [Test]
        public void Ctor_WhenConstructed_DefaultCacheRefreshCallbackIsNotNull()
        {
            Assert.That(_contentService.CacheRefreshCallback, Is.Not.Null);
        }

        [Test]
        public void Ctor_WhenConstructed_DefaultContentForCallbackIsNotNull()
        {
            Assert.That(_contentService.ContentForCallback, Is.Not.Null);
        }

        [Test]
        public void Ctor_WhenConstructedWithoutSpecificRefreshInterval_ContentRegistrationDefaultRefreshRateSetTo5Minutes()
        {
            Assert.That(_contentRegistration.DesiredRefreshInterval, Is.EqualTo(5.Minutes()));
        }

        [Test]
        public void ContentFor_WhenCalled_CallsCallbackWithContentInformation()
        {
            const string contentAreaName = "something";
            var callbackCalled = false;
            _contentService.ContentForCallback = (name, value) => { callbackCalled = true; };

            _contentService.ContentFor(contentAreaName);

            Assert.That(callbackCalled, Is.True);
        }

        [Test]
        public void ContentFor_WhenContentItemExists_RetrievesItem()
        {
            var contentItem = new CmsContentItem(ItemName) {Content = ItemValue};
            _contentRepository.Add(ItemName, contentItem);

            var item = _contentService.ContentFor(ItemName);

            Assert.That(item, Is.EqualTo(ItemValue));
        }

        [Test]
        public void ContentFor_WhenContentItemExists_RetrievesFromInternalCache()
        {
            var contentItem = new CmsContentItem(ItemName) {Content = ItemValue};
            _contentRepository.Add(ItemName, contentItem);

            _contentService.ContentFor(ItemName);

            Assert.That(_contentRepository.RetrieveAllCalled, Is.True);
            Assert.That(_contentRepository.RetrieveCalled, Is.False);
        }

        [Test]
        public void ContentFor_WhenContentItemDoesntExist_CallsSaveOrUpdateWithNewItem()
        {
            const string itemName = "item";

            _contentService.ContentFor(itemName);

            Assert.That(_contentRepository.SaveOrUpdateCalledAndNewItemCreated, Is.True);
        }

        [Test]
        public void ContentFor_WhenContentItemDoesntExist_DefaultItemReturned()
        {
            var item = _contentService.ContentFor(ItemName);

            Assert.That(item, Is.Not.Null);
        }
        
        private class CmsContentRepositoryFake: Dictionary<string, CmsContentItem>, ICmsContentRepository
        {
            protected internal bool RetrieveAllCalled { get; private set; }
            protected internal bool RetrieveCalled { get; private set; }
            protected internal bool SaveOrUpdateCalledAndNewItemCreated { get; private set; }

            public IList<CmsContentItem> RetrieveAll()
            {
                RetrieveAllCalled = true;
                return Values.ToList();
            }

            public CmsContentItem Retrieve(string contentItemName)
            {
                RetrieveCalled = true;
                return ContainsKey(contentItemName) ? this[contentItemName] : null;
            }

            public void SaveOrUpdate(CmsContentItem item)
            {
                var cItem = Retrieve(item.Name);

                if(cItem != null)
                {
                    cItem.Content = item.Content;
                }
                else
                {
                    SaveOrUpdateCalledAndNewItemCreated = true;
                    Add(item.Name, item);
                }
            }

            public void Delete(string contentItemName)
            {
                if(Retrieve(contentItemName) != null)
                {
                    Remove(contentItemName);
                }
            }

            public bool StorageExists()
            {
                return true;
            }

            public void CreateStorage()
            {
            }
        }
    }
}
