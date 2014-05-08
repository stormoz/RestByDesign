using System.Data.Entity;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Infrastructure.Mapping;

namespace RestByDesign
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(config => ConfigRestByDesignApp(config, false));

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        internal static void ConfigRestByDesignApp(HttpConfiguration config, bool dummyRepo = true)
        {
            //Web Api config
            WebApiConfig.Register(config);

            //Configure DI
            DependenciesConfig.Configure(config, dummyRepo);

            //Configure mappings
            MappingRegistration.RegisterMappings();

            if (!dummyRepo)
            {
                //Initialise DB
                Database.SetInitializer(new RestByDesignContextInitializer());
            }
        }
    }
}
