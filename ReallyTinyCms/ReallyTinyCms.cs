using System;

namespace ReallyTinyCms
{
    public static class ReallyTinyCms
    {
        private static ContentSourceRegistration _contentRegistration;

        public static void RegisterContentRepository(Func<ICmsContentRepository> contentRepository, int? refreshInterval = null)
        {
            _contentRegistration = new ContentSourceRegistration(contentRepository) {DesiredRefreshIntervalInSeconds = refreshInterval};
            HtmlHelperExtensionsForReallyTinyCms.ContentRegistration = _contentRegistration;
        }
    }
}
