using System;
using System.Web.Routing;
using ReallyTinyCms.Core;
using ReallyTinyCms.Core.ContentPipline;

namespace ReallyTinyCms
{
    public class ConfigurationBuilder
    {
        public ContentController ContentController { get; private set; }

        internal ConfigurationBuilder(ContentController contentController)
        {
            ContentController = contentController;
        }

        public ConfigurationBuilder WhenCacheRefreshes(Action action)
        {
            ContentController.CacheRefreshCallback = action;
            return this;
        } 

        public ConfigurationBuilder WhenContentIsRequested(Action<string, string> action)
        {
            ContentController.ContentForCallback = action;
            return this;
        }   

        public ConfigurationBuilder EditModeShouldBeEnabledWhen(Func<RequestContext, bool> funcWhichVerifiesRequesterIsAllowedToEdit)
        {
            ContentController.ContentRegistration.RequesterIsAllowedToEditContent = funcWhichVerifiesRequesterIsAllowedToEdit;
            return this;
        }    

        public ConfigurationBuilder WithFilters(params IContentPipelineFilter[] filters)
        {
            foreach (var contentPipelineFilter in filters)
            {
                ContentController.ContentRegistration.ContentPipelineFilters.Add(contentPipelineFilter);
            }

            return this;
        }  
    }
}