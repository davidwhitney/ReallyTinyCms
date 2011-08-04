using System;

namespace ReallyTinyCms
{
    public class ContentSourceRegistration
    {
        public Func<ICmsContentRepository> FunctionToRetrieveCurrentRepository { get; set; }
        public int? DesiredRefreshIntervalInSeconds { get; set; }
        public bool AutoRefreshContent { get { return DesiredRefreshIntervalInSeconds.HasValue; } }

        public ContentSourceRegistration(Func<ICmsContentRepository> contentRepository)
        {
            FunctionToRetrieveCurrentRepository = contentRepository;
        }
    }
}