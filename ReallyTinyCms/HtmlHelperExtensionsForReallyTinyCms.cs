using System.Web.Mvc;

namespace ReallyTinyCms
{
    public static class HtmlHelperExtensionsForReallyTinyCms
    {
        private static ICmsContentRepository _contentRepository;

        public static void RegisterContentRepository(ICmsContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public static ContentRepositoryUsageSugar Cms(this HtmlHelper helper)
        {
            return new ContentRepositoryUsageSugar(_contentRepository);
        }
    }
}
