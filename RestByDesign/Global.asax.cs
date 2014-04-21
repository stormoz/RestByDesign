#if (!DEBUG)
using System.Data.Entity;
#endif

using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RestByDesign
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //Web Api config
            GlobalConfiguration.Configure(WebApiConfig.Register);

#if (!DEBUG)
            //Initialise DB
            Database.SetInitializer(new RestByDesignContextInitializer());
#endif
            //Configure DI
            DependenciesConfig.Configure();

            //Configure mappings
            MappingRegistration.RegisterMappings();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
