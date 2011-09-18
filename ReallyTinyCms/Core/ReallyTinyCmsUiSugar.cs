using System;
using System.Text;
using System.Web;

namespace ReallyTinyCms.Core
{
    public class ReallyTinyCmsUiSugar
    {
        private readonly IContentService _contentService;

        public ReallyTinyCmsUiSugar(IContentService contentService)
        {
            if (contentService == null)
            {
                throw new ArgumentNullException("contentService", "UI Sugar is useless without a contentService");
            }

            _contentService = contentService;
        }

        public string ContentFor(string contentItemName, Func<string> action = null, bool useNativeMarkup = false, bool showEditHint = true)
        {
            if (action == null)
            {
                action = () => string.Empty;
            }

            var content = _contentService.ContentFor(contentItemName, action);
            
            if (useNativeMarkup)
            {
                return content;
            }

            return DecorateMarkup(contentItemName, showEditHint, content);
        }

        private string DecorateMarkup(string contentItemName, bool showEditHint, string content)
        {
            var buffer = new StringBuilder("<div id=\"contentItem_" + contentItemName + "\">");
            buffer.Append(content);
            buffer.Append("</div>");

            if (EditEnabledForCurrentRequest())
            {
                if (showEditHint)
                {
                    buffer.Append("<div id=\"editContentItem_" + contentItemName +
                                  "\" class=\"contentItemEditLink\">Edit Content</div>");
                }
            }

            return buffer.ToString();
        }

        public bool EditEnabledForCurrentRequest()
        {
            return _contentService.ContentRegistration.RequesterIsAllowedToEditContent(
                HttpContext.Current.Request.RequestContext);
        }
    }
}