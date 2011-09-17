using System;
using System.Web.Mvc;
using ReallyTinyCms.Core;

namespace ReallyTinyCms.Mvc
{
    public static class HtmlHelperExtensionsForReallyTinyCms
    {
        internal static ContentService ContentService { get; set; }

        public static ReallyTinyCmsUiSugar Cms(this HtmlHelper helper)
        {
            if (ContentService == null)
            {
                throw new InvalidOperationException("The ContentService is null, cannot continue. Registration failure.");
            }

            return new ReallyTinyCmsUiSugar(ContentService);
        }
    }
}
