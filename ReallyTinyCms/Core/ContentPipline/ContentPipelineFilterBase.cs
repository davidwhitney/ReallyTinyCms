using ReallyTinyCms.Core.Model;

namespace ReallyTinyCms.Core.ContentPipline
{
    public abstract class ContentPipelineFilterBase : IContentPipelineFilter
    {
        public virtual CmsContentItem OnRetrieve(CmsContentItem rawItem)
        {
            return rawItem;
        }

        public virtual CmsContentItem OnSave(CmsContentItem rawItem)
        {
            return rawItem;
        }
    }
}
