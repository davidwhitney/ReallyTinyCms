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
        public bool ContentForWasCalledWithDefault { get; private set; }
        public string LastContentForValue { get; private set; }

        public string ContentFor(string contentItemName)
        {
            ContentForWasCalled = true;
            LastContentForValue = contentItemName;
            return string.Empty;
        }

        public string ContentFor(string contentItemName, Func<string> action)
        {
            ContentForWasCalledWithDefault = true;
            LastContentForValue = contentItemName;
            return string.Empty;
        }

        public CmsContentItem SaveContentFor(string contentItemName, string contentValue)
        {
            throw new NotImplementedException();
        }
    }
}