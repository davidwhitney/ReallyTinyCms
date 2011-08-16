using System;

namespace ReallyTinyCms.Core
{
    public class ReallyTinyCmsUiSugar
    {
        private readonly ContentController _contentController;

        internal ReallyTinyCmsUiSugar(ContentController contentController)
        {
            _contentController = contentController;
        }

        public string ContentFor(string contentItemName)
        {
            return _contentController.ContentFor(contentItemName);
        }

        public string ContentFor(string contentItemName, Func<string> action)
        {
            return _contentController.ContentFor(contentItemName, action);
        }
    }
}