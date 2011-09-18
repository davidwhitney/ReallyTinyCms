using System;
using ReallyTinyCms.Core.Model;

namespace ReallyTinyCms.Core
{
    public interface IContentService
    {
        ContentSourceRegistration ContentRegistration { get; set; }
        Action<string, string> ContentForCallback { get; set; }
        Action CacheRefreshCallback { set; get; }
        string ContentFor(string contentItemName, Func<string> action = null);
        CmsContentItem SaveContentFor(string contentItemName, string contentValue);
        CmsContentItem RetrieveOrCreate(string contentItemName, string contentValue = "");
    }
}