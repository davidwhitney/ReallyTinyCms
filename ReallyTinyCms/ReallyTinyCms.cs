using System;
using ReallyTinyCms.Core;
using ReallyTinyCms.Core.Storage;
using ReallyTinyCms.Mvc;

namespace ReallyTinyCms
{
    /// <summary>
    /// Registration "wizard" for ReallyTinyCms
    /// </summary>
    public static class ReallyTinyCms
    {
        private static ContentController _contentController;

        public static ConfigurationBuilder ConfigureWithContentSource(Func<ICmsContentRepository> contentRepository, int? refreshInterval = null)
        {
            var contentRegistration = new ContentSourceRegistration(contentRepository) {DesiredRefreshIntervalInSeconds = refreshInterval};
            _contentController = new ContentController(contentRegistration);

            HtmlHelperExtensionsForReallyTinyCms.ContentController = _contentController;

            return new ConfigurationBuilder(_contentController);
        }
    }
}
