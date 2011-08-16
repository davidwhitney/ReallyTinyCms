using ReallyTinyCms.Core.Model;

namespace ReallyTinyCms.Core.ContentPipline
{
    public interface IContentPipelineFilter
    {
        CmsContentItem OnRetrieve(CmsContentItem rawItem);
        CmsContentItem OnSave(CmsContentItem rawItem);
    }
}
