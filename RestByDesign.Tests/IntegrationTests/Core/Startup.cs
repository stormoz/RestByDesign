using System;
using System.IO;
using System.Threading;
using System.Web.Http;
using Owin; //install-package Microsoft.AspNet.WebApi.OwinSelfHost

namespace RestByDesign.Tests.IntegrationTests.Core
{
    public class Startup
    {
        public static HttpConfiguration GlobalConfiguration { get; private set; }

        public void Configuration(IAppBuilder appBuilder)
        {
            //Configure whether to use dummy repo for tests
            var useDummyRepo = true;//NB: if false then DB should already exist

            GlobalConfiguration = new HttpConfiguration();

            if (!useDummyRepo)
                SetupDataDirectoryLocation();

            // register routes, IoC, mappings, DAL
            WebApiApplication.ConfigRestByDesignApp(GlobalConfiguration, dummyRepo: useDummyRepo);

            appBuilder.UseWebApi(GlobalConfiguration);
        }

        private static void SetupDataDirectoryLocation()
        {
            var asmPath = Thread.GetDomain().BaseDirectory;

            var parent1 = Directory.GetParent(asmPath).ToString();
            var parent2 = Directory.GetParent(parent1).ToString();
            var parent3 = Directory.GetParent(parent2).ToString();
            var mdfPath = Path.Combine(parent3, "RestByDesign", "App_Data");

            AppDomain.CurrentDomain.SetData("DataDirectory", mdfPath);
        }
    }
}
