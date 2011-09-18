using System.Web.Mvc;
using System.Web.Routing;
using ReallyTinyCms.Core;

namespace ReallyTinyCms.Mvc.Controllers
{
    public class ReallyTinyCmsController : Controller
    {
        private readonly IContentService _contentService;
        private ReallyTinyCmsUiSugar _uiSugar;

        public ReallyTinyCmsController()
            : this(ReallyTinyCms.ContentService)
        {
        }

        public ReallyTinyCmsController(IContentService contentService)
        {
            _contentService = contentService;
            _uiSugar = new ReallyTinyCmsUiSugar(_contentService);
        }

        public ActionResult Index(bool failedAuth = false, bool invalidName = false)
        {
            return View("~/bin/Views/ReallyTinyCms/Index.aspx");
        }

        [HttpGet]
        public ActionResult Edit(string name)
        {
            if (!_uiSugar.EditEnabledForCurrentRequest())
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
            if (!_uiSugar.EditEnabledForCurrentRequest())
            {
                return RedirectToAction("Index", new {failedAuth = true});
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                return RedirectToAction("Index", new {invalidName = true});
            }

            _contentService.SaveContentFor(name, content);

            var routeValues = new RouteValueDictionary {{"name", name}};
            foreach (var key in Request.QueryString.AllKeys)
            {
                var value = Request.QueryString[key];
                routeValues.Add(key, value);
            }

            return RedirectToAction("Edit", routeValues);
        }
    }
}
