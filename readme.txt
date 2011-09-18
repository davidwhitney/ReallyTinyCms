An ultra lightweight, lower-case "cms" which allows for editing simple content on a page.
Very configurable, designed to be plugged in to an IoC container.

Just add your own ICmsContentRepository, or use the provided Dapper-based Sql one.

At its simplest, the setup looks like this:

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

        	  var connectionString = ConfigurationManager.ConnectionStrings["CmsDatabase"].ConnectionString;
            var cmsContentRepository = new SqlCmsContentRepository(connectionString);

            ReallyTinyCms
                .ConfigureWithContentSource(() => cmsContentRepository, 5.Minutes())
                .EditModeShouldBeEnabledWhen(requestContext => requestContext.HttpContext.Request.QueryString.ToString().Contains("editmode"))
                .ConfigureEditRoute(RouteTable.Routes, "cms");
        }
 
 And usage looks like this:
 
    <%@ Import Namespace="ReallyTinyCms.Mvc" %>
 
     <p>
        <%= Html.Cms().ContentFor("HomePageTop") %>
        <%= Html.Cms().ContentFor("HomePageTop2", ()=>@"<b>This is default bold text</b>") %>
    </p>

    <p>
    <%if(Html.Cms().EditEnabledForCurrentRequest()){%>
        Whoop editing enabled for this request.
    <% } %>    
    </p>
 
    
Which is pretty nice. That will allow anyone who puts ?editmode=something in the query string.
You'll probably want some kind of security that doesn't suck here, some user / rolls / permission / facepalm rules.

Don't worry about databases with the default SqlCmsContentRepository, as long as it has permissions, it'll create its own database and do its own magic.
The entire CMS items table is loaded into memory every X minutes (whatever you specify in ConfigureWithContentSource) by design.
Please don't try run a content heavy site off this, the model isn't geared in that direction.

If you don't like my default CmsContentRepository feel free to implement your own:

    public interface ICmsContentRepository
    {
        IList<CmsContentItem> RetrieveAll();
        CmsContentItem Retrieve(string contentItemName);
        void SaveOrUpdate(CmsContentItem item);
        void Delete(string contentItemName);

        bool StorageExists();
        void CreateStorage();
    }
    
You'll need to take care of checking for database existance and creating storage schemas and mappings if you do this.

I'd recommend using some kind of IoC container and binding the function requested in ConfigureWithContentSource to that, to make sure your data access is
consistent.

More documentation coming soon, but there's a sample file in the project that should show you how to get started.
You'll need to copy the ReallyTinyCms/Views into your web apps /bin directory for the editing to work.