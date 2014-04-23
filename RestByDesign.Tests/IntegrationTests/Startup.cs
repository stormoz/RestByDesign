using System.Web.Http;
using Owin;

namespace RestByDesign.Tests.IntegrationTests
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            WebApiApplication.ConfigRegisterRestByDesignApp(config);

            appBuilder.UseWebApi(config);
        }
    }
}
