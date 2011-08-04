using System;
using System.Web.Mvc;

namespace ReallyTinyCms
{
    public static class HtmlHelperExtensionsForReallyTinyCms
    {
        public static ContentSourceRegistration ContentRegistration { get; set; }
        
        public static ReallyTinyCmsUiSugar Cms(this HtmlHelper helper)
        {
            return new ReallyTinyCmsUiSugar(ContentRegistration);
        }
    }
}
