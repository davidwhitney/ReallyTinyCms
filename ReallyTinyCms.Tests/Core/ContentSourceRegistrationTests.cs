using System;
using NUnit.Framework;
using ReallyTinyCms.Core;

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
    }
}
