using System;

namespace ReallyTinyCms
{
    public class ReallyTinyCms
    {
        public static void RegisterContentRepository(Func<ICmsContentRepository> contentRepository)
        {
            HtmlHelperExtensionsForReallyTinyCms.LoadContentRepository = contentRepository;
        }
    }
}
