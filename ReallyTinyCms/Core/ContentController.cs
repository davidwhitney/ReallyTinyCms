using System;
using ReallyTinyCms.Core.Model;

namespace ReallyTinyCms.Core
{
    internal class ContentController
    {
        public ContentSourceRegistration ContentRegistration { get; set; }
        
        public Action CacheRefreshCallback { get; set; }
        public Action<string, string> ContentForCallback { get; set; }
        
        internal ContentController(ContentSourceRegistration contentRegistration)
        {
            ContentRegistration = contentRegistration;
            
            CacheRefreshCallback = () => { };
            ContentForCallback = (contentItemName, defaultValue) => { };
        }

        public string ContentFor(string contentItemName)
        {
            var contentItem = RetrieveOrCreate(contentItemName);
            ContentForCallback(contentItemName, null);
            return contentItem.Content;
        }

        public string ContentFor(string contentItemName, Func<string> action)
        {
            var stringValue = action();
            var contentItem = RetrieveOrCreate(contentItemName, stringValue ?? string.Empty);
            ContentForCallback(contentItemName, stringValue);
            return contentItem.Content;
        }

        private CmsContentItem RetrieveOrCreate(string contentItemName, string contentValue = "")
        {
            var repo = ContentRegistration.FunctionToRetrieveCurrentRepository();
            var contentItem = repo.Retrieve(contentItemName);

            if (contentItem == null)
            {
                contentItem = new CmsContentItem(contentItemName) { Content = contentValue };
                repo.SaveOrUpdate(contentItem);
            }

            return contentItem;
        }
    }
}