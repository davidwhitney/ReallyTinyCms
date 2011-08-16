using System;
using ReallyTinyCms.Core.Storage;

namespace ReallyTinyCms.Core
{
    internal class ContentSourceRegistration
    {
        public Func<ICmsContentRepository> FunctionToRetrieveCurrentRepository { get; set; }
        public int? DesiredRefreshInterval { get; set; }
        public bool AutoRefreshContent { get { return DesiredRefreshInterval.HasValue; } }

        public ContentSourceRegistration(Func<ICmsContentRepository> contentRepository)
        {
            FunctionToRetrieveCurrentRepository = contentRepository;
        }
    }
}