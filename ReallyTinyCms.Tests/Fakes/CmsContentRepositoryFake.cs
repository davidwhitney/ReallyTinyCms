using System.Collections.Generic;
using System.Linq;
using ReallyTinyCms.Core.Model;
using ReallyTinyCms.Core.Storage;

namespace ReallyTinyCms.Tests.Fakes
{
    public class CmsContentRepositoryFake : Dictionary<string, CmsContentItem>, ICmsContentRepository
    {
        protected internal bool RetrieveAllCalled { get; private set; }
        protected internal bool RetrieveCalled { get; private set; }
        protected internal bool SaveOrUpdateCalledAndNewItemCreated { get; private set; }
        protected internal CmsContentItem LastSavedItem { get; private set; }

        public IList<CmsContentItem> RetrieveAll()
        {
            RetrieveAllCalled = true;
            return Values.ToList();
        }

        public CmsContentItem Retrieve(string contentItemName)
        {
            RetrieveCalled = true;
            return ContainsKey(contentItemName) ? this[contentItemName] : null;
        }

        public void SaveOrUpdate(CmsContentItem item)
        {
            var cItem = Retrieve(item.Name);

            if (cItem != null)
            {
                cItem.Content = item.Content;
            }
            else
            {
                SaveOrUpdateCalledAndNewItemCreated = true;
                Add(item.Name, item);
            }

            LastSavedItem = item;
        }

        public void Delete(string contentItemName)
        {
            if (Retrieve(contentItemName) != null)
            {
                Remove(contentItemName);
            }
        }

        public bool StorageExists()
        {
            return true;
        }

        public void CreateStorage()
        {
        }
    }
}