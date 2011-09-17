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
        private static ContentService _contentService;

        public static ConfigurationBuilder ConfigureWithContentSource(Func<ICmsContentRepository> contentRepository, int? refreshInterval = null)
        {
            var contentRegistration = new ContentSourceRegistration(contentRepository, refreshInterval);
            _contentService = new ContentService(contentRegistration);

            HtmlHelperExtensionsForReallyTinyCms.ContentService = _contentService;

            return new ConfigurationBuilder(_contentService);
        }
    }
}
