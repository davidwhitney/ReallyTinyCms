using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Routing;
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
        public void WhenCacheRefreshes_ActionIsNull_DefaultActionConfigured()
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

        [Test]
        public void WhenContentIsRequested_ActionIsNull_DefaultActionConfigured()
        {
            _builder.WhenContentIsRequested(null);

            Assert.That(_contentService.ContentForCallback, Is.Not.Null);
        }

        [Test]
        public void WhenContentIsRequested_ProvidedWithCallbackAction_CallbackActionConfigured()
        {
            Action<string,string> callbackAction = (x, y) => Debug.WriteLine("Something");

            _builder.WhenContentIsRequested(callbackAction);

            Assert.That(_contentService.ContentForCallback, Is.EqualTo(callbackAction));
        }

        [Test]
        public void EditModeShouldBeEnabledWhen_FunctionIsNull_DefaultActionConfigured()
        {
            _builder.EditModeShouldBeEnabledWhen(null);

            Assert.That(_contentSourceRegistration.RequesterIsAllowedToEditContent, Is.Not.Null);
        }

        [Test]
        public void EditModeShouldBeEnabledWhen_ProvidedWithFunctionToUseInEvaluation_EvaluationFunctionRegistered()
        {
            Func<RequestContext, bool> evaluationFunction = x => true;

            _builder.EditModeShouldBeEnabledWhen(evaluationFunction);

            Assert.That(_contentSourceRegistration.RequesterIsAllowedToEditContent, Is.Not.Null);
        }
    }
}
