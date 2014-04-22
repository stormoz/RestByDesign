#if (!DEBUG)
using System.Data.Entity;
#endif

using System;
using System.Linq;
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

            //ViewEngines.Engines.Clear();
            //ViewEngines.Engines.Add(new RazorAreaAwareViewEngine());

            //var razorViewEngine = ((VirtualPathProviderViewEngine) ViewEngines.Engines[1]);
            //var newViewSearchPaths = razorViewEngine.AreaViewLocationFormats.ToList();
            //newViewSearchPaths.AddRange(
            //    new[]
            //    {
            //        "~/Areas/{2}/Views/{1}/{0}.cshtml",
            //        "~/Areas/{2}/Views/Shared/{0}.cshtml"
            //    });

            //razorViewEngine.AreaViewLocationFormats = newViewSearchPaths.ToArray();

            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
