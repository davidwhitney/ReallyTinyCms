using System;

namespace ReallyTinyCms.Core
{
    public class CmsController
    {
        public ContentSourceRegistration ContentRegistration { get; set; }

        public CmsController(ContentSourceRegistration contentRegistration)
        {
            ContentRegistration = contentRegistration;
        }

        public string ContentFor(string contentItemName)
        {
            var repo = ContentRegistration.FunctionToRetrieveCurrentRepository();
            var contentItem = repo.RetrieveOrCreate(contentItemName);
            return contentItem.Content;
        }

        public string ContentFor(string contentItemName, Func<string> action)
        {
            var repo = ContentRegistration.FunctionToRetrieveCurrentRepository();
            var stringValue = action();
            var contentItem = repo.RetrieveOrCreate(contentItemName, stringValue ?? string.Empty);
            return contentItem.Content;
        }
    }
}