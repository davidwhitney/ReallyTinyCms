using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;
using ReallyTinyCms.Core.Model;
using ReallyTinyCms.Core.Storage;

namespace ReallyTinyCms.ExampleWebsite
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            var contentItems = new List<CmsContentItem> {new CmsContentItem("HomePageTop") {Content = "<b>Woo!</b>"}};
            var cmsContentRepository = new StaticDictionaryCmsContentRepository(contentItems);

            ReallyTinyCms
                .ConfigureWithContentSource(() => cmsContentRepository)
                .AndRefreshInterval(1.Minute())
                .WhenCacheRefreshes(() => Debug.WriteLine("ReallyTinyCms just performed a cache refresh"))
                .WhenContentIsRequested((contentItemName, defaultValue) => Debug.WriteLine("ReallyTinyCms just performed a lookup for " + contentItemName));

        }
    }
}