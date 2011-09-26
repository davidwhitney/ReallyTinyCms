using System.Web.Mvc;

namespace ReallyTinyCms.Tests.Fakes
{
    
    public class ViewEngineFake : VirtualPathProviderViewEngine
    {
        public ViewEngineFake() 
        {
            base.PartialViewLocationFormats = new[] { "~/Views/{1}/{0}.aspx", "~/Views/Shared/{0}.aspx" };
        }

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            return false;
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            throw new System.NotImplementedException();
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            throw new System.NotImplementedException();
        }
    }
}