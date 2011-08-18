using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;
using ReallyTinyCms.Core.ContentPipline;
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

			var cmsContentRepository = new SqlCmsContentRepository(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\github-davidwhitney\ReallyTinyCms\ReallyTinyCms.ExampleWebsite\App_Data\MyData.mdf;Integrated Security=True;User Instance=True");

            ReallyTinyCms
                .ConfigureWithContentSource(() => cmsContentRepository)
                .AndRefreshInterval(1.Minute())
                .WhenCacheRefreshes(() => Debug.WriteLine("Just performed a cache refresh"))
                .WhenContentIsRequested((contentItemName, defaultValue) => Debug.WriteLine("Just performed a lookup for " + contentItemName))
                .EditModeShouldBeEnabledWhen(requestContext => requestContext.HttpContext.Request.QueryString.ToString().Contains("editmode")) /* You'd want to check user auth here */
                .WithFilters(new NoOpFilter(), new NoOpFilter());
        }
    }
}