using System;
using System.Web;

namespace ReallyTinyCms.Core
{
    public class ReallyTinyCmsUiSugar
    {
        private readonly ContentService _contentService;

        internal ReallyTinyCmsUiSugar(ContentService contentService)
        {
            _contentService = contentService;
        }

        public string ContentFor(string contentItemName)
        {
            return _contentService.ContentFor(contentItemName);
        }

        public string ContentFor(string contentItemName, Func<string> action)
        {
            return _contentService.ContentFor(contentItemName, action);
        }

        public bool EditEnabledForCurrentRequest()
        {
            return _contentService.ContentRegistration.RequesterIsAllowedToEditContent(
                HttpContext.Current.Request.RequestContext);
        }
    }
}