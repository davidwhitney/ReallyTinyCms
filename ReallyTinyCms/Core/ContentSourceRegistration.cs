using System;
using System.Collections.Generic;
using System.Web.Routing;
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
		
        public ContentSourceRegistration(Func<ICmsContentRepository> contentRepository, int? refreshInterval)
        {
            if (contentRepository == null)
            {
                throw new ArgumentNullException("contentRepository", "Content source registration needs access to a content repository.");
            }

        	DesiredRefreshInterval = refreshInterval;
			FunctionToRetrieveCurrentRepository = contentRepository;
            RequesterIsAllowedToEditContent = x => false;
            ContentPipelineFilters = new List<IContentPipelineFilter>();
        }
    }
}