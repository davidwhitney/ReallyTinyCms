using System;
using System.Collections.Generic;
using System.Web.Routing;
using ReallyTinyCms.Core.Caching;
using ReallyTinyCms.Core.ContentPipline;
using ReallyTinyCms.Core.Storage;

namespace ReallyTinyCms.Core
{
    public class ContentSourceRegistration
    {
		public Func<ICmsContentRepository> FunctionToRetrieveCurrentRepository { get; set; }
        public int? DesiredRefreshInterval { get; set; }
        public bool AutoRefreshContent { get { return DesiredRefreshInterval.HasValue; } }
        public Func<RequestContext, bool> RequesterIsAllowedToEditContent { get; set; }
        public IList<IContentPipelineFilter> ContentPipelineFilters { get; set; }

		private StaticRepositoryCacheWrapper _contentCache;

        public ContentSourceRegistration(Func<ICmsContentRepository> contentRepository)
        {
			AdornRepositoryWithCache(contentRepository);
            RequesterIsAllowedToEditContent = x => false;
            ContentPipelineFilters = new List<IContentPipelineFilter>();
        }

    	private void AdornRepositoryWithCache(Func<ICmsContentRepository> contentRepository)
    	{
    		_contentCache = new StaticRepositoryCacheWrapper(contentRepository, DesiredRefreshInterval.GetValueOrDefault(5.Minutes()));
    		FunctionToRetrieveCurrentRepository = () => _contentCache;
    	}
    }
}