using System;
using System.Diagnostics;
using System.Web.Routing;
using NUnit.Framework;
using ReallyTinyCms.Core;
using ReallyTinyCms.Core.ContentPipline;
using ReallyTinyCms.Tests.Fakes;

namespace ReallyTinyCms.Tests
{
    [TestFixture]
    public class ConfigurationBuilderTests
    {
        private ContentService _contentService;
        private ConfigurationBuilder _builder;
        private ContentSourceRegistration _contentSourceRegistration;
        private CmsContentRepositoryFake _contentRepository;

        [SetUp]
        public void SetUp()
        {
            _contentRepository = new CmsContentRepositoryFake();
            _contentSourceRegistration = new ContentSourceRegistration(() => _contentRepository, null);
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

        [Test]
        public void WithFilters_ProvidedWithNullParamsCollection_AddsNoFilters()
        {
            _builder.WithFilters(null);

            Assert.That(_contentSourceRegistration.ContentPipelineFilters, Is.Empty);
        }

        [Test]
        public void WithFilters_ProvidedWithAFilter_AddsFilterToRegistration()
        {
            var filter = new NoOpFilter();

            _builder.WithFilters(filter);

            Assert.That(_contentSourceRegistration.ContentPipelineFilters, Contains.Item(filter));
        }

        [Test]
        public void WithFilters_ProvidedWithMultipleFilters_AddsFiltersToRegistration()
        {
            var filter1 = new NoOpFilter();
            var filter2 = new NoOpFilter();
            var filter3 = new NoOpFilter();

            _builder.WithFilters(filter1, filter2, filter3);

            Assert.That(_contentSourceRegistration.ContentPipelineFilters, Contains.Item(filter1));
            Assert.That(_contentSourceRegistration.ContentPipelineFilters, Contains.Item(filter2));
            Assert.That(_contentSourceRegistration.ContentPipelineFilters, Contains.Item(filter3));
        }

        [Test]
        public void WithFilters_ProvidedWithMultipleFilters_AddsFiltersInOrder()
        {
            var filter1 = new NoOpFilter();
            var filter2 = new NoOpFilter();
            var filter3 = new NoOpFilter();

            _builder.WithFilters(filter1, filter2, filter3);

            Assert.That(_contentSourceRegistration.ContentPipelineFilters[0], Is.EqualTo(filter1));
            Assert.That(_contentSourceRegistration.ContentPipelineFilters[1], Is.EqualTo(filter2));
            Assert.That(_contentSourceRegistration.ContentPipelineFilters[2], Is.EqualTo(filter3));
        }
    }
}
