using System;

namespace ReallyTinyCms.Core
{
    public class ReallyTinyCmsUiSugar
    {
        private readonly CmsController _cmsController;

        public ReallyTinyCmsUiSugar(CmsController cmsController)
        {
            _cmsController = cmsController;
        }

        public string ContentFor(string contentItemName)
        {
            return _cmsController.ContentFor(contentItemName);
        }

        public string ContentFor(string contentItemName, Func<string> action)
        {
            return _cmsController.ContentFor(contentItemName, action);
        }
    }
}