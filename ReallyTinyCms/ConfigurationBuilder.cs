using System;
using System.Web.Routing;
using ReallyTinyCms.Core;
using ReallyTinyCms.Core.ContentPipline;

namespace ReallyTinyCms
{
    public class ConfigurationBuilder
    {
        public ContentService ContentService { get; private set; }

        public ConfigurationBuilder(ContentService contentService)
        {
            if (contentService == null)
            {
                throw new ArgumentNullException("contentService", "Configuration builder requires a content service to configure.");
            }
                
            ContentService = contentService;
        }

        public ConfigurationBuilder WhenCacheRefreshes(Action action)
        {
            if (action == null)
            {
                action = () => { };
            }

            ContentService.CacheRefreshCallback = action;
            return this;
        } 

        public ConfigurationBuilder WhenContentIsRequested(Action<string, string> action)
        {
            ContentService.ContentForCallback = action;
            return this;
        }   

        public ConfigurationBuilder EditModeShouldBeEnabledWhen(Func<RequestContext, bool> funcWhichVerifiesRequesterIsAllowedToEdit)
        {
            ContentService.ContentRegistration.RequesterIsAllowedToEditContent = funcWhichVerifiesRequesterIsAllowedToEdit;
            return this;
        }    

        public ConfigurationBuilder WithFilters(params IContentPipelineFilter[] filters)
        {
            foreach (var contentPipelineFilter in filters)
            {
                ContentService.ContentRegistration.ContentPipelineFilters.Add(contentPipelineFilter);
            }

            return this;
        }  
    }
}