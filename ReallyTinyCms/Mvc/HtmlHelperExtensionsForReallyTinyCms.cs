using System.Web.Mvc;
using ReallyTinyCms.Core;

namespace ReallyTinyCms.Mvc
{
    public static class HtmlHelperExtensionsForReallyTinyCms
    {
        public static CmsController CmsController { get; set; }

        public static ReallyTinyCmsUiSugar Cms(this HtmlHelper helper)
        {
            return new ReallyTinyCmsUiSugar(CmsController);
        }
    }
}
