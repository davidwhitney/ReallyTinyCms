using System.Diagnostics;

namespace ReallyTinyCms.Core.ContentPipline
{
    public class NoOpFilter: ContentPipelineFilterBase
    {
        public override Model.CmsContentItem OnRetrieve(Model.CmsContentItem rawItem)
        {
            Debug.WriteLine("NoOp filter " + FilterId + " OnRetrieve for " + rawItem.Name);
            return base.OnRetrieve(rawItem);
        }

        public override Model.CmsContentItem OnSave(Model.CmsContentItem rawItem)
        {
            Debug.WriteLine("NoOp filter " + FilterId + " OnSave for " + rawItem.Name);
            return base.OnSave(rawItem);
        }
    }
}
