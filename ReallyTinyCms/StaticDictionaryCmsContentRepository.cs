using System.Collections.Generic;
using System.Linq;

namespace ReallyTinyCms
{
    public class StaticDictionaryCmsContentRepository : ICmsContentRepository
    {
        private readonly IList<CmsContentItem> _storage;

        public StaticDictionaryCmsContentRepository(IList<CmsContentItem> storage)
        {
            _storage = storage;
        }

        public IList<CmsContentItem> RetrieveAll()
        {
            return _storage;
        }

        public CmsContentItem Retrieve(string contentItemName)
        {
            return _storage.FirstOrDefault(item => item.Name == contentItemName);
        }

        public void SaveOrUpdate(CmsContentItem item)
        {
            var existing = Retrieve(item.Name);

            if(existing == null)
            {
                _storage.Add(item);
                return;
            }

            existing.Content = item.Content;
        }

        public void Delete(string contentItemName)
        {
            var existing = Retrieve(contentItemName);
            if (existing == null) return;

            _storage.Remove(existing);
        }
    }
}
