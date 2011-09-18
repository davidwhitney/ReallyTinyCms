using System;

namespace ReallyTinyCms.Core.Model
{
    public class CmsContentItem
    {
        private string _name;
        private string _content;
        
        public CmsContentItem()
        {
        }

        public CmsContentItem(string contentItemName)
        {
            if (string.IsNullOrWhiteSpace(contentItemName))
            {
                throw new ArgumentNullException("contentItemName");
            }

            Name = contentItemName;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("value");
                }

                _name = value;
            }
        }

        public string Content
        {
            get { return _content; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                _content = value;
            }
        }
    }
}