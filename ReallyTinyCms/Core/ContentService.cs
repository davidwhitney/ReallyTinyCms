using System;
using System.Linq;
using ReallyTinyCms.Core.Caching;
using ReallyTinyCms.Core.Model;
using ReallyTinyCms.Core.Storage;

namespace ReallyTinyCms.Core
{
    public class ContentService : IContentService
    {
    	public ContentSourceRegistration ContentRegistration { get; set; }
    	public Action<string, string> ContentForCallback { get; set; }

		private StaticRepositoryCacheWrapper _contentCache;
		private Func<ICmsContentRepository> _repoProxy;
    	public Action CacheRefreshCallback
    	{
    	    set { _contentCache.CacheRefreshCallback = value; }
            get { return _contentCache.CacheRefreshCallback; }
    	}

        public ContentService(ContentSourceRegistration contentRegistration)
        {
            if (contentRegistration == null)
            {
                throw new ArgumentNullException("contentRegistration", "ContentService requires a valid ContentRegistration to function.");
            }

            ContentRegistration = contentRegistration;
			BuildContentCache(contentRegistration.FunctionToRetrieveCurrentRepository);
            CacheRefreshCallback = () => { };
            ContentForCallback = (contentItemName, defaultValue) => { };
        }

		private void BuildContentCache(Func<ICmsContentRepository> contentRepository)
		{
			_contentCache = new StaticRepositoryCacheWrapper(contentRepository,
			                                                 ContentRegistration.DesiredRefreshInterval.GetValueOrDefault(
			                                                 	5.Minutes()));
			_repoProxy = () => _contentCache;
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
			var repo = _repoProxy();
            var contentItem = repo.Retrieve(contentItemName);

            if (contentItem == null)
            {
                contentItem = new CmsContentItem(contentItemName) { Content = contentValue };
                contentItem = ApplyOnSaveFilters(contentItem);
                repo.SaveOrUpdate(contentItem);
            }

            return ApplyOnRetrieveFilters(contentItem);
        }

        private CmsContentItem ApplyOnRetrieveFilters(CmsContentItem contentItem)
        {
            return ContentRegistration.ContentPipelineFilters.Aggregate(contentItem, (current, filter) => filter.OnRetrieve(current));
        }

        private CmsContentItem ApplyOnSaveFilters(CmsContentItem contentItem)
        {
            contentItem = ContentRegistration.ContentPipelineFilters.Aggregate(contentItem, (current, filter) => filter.OnSave(current));
            return contentItem;
        }
    }
}