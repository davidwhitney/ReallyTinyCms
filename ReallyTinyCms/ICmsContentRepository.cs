using System.Collections.Generic;

namespace ReallyTinyCms
{
    public interface ICmsContentRepository
    {
        IList<CmsContentItem> RetrieveAll();
        CmsContentItem Retrieve(string contentItemName);
        CmsContentItem RetrieveOrCreate(string contentItemName, string contentValue = "");
        void SaveOrUpdate(CmsContentItem item);
        void Delete(string contentItemName);
    }
}