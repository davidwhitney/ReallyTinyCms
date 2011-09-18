using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

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
                    var requestContext = HttpContext.Current.Request.RequestContext;

                    var routeValues = new RouteValueDictionary
                                          {
                                              {"action", "edit"},
                                              {"controller", "ReallyTinyCms"},
                                              {"name", contentItemName}
                                          };
                    foreach(var item in HttpContext.Current.Request.QueryString.AllKeys)
                    {
                        routeValues.Add(item, HttpContext.Current.Request.QueryString[item]);
                    }

                    var link = HtmlHelper.GenerateRouteLink(requestContext, RouteTable.Routes, "Edit", "ReallyTinyCmsAdmin",
                                                 routeValues, new Dictionary<string, object>{{"target", "_blank"}});

                    buffer.Append("<div id=\"editContentItem_" + contentItemName + "\" class=\"contentItemEditLink\">");
                    buffer.Append(link);
                    buffer.Append("</div>");
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