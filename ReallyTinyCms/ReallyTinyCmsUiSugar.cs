using System;

namespace ReallyTinyCms
{
    public class ReallyTinyCmsUiSugar
    {
        private readonly ICmsContentRepository _repository;

        public ReallyTinyCmsUiSugar(ContentSourceRegistration contentSourceRegistration)
        {
            _repository = contentSourceRegistration.FunctionToRetrieveCurrentRepository();
        }

        public string ContentFor(string contentItemName)
        {
            var contentItem = _repository.RetrieveOrCreate(contentItemName);
            return contentItem.Content;
        }

        public string ContentFor(string contentItemName, Func<string> action)
        {
            var stringValue = action();
            var contentItem = _repository.RetrieveOrCreate(contentItemName, stringValue ?? string.Empty);
            return contentItem.Content;
        }
    }
}