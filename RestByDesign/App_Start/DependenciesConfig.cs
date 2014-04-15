using System.Reflection;
using System.Web.Http;
using Autofac.Integration.WebApi;

namespace RestByDesign
{
    public class DependenciesConfig
    {
        public static void Configure()
        {
            var container = IocHelper.CreateContainer(bld => bld.RegisterApiControllers(Assembly.GetExecutingAssembly()));
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}