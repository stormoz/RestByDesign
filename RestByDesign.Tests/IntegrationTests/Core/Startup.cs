using System.Web.Http;
using Owin;

namespace RestByDesign.Tests.IntegrationTests.Core
{
    public class Startup
    {
        public static HttpConfiguration GlobalConfiguration { get; private set; }

        public void Configuration(IAppBuilder appBuilder)
        {
            GlobalConfiguration = new HttpConfiguration();

            // register routes, IoC, mappings, DAL
            WebApiApplication.ConfigRegisterRestByDesignApp(GlobalConfiguration, dummyRepo: true);

            appBuilder.UseWebApi(GlobalConfiguration);
        }
    }
}
