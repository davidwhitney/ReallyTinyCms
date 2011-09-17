using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ReallyTinyCms.Core;

namespace ReallyTinyCms.Tests
{
    [TestFixture]
    public class ConfigurationBuilderTests
    {
        private ContentService _contentService;
        private ConfigurationBuilder _builder;
        private ContentSourceRegistration _contentSourceRegistration;

        [SetUp]
        public void SetUp()
        {
            _contentSourceRegistration = new ContentSourceRegistration(null, null);
            _contentService = new ContentService(_contentSourceRegistration);
            _builder = new ConfigurationBuilder(_contentService);
        }

        [Test]
        public void Ctor_ContentServiceIsNull_ThrowsArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new ConfigurationBuilder(null));

            Assert.That(ex.ParamName, Is.StringMatching("contentService"));
            Assert.That(ex.Message, Is.StringMatching("Configuration builder requires a content service to configure."));
        }

        [Test]
        public void WhenCacheRefreshes_ActionIsNull_DefaultFunctionConfigured()
        {
            _builder.WhenCacheRefreshes(null);

            Assert.That(_contentService.CacheRefreshCallback, Is.Not.Null);
        }

        [Test]
        public void WhenCacheRefreshes_ProvidedWithCallbackAction_CallbackActionConfigured()
        {
            Action callbackAction = () => Debug.WriteLine("Something");

            _builder.WhenCacheRefreshes(callbackAction);

            Assert.That(_contentService.CacheRefreshCallback, Is.EqualTo(callbackAction));
        }
    }
}
