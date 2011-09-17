using System;
using System.Web.Mvc;
using ReallyTinyCms.Core;

namespace ReallyTinyCms.Mvc
{
    public static class HtmlHelperExtensionsForReallyTinyCms
    {
        internal static ContentController ContentController { get; set; }

        public static ReallyTinyCmsUiSugar Cms(this HtmlHelper helper)
        {
            if (ContentController == null)
            {
                throw new InvalidOperationException("The ContentController is null, cannot continue. Registration failure.");
            }

            return new ReallyTinyCmsUiSugar(ContentController);
        }
    }
}
