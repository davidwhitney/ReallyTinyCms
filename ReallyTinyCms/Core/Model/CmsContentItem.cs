namespace ReallyTinyCms.Core.Model
{
    public class CmsContentItem
    {
        public string Name { get; set; }
        public string Content { get; set; }
        
        public CmsContentItem()
        {
        }

        public CmsContentItem(string contentItemName)
        {
            Name = contentItemName;
        }
    }
}