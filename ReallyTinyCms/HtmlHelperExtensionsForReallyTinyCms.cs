using System;
using System.Web.Mvc;

namespace ReallyTinyCms
{
    public static class HtmlHelperExtensionsForReallyTinyCms
    {
        public static Func<ICmsContentRepository> LoadContentRepository { get; set; }
        
        public static ContentRepositoryUsageSugar Cms(this HtmlHelper helper)
        {
            return new ContentRepositoryUsageSugar(LoadContentRepository());
        }
    }
}
