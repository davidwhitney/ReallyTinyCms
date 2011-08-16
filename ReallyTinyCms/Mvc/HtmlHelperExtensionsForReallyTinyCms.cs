using System.Web.Mvc;
using ReallyTinyCms.Core;

namespace ReallyTinyCms.Mvc
{
    public static class HtmlHelperExtensionsForReallyTinyCms
    {
        internal static ContentController ContentController { get; set; }

        public static ReallyTinyCmsUiSugar Cms(this HtmlHelper helper)
        {
            return new ReallyTinyCmsUiSugar(ContentController);
        }
    }
}
