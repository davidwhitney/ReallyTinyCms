using System;
using ReallyTinyCms.Core.Model;

namespace ReallyTinyCms.Core.ContentPipline
{
    public interface IContentPipelineFilter
    {
        Guid FilterId { get; }
        CmsContentItem OnRetrieve(CmsContentItem rawItem);
        CmsContentItem OnSave(CmsContentItem rawItem);
    }
}
