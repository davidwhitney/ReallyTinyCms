using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using ReallyTinyCms.Core;

namespace ReallyTinyCms.Mvc
{
    public static class HtmlHelperExtensionsForReallyTinyCms
    {
        public static IContentService ContentService { get; set; }
        public static readonly string EditContentHtmlFieldNamePrefix = "editContentItem_";
        public static readonly string DisplayContentHtmlFieldNamePrefix = "displayContentItem_";
    
        public static readonly string ReturnUrlKey = "returnUrl";
        public static readonly string EditModeKey = "editMode";

        public static MvcHtmlString ContentFor(this HtmlHelper helper, string contentItemName, Func<string> action = null)
        {
            Validate();

            if (action == null)
            {
                action = () => string.Empty;
            }

            var currentModel = helper.ViewData.Model; //Support implementing in existing applications
            helper.ViewData.Model = ContentService.ContentFor(contentItemName, action);

            var content = EditEnabledForCurrentRequest(helper) ? EditorContentTemplate(helper, contentItemName) : helper.DisplayForModel(contentItemName, DisplayContentHtmlFieldNamePrefix + contentItemName); ;
            helper.ViewData.Model = currentModel;

            return content;
        }

        private static MvcHtmlString EditorContentTemplate(HtmlHelper helper, string contentItemName)
        {
            var form = new TagBuilder("form");
            form.MergeAttribute("action", GenerateEditUrl(helper, contentItemName));
            form.MergeAttribute("method", HtmlHelper.GetFormMethodString(FormMethod.Post), true);
            form.GenerateId("form_" + EditContentHtmlFieldNamePrefix + contentItemName);
            form.InnerHtml = helper.EditorForModel(contentItemName, EditContentHtmlFieldNamePrefix + contentItemName).ToHtmlString();
            
            var submitButton = new TagBuilder("input");
            submitButton.MergeAttribute("type", "submit");
            submitButton.MergeAttribute("value", "Save");
            submitButton.GenerateId("button_" + EditContentHtmlFieldNamePrefix + contentItemName);
            form.InnerHtml += submitButton.ToString(); 

            return MvcHtmlString.Create(form.ToString());
        }

        public static string GenerateEditUrl(this HtmlHelper helper, string contentItemName)
        {
            var httpContext = helper.ViewContext.HttpContext;
            var requestContext = httpContext.Request.RequestContext;

            var routeValues = new RouteValueDictionary
                                  {
                                      {"action", "edit"},
                                      {"controller", "ReallyTinyCms"},
                                      {"name", contentItemName}
                                  };

            httpContext.Request.QueryString.AllKeys
                .ToList()
                .ForEach(key => routeValues.Add(key, httpContext.Request.QueryString[key]));

            return UrlHelper.GenerateUrl("ReallyTinyCmsAdmin", "Edit", "ReallyTinyCms", routeValues, RouteTable.Routes, requestContext, false);
        }

        public static bool EditEnabledForCurrentRequest(this HtmlHelper helper)
        {
            return EditEnabledForCurrentRequest(helper.ViewContext.HttpContext.Request.RequestContext);
        }

        public static bool EditEnabledForCurrentRequest(RequestContext requestContext)
        {
            return ContentService.ContentRegistration.RequesterIsAllowedToEditContent(requestContext);
        }

        private static void Validate()
        {
            if (ContentService == null)
            {
                throw new InvalidOperationException("The ContentService is null, cannot continue. Registration failure.");
            }
        }

    }
}
