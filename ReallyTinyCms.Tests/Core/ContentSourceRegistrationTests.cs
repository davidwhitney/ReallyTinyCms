using System;
using System.Web;
using System.Web.Routing;
using NUnit.Framework;
using ReallyTinyCms.Core;
using ReallyTinyCms.Core.Storage;
using ReallyTinyCms.Tests.Fakes;

namespace ReallyTinyCms.Tests.Core
{
    [TestFixture]
    public class ContentSourceRegistrationTests
    {
        [Test]
        public void Ctor_NullContentRepositoryFunction_ThrowsArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new ContentSourceRegistration(null, 5.Minutes()));

            Assert.That(ex.ParamName, Is.StringMatching("contentRepository"));
            Assert.That(ex.Message, Is.StringMatching("Content source registration needs access to a content repository."));
        }

        [Test]
        public void Ctor_ValidArguments_ConstructsWithEditableFunctionSetToReturnFalse()
        {
            var contentSource = new ContentSourceRegistration(() => new CmsContentRepositoryFake(), null);

            Assert.That(contentSource.RequesterIsAllowedToEditContent(new RequestContext()), Is.False);
        }

        [Test]
        public void Ctor_ConstructedWithValidArgs_ContentRepositoryFunctionSet()
        {
            Func<ICmsContentRepository> repoFunc = () => new CmsContentRepositoryFake();
            var contentSource = new ContentSourceRegistration(repoFunc, null);

            Assert.That(contentSource.FunctionToRetrieveCurrentRepository, Is.EqualTo(repoFunc));
        }

        [Test]
        public void Ctor_ConstructedWithValidArgs_RefreshIntervalSet()
        {
            const int refresh = 5;
            var contentSource = new ContentSourceRegistration(() => new CmsContentRepositoryFake(), refresh);

            Assert.That(contentSource.DesiredRefreshInterval, Is.EqualTo(refresh));
        }

        [Test]
        public void Ctor_ConstructedWithValidArgs_ContentPipelineFiltersAreNotNull()
        {
            var contentSource = new ContentSourceRegistration(() => new CmsContentRepositoryFake(), null);

            Assert.That(contentSource.ContentPipelineFilters, Is.Not.Null);
        }
    }
}
