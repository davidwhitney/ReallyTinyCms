using System.Web.Mvc;

namespace ReallyTinyCms
{
    public class ContentRepositoryUsageSugar
    {
        private readonly ICmsContentRepository _repository;

        public ContentRepositoryUsageSugar(ICmsContentRepository repository)
        {
            _repository = repository;
        }

        public MvcHtmlString ContentFor(string contentItemName)
        {
            var contentItem = RetrieveOrCreateEmpty(contentItemName);
            return MvcHtmlString.Create(contentItem.Content);
        }

        private CmsContentItem RetrieveOrCreateEmpty(string contentItemName)
        {
            var contentItem = _repository.Retrieve(contentItemName);

            if (contentItem == null)
            {
                contentItem = new CmsContentItem(contentItemName);
                _repository.SaveOrUpdate(contentItem);
            }

            return contentItem;
        }
    }
}