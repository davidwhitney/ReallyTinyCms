using System.Collections.Generic;

namespace ReallyTinyCms
{
    public interface ICmsContentRepository
    {
        IList<CmsContentItem> RetrieveAll();
        CmsContentItem Retrieve(string contentItemName);
        void SaveOrUpdate(CmsContentItem item);
        void Delete(string contentItemName);
    }
}