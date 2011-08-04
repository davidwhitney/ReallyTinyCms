using System.Web.Mvc;

namespace ReallyTinyCms
{
    public class ReallyTinyCmsUiSugar
    {
        private readonly ICmsContentRepository _repository;

        public ReallyTinyCmsUiSugar(ContentSourceRegistration contentSourceRegistration)
        {
            _repository = contentSourceRegistration.FunctionToRetrieveCurrentRepository();
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