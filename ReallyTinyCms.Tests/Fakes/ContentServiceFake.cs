using System;
using ReallyTinyCms.Core;
using ReallyTinyCms.Core.Model;

namespace ReallyTinyCms.Tests.Fakes
{
    public class ContentServiceFake: IContentService
    {
        public ContentSourceRegistration ContentRegistration { get; set; }
        public Action<string, string> ContentForCallback { get; set; }
        public Action CacheRefreshCallback { get; set; }

        public bool ContentForWasCalled { get; private set; }
        public string LastContentForValue { get; private set; }
        
        public string ContentFor(string contentItemName, Func<string> action = null)
        {
            if (action == null)
            {
                action = () => string.Empty;
            }

            ContentForWasCalled = true;
            LastContentForValue = contentItemName;
            return action();
        }

        public CmsContentItem SaveContentFor(string contentItemName, string contentValue)
        {
            throw new NotImplementedException();
        }

        public CmsContentItem RetrieveOrCreate(string contentItemName, string contentValue = "")
        {
            throw new NotImplementedException();
        }
    }
}