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

		private StaticRepositoryCacheWrapper _contentCache;
		private Func<ICmsContentRepository> _repoProxy;

        public Action<string, string> ContentForCallback { get; set; }
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
		    ContentRegistration.DesiredRefreshInterval =
		        ContentRegistration.DesiredRefreshInterval.GetValueOrDefault(5.Minutes());

            _contentCache = new StaticRepositoryCacheWrapper(contentRepository, ContentRegistration.DesiredRefreshInterval.Value);
			_repoProxy = () => _contentCache;
		}

        public string ContentFor(string contentItemName)
        {
            return ContentFor(contentItemName, () => string.Empty);
        }

        public string ContentFor(string contentItemName, Func<string> action)
        {
            if (string.IsNullOrWhiteSpace(contentItemName))
            {
                return action != null ? action() : string.Empty;
            }

            var localisedContentItemName = LocaliseContentItemName(contentItemName);

            var stringValue = action();
            var contentItem = RetrieveOrCreate(localisedContentItemName, stringValue ?? string.Empty);
            ContentForCallback(contentItemName, stringValue);
            return contentItem.Content;
        }

        public CmsContentItem SaveContentFor(string contentItemName, string contentValue)
        {
            var localisedContentItemName = LocaliseContentItemName(contentItemName);

            var contentItem = new CmsContentItem(localisedContentItemName) { Content = contentValue };
            contentItem = ApplyOnSaveFilters(contentItem);

            var repo = _repoProxy();
            repo.SaveOrUpdate(contentItem);

            return contentItem;
        }

        public CmsContentItem RetrieveOrCreate(string contentItemName, string contentValue = "")
        {
            if (string.IsNullOrWhiteSpace(contentItemName))
            {
                throw new ArgumentNullException("contentItemName");
            }

            var repo = _repoProxy();
            var contentItem = repo.Retrieve(contentItemName) ?? SaveContentFor(contentItemName, contentValue);
            return ApplyOnRetrieveFilters(contentItem);
        }

        private string LocaliseContentItemName(string contentItemName)
        {
            return contentItemName;
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