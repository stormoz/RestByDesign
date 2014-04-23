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
            
            GlobalConfiguration.Configure(ConfigRegisterRestByDesignApp);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        internal static void ConfigRegisterRestByDesignApp(HttpConfiguration config)
        {
            //Web Api config
            WebApiConfig.Register(config);

            //Configure DI
            DependenciesConfig.Configure(config);

            //Configure mappings
            MappingRegistration.RegisterMappings();

#if (!DUMMYREPO)
            //Initialise DB
            Database.SetInitializer(new RestByDesignContextInitializer());
#endif
        }
    }
}
