using System;
using NUnit.Framework;
using ReallyTinyCms.Core.Storage;

namespace ReallyTinyCms.Tests
{
    [TestFixture]
    public class ReallyTinyCmsTests
    {
        [Test]
        public void ConfigureWithContentSource_NullContentSourceSupplied_ThrowsArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => ReallyTinyCms.ConfigureWithContentSource(null));

            Assert.That(ex.ParamName, Is.StringMatching("contentRepository"));
            Assert.That(ex.Message, Is.StringMatching("CMS requires a content source to function"));
        }

        [Test]
        public void ConfigureWithContentSource_ContentSourceFunctionSupplied_ReturnsABuilder()
        {
            Func<ICmsContentRepository> contentSource = () => null;

            var builder = ReallyTinyCms.ConfigureWithContentSource(contentSource);

            Assert.That(builder, Is.Not.Null);
        }

        [Test]
        public void ConfigureWithContentSource_ContentSourceFunctionSupplied_ReturnsABuilderWithThatContentSourceWiredUp()
        {
            Func<ICmsContentRepository> contentSource = () => null;
            
            var builder = ReallyTinyCms.ConfigureWithContentSource(contentSource);

            Assert.That(builder.ContentService.ContentRegistration.FunctionToRetrieveCurrentRepository, Is.EqualTo(contentSource));
        }
    }
}