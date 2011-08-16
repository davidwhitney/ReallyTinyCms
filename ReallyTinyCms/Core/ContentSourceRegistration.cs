using System;
using System.Web.Routing;
using ReallyTinyCms.Core.Storage;

namespace ReallyTinyCms.Core
{
    public class ContentSourceRegistration
    {
        public Func<ICmsContentRepository> FunctionToRetrieveCurrentRepository { get; set; }
        public int? DesiredRefreshInterval { get; set; }
        public bool AutoRefreshContent { get { return DesiredRefreshInterval.HasValue; } }
        public Func<RequestContext, bool> RequesterIsAllowedToEditContent { get; set; }

        public ContentSourceRegistration(Func<ICmsContentRepository> contentRepository)
        {
            FunctionToRetrieveCurrentRepository = contentRepository;
            RequesterIsAllowedToEditContent = x => false;
        }
    }
}