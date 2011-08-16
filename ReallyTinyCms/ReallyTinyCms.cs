using System;
using ReallyTinyCms.Core;
using ReallyTinyCms.Core.Storage;
using ReallyTinyCms.Mvc;

namespace ReallyTinyCms
{
    public static class ReallyTinyCms
    {
        private static ContentSourceRegistration _contentRegistration;

        public static void Configure(Func<ICmsContentRepository> contentRepository, int? refreshInterval = null)
        {
            _contentRegistration = new ContentSourceRegistration(contentRepository) {DesiredRefreshIntervalInSeconds = refreshInterval};
            HtmlHelperExtensionsForReallyTinyCms.CmsController = new CmsController(_contentRegistration);
        }
    }
}
