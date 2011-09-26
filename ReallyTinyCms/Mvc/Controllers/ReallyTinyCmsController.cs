using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using ReallyTinyCms.Core;

namespace ReallyTinyCms.Mvc.Controllers
{
    public class ReallyTinyCmsController : Controller
    {
        private readonly IContentService _contentService;
        
        public ReallyTinyCmsController() : this(ReallyTinyCms.ContentService)
        {
        }

        public ReallyTinyCmsController(IContentService contentService)
        {
            _contentService = contentService;
        }

        public ActionResult Index(bool failedAuth = false, bool invalidName = false)
        {
            return View("~/bin/Views/ReallyTinyCms/Index.aspx");
        }

        [HttpGet]
        public ActionResult Edit(string name)
        {
            if (!HtmlHelperExtensionsForReallyTinyCms.EditEnabledForCurrentRequest(Request.RequestContext))
            {
                return RedirectToAction("Index", new {failedAuth = true});
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                return RedirectToAction("Index", new {invalidName = true});
            }

            var item = _contentService.RetrieveOrCreate(name);

            return View("~/bin/Views/ReallyTinyCms/Edit.aspx", item);
        }

        [HttpPost]
        public ActionResult Edit(string name, string content)
        {
            if (!HtmlHelperExtensionsForReallyTinyCms.EditEnabledForCurrentRequest(Request.RequestContext))
            {
                return RedirectToAction("Index", new {failedAuth = true});
            }

            if (Request.Form.AllKeys.Where(v => v.StartsWith(HtmlHelperExtensionsForReallyTinyCms.EditContentHtmlFieldNamePrefix)).Count() == 0)
            {
                return RedirectToAction("Index", new { invalidName = true });
            }

            Request.Form.AllKeys //TODO create model binder =>  CmsContentItem
                .Where(v => v.StartsWith(HtmlHelperExtensionsForReallyTinyCms.EditContentHtmlFieldNamePrefix))
                .ToList()
                .ForEach(key => _contentService.SaveContentFor(key.Replace(HtmlHelperExtensionsForReallyTinyCms.EditContentHtmlFieldNamePrefix, string.Empty), Request.Form[key]));
            
            if (Request.QueryString[HtmlHelperExtensionsForReallyTinyCms.ReturnUrlKey] != null)
                return Redirect(Request.QueryString[HtmlHelperExtensionsForReallyTinyCms.ReturnUrlKey]);

            return Content("Content successfully saved");
        }
    }
}
