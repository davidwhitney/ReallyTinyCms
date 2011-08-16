using System;
using ReallyTinyCms.Core.Model;

namespace ReallyTinyCms.Core.ContentPipline
{
    public abstract class ContentPipelineFilterBase : IContentPipelineFilter
    {
        public Guid FilterId { get; private set; }

        protected ContentPipelineFilterBase()
        {
            FilterId = Guid.NewGuid();
        }

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
