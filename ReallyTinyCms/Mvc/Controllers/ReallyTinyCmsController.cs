using System.Web.Mvc;
using ReallyTinyCms.Core;

namespace ReallyTinyCms.Mvc.Controllers
{
    public class ReallyTinyCmsController: Controller
    {
        private readonly IContentService _contentService;

        public ReallyTinyCmsController():this(ReallyTinyCms.ContentService)
        {
        }
        
        public ReallyTinyCmsController(IContentService contentService)
        {
            _contentService = contentService;
        }

        public ActionResult Index(bool failedAuth = false)
        {
            return View("~/bin/Views/ReallyTinyCms/Index.aspx");
        }

        [HttpGet]
        public ActionResult Edit(string name)
        {
            var uiSugar = new ReallyTinyCmsUiSugar(_contentService);
            
            if(!uiSugar.EditEnabledForCurrentRequest())
            {
                return RedirectToAction("Index", new { failedAuth = true });
            }


            return View("~/bin/Views/ReallyTinyCms/Edit.aspx");
        }

        [HttpPost]
        public ActionResult Edit(string name, string content)
        {
            return View("~/bin/Views/ReallyTinyCms/Edit.aspx");
        }
    }
}
