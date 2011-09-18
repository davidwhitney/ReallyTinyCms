using System.Configuration;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;
using ReallyTinyCms.Core.ContentPipline;
using ReallyTinyCms.Core.Storage;

namespace ReallyTinyCms.ExampleWebsite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.MapRoute("Default", "{controller}/{action}/{id}",  new { controller = "Home", action = "Index", id = UrlParameter.Optional } );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

        	ConfigureCms();
        }

        private static void ConfigureCms()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["CmsDatabase"].ConnectionString;
            var cmsContentRepository = new SqlCmsContentRepository(connectionString);

            ReallyTinyCms
                .ConfigureWithContentSource(() => cmsContentRepository, 1.Minute())
                .WhenCacheRefreshes(() => Debug.WriteLine("Just performed a cache refresh"))
                .WhenContentIsRequested((contentItemName, defaultValue) => Debug.WriteLine("Just performed a lookup for " + contentItemName))
                .EditModeShouldBeEnabledWhen(requestContext => requestContext.HttpContext.Request.QueryString.ToString().Contains("editmode"))
                .WithFilters(new NoOpFilter())
                .ConfigureEditRoute(RouteTable.Routes, "cms");
        }

        protected void MinimalSqlBackedExample()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["CmsDatabase"].ConnectionString;
            var cmsContentRepository = new SqlCmsContentRepository(connectionString);

            ReallyTinyCms
                .ConfigureWithContentSource(() => cmsContentRepository, 1.Minute())
                .EditModeShouldBeEnabledWhen(requestContext => requestContext.HttpContext.Request.QueryString.ToString().Contains("editmode"))
                .ConfigureEditRoute(RouteTable.Routes, "cms");
        }
    }
}