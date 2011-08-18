using System.Configuration;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;
using ReallyTinyCms.Core.ContentPipline;
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

        	var connectionString = ConfigurationManager.ConnectionStrings["CmsDatabase"].ConnectionString;
			var cmsContentRepository = new SqlCmsContentRepository(connectionString);

        	ReallyTinyCms
        		.ConfigureWithContentSource(() => cmsContentRepository, 1.Minute())
        		.WhenCacheRefreshes(() => Debug.WriteLine("Just performed a cache refresh"))
        		.WhenContentIsRequested((contentItemName, defaultValue) => Debug.WriteLine("Just performed a lookup for " + contentItemName))
        		.EditModeShouldBeEnabledWhen(requestContext => requestContext.HttpContext.Request.QueryString.ToString().Contains("editmode"))
				.WithFilters(new NoOpFilter());
        }
    }
}