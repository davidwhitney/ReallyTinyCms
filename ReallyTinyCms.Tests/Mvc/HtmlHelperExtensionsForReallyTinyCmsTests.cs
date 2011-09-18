using System;
using NUnit.Framework;
using ReallyTinyCms.Core;
using ReallyTinyCms.Mvc;

namespace ReallyTinyCms.Tests.Mvc
{
    [TestFixture]
    public class HtmlHelperExtensionsForReallyTinyCmsTests
    {
        [SetUp]
        public void SetUp()
        {
            HtmlHelperExtensionsForReallyTinyCms.ContentService = new ContentServiceFake();
        }

        [Test]
        public void Cms_ContentServiceIsNull_ThrowsInvalidOperationException()
        {
            HtmlHelperExtensionsForReallyTinyCms.ContentService = null;

            var ex = Assert.Throws<InvalidOperationException>(() => HtmlHelperExtensionsForReallyTinyCms.Cms(null));

            Assert.That(ex.Message, Is.StringMatching("The ContentService is null, cannot continue. Registration failure."));
        }

        [Test]
        public void Cms_ContentServiceIsAlreadyConfigured_ReturnsSomeUiSugar()
        {
            var uiSugar = HtmlHelperExtensionsForReallyTinyCms.Cms(null);

            Assert.That(uiSugar, Is.Not.Null);
        }

        private class ContentServiceFake : IContentService
        {
            public ContentSourceRegistration ContentRegistration { get; set; }
            public Action<string, string> ContentForCallback { get; set; }
            public Action CacheRefreshCallback { get; set; }
            public string ContentFor(string contentItemName)
            {
                throw new NotImplementedException();
            }

            public string ContentFor(string contentItemName, Func<string> action)
            {
                throw new NotImplementedException();
            }
        }
    }
}
