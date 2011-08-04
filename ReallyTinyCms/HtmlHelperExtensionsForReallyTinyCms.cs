using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ReallyTinyCms
{
    public static class HtmlHelperExtensionsForReallyTinyCms
    {
        public static CmsContentRepository Cms(this HtmlHelper helper)
        {
            return new CmsContentRepository();
        } 
    }
}
